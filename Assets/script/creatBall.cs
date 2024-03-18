using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creatBall : MonoBehaviour
{
    public static bool isCollider = false;
    public GameObject ballFire; // Quả bóng mẫu bắn
    public Transform pointFire; // Điểm tạo bóng bắn
    public GameObject ballNext; // Quả bóng mẫu next
    public Transform pointNext; // Điểm tạo bóng next
    public GameObject newBallFire; // bóng bắn
    public GameObject newBallNext; // bóng next
    public Color[] ballCollor;
    public static Color n; // Biến lưu trữ màu sắc bóng next
    public static Color m; // Biến lưu trữ màu sắc bóng bắn
    // Start is called before the first frame update
    void Start()
    {
        m = ballCollor[Random.Range(0, ballCollor.Length)]; // gán m bằng 1 màu ngẫu nhiên
        newBallFire = Instantiate(ballFire, new Vector3(pointFire.position.x,pointFire.position.y,pointFire.position.z), Quaternion.identity); // Tạo bóng bắn mới
        newBallFire.GetComponent<SpriteRenderer>().color = m; // tạo màu cho bóng
        
        n = ballCollor[Random.Range(0, ballCollor.Length)]; // gán n bằng 1 màu ngẫu nhiên
        newBallNext = Instantiate(ballNext, new Vector3(pointNext.position.x,pointNext.position.y,pointNext.position.z), Quaternion.identity); // Tạo bóng next mới
        newBallNext.GetComponent<SpriteRenderer>().color = n; // tạo màu cho bóng
    }

    // Update is called once per frame
    void Update()
    {
        if(isCollider){
            isCollider = false;
            Destroy_Creat();
        }
    }
    void Destroy_Creat(){
        Destroy(newBallNext);
        m = n; // gán m bằng màu next
        newBallFire = Instantiate(ballFire, new Vector3(pointFire.position.x,pointFire.position.y,pointFire.position.z), Quaternion.identity); // Tạo bóng bắn mới
        newBallFire.GetComponent<SpriteRenderer>().color = m; // tạo màu cho bóng
        
        n = ballCollor[Random.Range(0, ballCollor.Length)]; // gán n bằng 1 màu ngẫu nhiên
        newBallNext = Instantiate(ballNext, new Vector3(pointNext.position.x,pointNext.position.y,pointNext.position.z), Quaternion.identity); // Tạo bóng next mới
        newBallNext.GetComponent<SpriteRenderer>().color = n; // tạo màu cho bóng
    }
    public void swapBall(){
        Color a;
        a = m;
        m = n;
        n = a;
        newBallFire.GetComponent<SpriteRenderer>().color = m; // tạo màu cho bóng
        newBallNext.GetComponent<SpriteRenderer>().color = n; // tạo màu cho bóng
    }
}
