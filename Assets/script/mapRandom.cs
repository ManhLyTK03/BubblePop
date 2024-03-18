using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapRandom : MonoBehaviour
{
    public GameObject ballMap; // prefab bóng để tạo map
    public SpriteRenderer ballSR;
    public float widthBall;
    public int col,row;
    float screenWidth = Screen.width; // Chiều rộng màn hình game
    public float[,] mapPosition;
    public Color[] ballCollor;
    // Start is called before the first frame update
    void Start()
    {
        float leftEdgeX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;// Lấy tọa độ x của cạnh trái (left) của màn hình
        float topEdgeY = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;// Lấy tọa độ y của cạnh trên (top) của màn hình
        mapPosition = new float[col,row];
        widthBall = ballSR.bounds.size.x; // Lấy độ rộng của SpriteRenderer
        for (int i = 0; i <= col ; i++)
        {
            for (int j = 0; j <= row ; j++)
            {
                if(i%2 != 1 || j != row){
                    Vector3 ballPosition = new Vector3(leftEdgeX + i%2*widthBall/2 + widthBall/2 + widthBall*j ,topEdgeY - widthBall/2 - Mathf.Sqrt(3f)*widthBall*i/2,0f);
                    GameObject newBallMap = Instantiate(ballMap, ballPosition, Quaternion.identity);
                    // Gắn GameObject con vừa tạo vào GameObject cha
                    newBallMap.transform.parent = transform;
                    newBallMap.GetComponent<SpriteRenderer>().color = ballCollor[Random.Range(0, ballCollor.Length)]; // tạo màu cho bóng
                }
            }
        }
    }
}