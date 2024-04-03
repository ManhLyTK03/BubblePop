using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mapRandom : MonoBehaviour
{
    public SpriteRenderer ballSR;
    public float widthBall;
    public Transform ceiling;
    public int col,row;
    float screenWidth = Screen.width; // Chiều rộng màn hình game
    public float[,] mapPosition;
    public GameObject ballMap;
    public Sprite[] ballColors;
    public static bool checkWin = false;
    public float leftEdgeX,topEdgeY;
    public string[] tagsToSearch; // Mảng các tag cần tìm
    // Start is called before the first frame update
    void Start()
    {
        leftEdgeX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;// Lấy tọa độ x của cạnh trái (left) của màn hình
        topEdgeY = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;// Lấy tọa độ y của cạnh trên (top) của màn hình
        mapPosition = new float[col,row];
        widthBall = ballSR.bounds.size.x; // Lấy độ rộng của SpriteRenderer
        tagsToSearch = new string[]{"ballStone"};
        // Cập nhật vị trí của ceiling
        ceiling.transform.position = new Vector3(ceiling.transform.position.x, topEdgeY, ceiling.transform.position.z);
        creatMap();
    }
    void Update(){
        if(checkWin){
            SceneManager.LoadScene("mainPlay");
        }
    }
    void creatMap(){
        checkWin = false;
        for (int i = 0; i <= col ; i++)
        {
            for (int j = 0; j <= row ; j++)
            {
                if(i%2 != 1 || j != row){
                    Vector3 ballPosition = new Vector3(leftEdgeX + i%2*widthBall/2 + widthBall/2 + widthBall*j ,ceiling.position.y - ceiling.localScale.y/2 - widthBall/2 - Mathf.Sqrt(3f)*widthBall*i/2,0f);
                    GameObject newBallMap = Instantiate(ballMap, ballPosition, Quaternion.identity);
                    Sprite spriteRandom = ballColors[Random.Range(0, ballColors.Length)];
                    newBallMap.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spriteRandom;
                    // Duyệt qua mỗi tag trong mảng tagsToSearch
                    foreach (string tagToSearch in tagsToSearch){
                        if(spriteRandom.name == tagToSearch){
                            newBallMap.tag = tagToSearch;
                        }
                    }
                    // Gắn GameObject con vừa tạo vào GameObject cha
                    newBallMap.transform.parent = transform;
                }
            }
        }
    }
}