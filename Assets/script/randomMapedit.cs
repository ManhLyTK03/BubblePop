using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;


public class randomMapedit : MonoBehaviour
{
    public static bool boolRandomMap = false;
    public SpriteRenderer ballSR;
    public float widthBall;
    public int col;
    public int row = 10;
    public GameObject ballMap;
    public GameObject ballIce;
    public Sprite[] ballColors;
    public Sprite spriteStone,spriteHole;
    public float leftEdgeX;
    public Vector3[] mapPositions;
    // Start is called before the first frame update
    void Start()
    {
        widthBall = ballSR.bounds.size.x; // Lấy độ rộng của SpriteRenderer
        Invoke("creatMap", 0.1f);
    }
    void creatMap(){
        leftEdgeX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;// Lấy tọa độ x của cạnh trái (left) của màn hình
        mapPositions = new Vector3[0];
        if(!boolRandomMap){
            creatMapRanDom();
        }
        else{
            continueMap();
        }
    }
    void creatMapRanDom(){
        for (int i = 0; i <= col ; i++)
        {
            for (int j = 0; j <= (row/2)+((i+1)%2)-1 ; j++)
            {
                float pointX = leftEdgeX + widthBall/(((i+1)%2)+1) + widthBall*j;
                Vector3 ballPosition = new Vector3(Mathf.Round(pointX * 100f) / 100f, widthBall/2f + Mathf.Sqrt(3f)*widthBall*i/2f,0f);
                mapPositions = mapPositions.Concat(new[] { ballPosition }).ToArray();
            }
        }
        foreach (Vector3 mapPosition in mapPositions){
            Sprite spriteRandom = ballColors[Random.Range(0, ballColors.Length)];
            GameObject newBallMap = Instantiate(ballMap, mapPosition, Quaternion.identity); // tạo bóng
            newBallMap.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spriteRandom; // gán img
            newBallMap.transform.parent = transform;
            if(mapPosition.x != 0f){
                newBallMap = Instantiate(ballMap, new Vector3(-mapPosition.x, mapPosition.y, mapPosition.z) , Quaternion.identity); // tạo bóng
                newBallMap.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spriteRandom; // gán img
                newBallMap.transform.parent = transform;
            }
        }
    }
    void continueMap(){
        for (int i = 0; i <= col ; i++)
        {
            for (int j = 0; j <= row + ((i+1)%2)-1; j++)
            {
                float pointX = leftEdgeX + widthBall/(((i+1)%2)+1) + widthBall*j;
                Vector3 ballPosition = new Vector3(Mathf.Round(pointX * 100f) / 100f, widthBall/2f + Mathf.Sqrt(3f)*widthBall*i/2f,0f);
                mapPositions = mapPositions.Concat(new[] { ballPosition }).ToArray();
            }
        }
        for(int i = 0; i < mapRandom.typeMap.Length; i++){
            Sprite spriteRandom = null;
            if(mapRandom.typeMap[i] == 1 || mapRandom.typeMap[i] == 21){
                spriteRandom = ballColors[0];
            }
            if(mapRandom.typeMap[i] == 2 || mapRandom.typeMap[i] == 22){
                spriteRandom = ballColors[1];
            }
            if(mapRandom.typeMap[i] == 3 || mapRandom.typeMap[i] == 23){
                spriteRandom = ballColors[2];
            }
            if(mapRandom.typeMap[i] == 4 || mapRandom.typeMap[i] == 24){
                spriteRandom = ballColors[3];
            }
            if(mapRandom.typeMap[i] == 11){
                spriteRandom = spriteStone;
            }
            if(mapRandom.typeMap[i] == 12){
                spriteRandom = spriteHole;
            }
            if(spriteRandom != null){
                GameObject newBallMap = Instantiate(ballMap, mapPositions[i], Quaternion.identity); // tạo bóng
                newBallMap.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spriteRandom; // gán img
                newBallMap.transform.parent = transform;
                if(mapRandom.typeMap[i] > 20){
                    newBallMap.transform.parent = transform;
                    GameObject newBallIce = Instantiate(ballIce, mapPositions[i], Quaternion.identity); // tạo băng
                    newBallIce.transform.parent = newBallMap.transform;
                    newBallIce.transform.localScale = new Vector3(1,1,1);
                }
            }
        }
    }
}