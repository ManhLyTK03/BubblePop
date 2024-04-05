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
    public int col;
    public int row = 10;
    public GameObject ballMap;
    public Sprite[] ballColors;
    public Sprite[] ballIces;
    private Dictionary<string, Sprite> spriteDictionary = new Dictionary<string, Sprite>(); // Dictionary để ánh xạ tên và Sprite tương ứng
    public int intStone,intIce,intHole;
    public Sprite spriteStone,spriteHole;
    public static bool checkWin = false;
    public float leftEdgeX;
    public string[] tagsToSearch; // Mảng các tag cần tìm
    public Vector3[] mapPositions;
    public SpriteRenderer spriteRenderer;
    private float widthMap;
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
        creatMap();
    }
    void Update(){
        if(checkWin){
            SceneManager.LoadScene("mainPlay");
        }
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
            Sprite spriteRandom = ballIces[Random.Range(0, ballIces.Length)];
            newBallMap.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spriteRandom; // gán img
            newBallMap.GetComponent<SpriteRenderer>().sprite = GetSprite(spriteRandom.name.Substring(0, spriteRandom.name.Length - 3)); // gán img
            newBallMap.tag = "ballIce";
            newBallMap.transform.parent = transform;
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
}