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
    public int col;
    public int row = 10;
    public GameObject ballMap;
    public GameObject ballIce;
    public Sprite[] ballColors;
    public Sprite[] spriteLai;
    private Dictionary<string, Sprite> spriteDictionary = new Dictionary<string, Sprite>(); // Dictionary để ánh xạ tên và Sprite tương ứng
    public int intStone,intIce,intHole,intLai;
    public Sprite spriteStone,spriteHole;
    public static bool checkWin = false;
    public float leftEdgeX;
    public string[] tagsToSearch; // Mảng các tag cần tìm
    public Vector3[] mapPositions;
    public SpriteRenderer spriteRenderer;
    private float widthMap;
    public Vector3 targetMap;
    public float speedMap = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        // Điền thông tin từ mảng vào Dictionary
        foreach (Sprite sprite in ballColors)
        {
            spriteDictionary.Add(sprite.name, sprite);
        }
        leftEdgeX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;// Lấy tọa độ x của cạnh trái (left) của màn hình
        widthBall = ballSR.bounds.size.x; // Lấy độ rộng của SpriteRenderer
        widthMap = widthBall + (widthBall/2)*Mathf.Sqrt(3f)*col;
        // Cập nhật vị trí của wallTop
        wallTop.localScale = new Vector3(wallTop.localScale.x, widthBall/2, wallTop.localScale.z);
        wallTop.position = new Vector3(wallTop.transform.position.x, widthMap, wallTop.transform.position.z);
        wallTopImg.position = new Vector3(wallTopImg.transform.position.x, widthMap + wallTopImg.localScale.y, wallTopImg.transform.position.z);
        creatMap();
    }
    void Update(){
        if(checkWin){
            Restart();
        }
        if(ballCollider.daVacham){
            Invoke("setMapPosition", 0.1f);
        }
        if (transform.position.y > targetMap.y + 0.05f || transform.position.y < targetMap.y - 0.05f)
        {
            if(wallTop.position.y > ballBoom.maxCeiling - widthBall){
                transform.position = Vector3.Lerp(transform.position, targetMap, speedMap * Time.deltaTime);
            }
        }
    }
    public void Restart(){
        SceneManager.LoadScene("mainPlay");
    }
    void creatMap(){
        checkWin = false;
        mapPositions = new Vector3[0];
        for (int i = 0; i <= col ; i++)
        {
            for (int j = 0; j <= row ; j++)
            {
                if(i%2 != 1 || j != row){
                    Vector3 ballPosition = new Vector3(leftEdgeX + i%2*widthBall/2 + widthBall/2 + widthBall*j ,widthBall/2 + Mathf.Sqrt(3f)*widthBall*i/2,0f);
                    mapPositions = mapPositions.Concat(new[] { ballPosition }).ToArray();
                }
            }
        }
        for (int i = 0; i < intStone; i++){
            int positionRandom = Random.Range(0,mapPositions.Length);
            Vector3 positionMap = mapPositions[positionRandom]; // vị trí tạo bóng
            mapPositions = mapPositions.Where((positionMap, index) => index != positionRandom).ToArray();
            GameObject newBallMap = Instantiate(ballMap, positionMap, Quaternion.identity); // tạo bóng
            newBallMap.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spriteStone; // gán img
            newBallMap.tag = "ballStone";
            newBallMap.transform.parent = transform;
        }
        for (int i = 0; i < intIce; i++){
            int positionRandom = Random.Range(0,mapPositions.Length);
            Vector3 positionMap = mapPositions[positionRandom]; // vị trí tạo bóng
            mapPositions = mapPositions.Where((positionMap, index) => index != positionRandom).ToArray();
            GameObject newBallMap = Instantiate(ballMap, positionMap, Quaternion.identity); // tạo bóng
            Sprite spriteRandom = ballColors[Random.Range(0, ballColors.Length)];
            newBallMap.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spriteRandom; // gán img
            newBallMap.tag = "ballIce";
            newBallMap.transform.parent = transform;
            GameObject newBallIce = Instantiate(ballIce, positionMap, Quaternion.identity); // tạo băng
            newBallIce.transform.parent = newBallMap.transform;
            newBallIce.transform.localScale = new Vector3(1,1,1);
        }
        for (int i = 0; i < intHole; i++){
            int positionRandom = Random.Range(0,mapPositions.Length);
            Vector3 positionMap = mapPositions[positionRandom]; // vị trí tạo bóng
            mapPositions = mapPositions.Where((positionMap, index) => index != positionRandom).ToArray();
            GameObject newBallMap = Instantiate(ballMap, positionMap, Quaternion.identity); // tạo bóng
            newBallMap.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spriteHole; // gán img
            newBallMap.tag = "ballHole";
            newBallMap.transform.parent = transform;
        }
        for (int i = 0; i < intLai; i++){
            int positionRandom = Random.Range(0,mapPositions.Length);
            Vector3 positionMap = mapPositions[positionRandom]; // vị trí tạo bóng
            mapPositions = mapPositions.Where((positionMap, index) => index != positionRandom).ToArray();
            GameObject newBallMap = Instantiate(ballMap, positionMap, Quaternion.identity); // tạo bóng
            newBallMap.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spriteLai[Random.Range(0,spriteLai.Length-1)]; // gán img
            newBallMap.tag = "ballLai";
            newBallMap.transform.parent = transform;
        }
        foreach (Vector3 mapPosition in mapPositions){
            GameObject newBallMap = Instantiate(ballMap, mapPosition, Quaternion.identity); // tạo bóng
            Sprite spriteRandom = ballColors[Random.Range(0, ballColors.Length)];
            newBallMap.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spriteRandom; // gán img
            newBallMap.transform.parent = transform;
        }
    }
    // Hàm để lấy sprite dựa trên tên
    public Sprite GetSprite(string spriteName){
        Sprite sprite;
        if (spriteDictionary.TryGetValue(spriteName, out sprite))
        {
            return sprite;
        }
        else
        {
            return null;
        }
    }
    string[] GetColorFromTag(string colorName)
    {
        switch (colorName)
        {
            case "laiballBR":
                return new string[] { "ballBlue", "ballRed" };
            case "laiballBY":
                return new string[] { "ballBlue", "ballYellow" };
            case "laiballRY":
                return new string[] { "ballYellow", "ballRed" };
            // Thêm các màu khác nếu cần
            default:
                return new string[] { "ballBlue", "ballRed" }; // Màu mặc định
        }
    }
    void setMapPosition(){
        if(ballCollider.daVacham){
            ballCollider.daVacham = false;
        }
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
    }
}