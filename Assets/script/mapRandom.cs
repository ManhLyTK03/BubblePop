using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;


public class mapRandom : MonoBehaviour
{
    public SpriteRenderer ballSR;
    public float widthBall;
    public Transform wallTop;
    public Transform wallTopImg;
    public static int col;
    public int row = 10;
    public GameObject ballMap;
    public GameObject ballIce;
    public Sprite[] ballColors;
    public Sprite spriteStone,spriteHole;
    public float leftEdgeX,rightEdgeX;
    public Vector3[] mapPositions;
    public static int[] typeMap;
    private float widthMap  = 0.0f;
    public Vector3 targetMap;
    public float speedMap = 5.0f;
    public float overlapRadius; // Bán kính để tìm các GameObject khác va chạm với "ball"
    public bool checkScrolling;
    public static bool checkStop;
    // Start is called before the first frame update
    void Start()
    {
        checkScrolling = false;
        checkStop = false;
        col = checkCol(typeMap.Length);
        lostGame.intMaxBall = col + 10;
        leftEdgeX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;// Lấy tọa độ x của cạnh trái (left) của màn hình
        rightEdgeX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        widthBall = ballSR.bounds.size.x; // Lấy độ rộng của SpriteRenderer
        overlapRadius = (widthBall/2)*1.5f;
        // Cập nhật vị trí của wallTop
        creatMap();
    }
    void Update(){
        if(ghiban.checkGhiban){
            Invoke("setMapPosition", 0.1f);
        }
        if (transform.position.y > targetMap.y + 0.05f || transform.position.y < targetMap.y - 0.05f)
        {
            if(wallTop.position.y > ballBoom.maxCeiling - widthBall){
                transform.position = Vector3.Lerp(transform.position, targetMap, speedMap * Time.deltaTime);
            }
            else{
                if(!checkStop){
                    checkStop = true;
                }
                else{
                    if(checkScrolling){
                        checkScrolling = false;
                        creatBall.isCreat = true;
                    }
                }
            }
        }
        else{
            if(checkScrolling){
                checkScrolling = false;
                creatBall.isCreat = true;
            }
        }
    }
    public void Restart(){
        SceneManager.LoadScene("mainPlay");
    }
    public void Home(){
        SceneManager.LoadScene("mainMenu");
    }
    public void EditMap(){
        randomMapedit.col = col;
        randomMapedit.boolRandomMap = true;
        SceneManager.LoadScene("CreateMap");
    }
    void creatMap(){
        mapPositions = new Vector3[0];
        wallTop.localScale = new Vector3(wallTop.localScale.x, widthBall/2, wallTop.localScale.z);
        for (int i = 0; i <= col ; i++)
        {
            for (int j = 0; j <= row + ((i+1)%2)-1; j++)
            {
                float pointX = leftEdgeX + widthBall/(((i+1)%2)+1) + widthBall*j;
                Vector3 ballPosition = new Vector3(Mathf.Round(pointX * 100f) / 100f, widthBall/2f + Mathf.Sqrt(3f)*widthBall*i/2f,0f);
                mapPositions = mapPositions.Concat(new[] { ballPosition }).ToArray();
            }
        }
        for(int i = typeMap.Length-1; i >= 0; i--){
            if(typeMap[i] != 10){
                if(widthMap == 0.0f){
                    widthMap = widthBall/2 + mapPositions[i].y;
                    wallTop.position = new Vector3(wallTop.transform.position.x, widthMap, wallTop.transform.position.z);
                    wallTopImg.position = new Vector3(wallTopImg.transform.position.x, widthMap + wallTopImg.localScale.y, wallTopImg.transform.position.z);
                    break;
                }
            }
        }
        for(int i = 0; i < typeMap.Length; i++){
            Sprite spriteRandom = null;
            if(typeMap[i] == 1 || typeMap[i] == 21){
                spriteRandom = ballColors[0];
            }
            if(typeMap[i] == 2 || typeMap[i] == 22){
                spriteRandom = ballColors[1];
            }
            if(typeMap[i] == 3 || typeMap[i] == 23){
                spriteRandom = ballColors[2];
            }
            if(typeMap[i] == 4 || typeMap[i] == 24){
                spriteRandom = ballColors[3];
            }
            if(typeMap[i] == 11){
                spriteRandom = spriteStone;
            }
            if(typeMap[i] == 12){
                spriteRandom = spriteHole;
            }
            if(typeMap[i] == 10){
                spriteRandom = null;
            }
            if(spriteRandom != null){
                GameObject newBallMap = Instantiate(ballMap, mapPositions[i], Quaternion.identity); // tạo bóng
                newBallMap.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spriteRandom; // gán img
                newBallMap.transform.parent = transform;
                if(typeMap[i] > 20){
                    newBallMap.tag = "ballIce";
                    newBallMap.transform.parent = transform;
                    GameObject newBallIce = Instantiate(ballIce, mapPositions[i], Quaternion.identity); // tạo băng
                    newBallIce.transform.parent = newBallMap.transform;
                    newBallIce.transform.localScale = new Vector3(1,1,1);
                }
                if(typeMap[i] == 11){
                    newBallMap.tag = "ballStone";
                }
                if(typeMap[i] == 12){
                    newBallMap.tag = "ballHole";
                }
            }
            else{
                GameObject newBallMap = new GameObject("NewBallMap"); // Tạo một GameObject mới với tên là "NewBallMap"
                newBallMap.transform.position = mapPositions[i]; // Đặt vị trí của GameObject mới
                newBallMap.transform.parent = transform;
            }
        }
    }
    void setMapPosition(){
        string[] tags = new string[] { "ballMap", "ballIce" };
        List<GameObject> gameObjects = new List<GameObject>();

        foreach (var tag in tags)
        {
            gameObjects.AddRange(GameObject.FindGameObjectsWithTag(tag));
        }

        GameObject minObject = gameObjects.OrderBy(go => go.transform.position.y).FirstOrDefault();

        if (minObject != null)
        {
            targetMap = new Vector3(transform.position.x, transform.position.y - (minObject.transform.position.y - widthBall / 2), transform.position.z);
        }
        checkScrolling = true;
    }
    int checkCol(int sumBall){
        int checkRow = 10;
        for(int i = 0; i <= 100; i++){
            if(i%2==0){
                checkRow = 11;
            }
            else{
                checkRow = 10;
            }
            sumBall -= checkRow;
            if(sumBall == 0){
                return i;
            }
        }
        return sumBall;
    }
}