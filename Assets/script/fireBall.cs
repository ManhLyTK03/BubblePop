using UnityEngine;
using UnityEngine.EventSystems;

public class fireBall : MonoBehaviour
{
    public float ballSpeed = 10f; // Tốc độ của quả bóng
    public static bool boolFire;    
    private Vector3 direction;
    private float widthBall;
    public Vector3 mousePosition;
    public bool checkClickDown = false;
    private int pointLine;

    void Start(){
        boolFire = false;
        // Lấy ra chiều rộng và chiều cao của màn hình
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        // chiều rộng của bóng
        SpriteRenderer ballRenderer = GetComponent<SpriteRenderer>();
        widthBall = ballRenderer.bounds.size.x/2;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !boolFire) // Kiểm tra xem người dùng đã nhấn chuột trái chưa
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Lấy vị trí của chuột trong không gian thế giới
            if(mousePosition.y > aimingLine.minLine && mousePosition.y < aimingLine.maxLine){
                checkClickDown = true;
                ballCollider.isArrange = false;
            }
        }
        if (Input.GetMouseButtonUp(0) && !boolFire && checkClickDown) // Kiểm tra xem người dùng đã nhấn chuột trái chưa
        {
            checkClickDown = true;
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Lấy vị trí của chuột trong không gian thế giới
            if(mousePosition.y > aimingLine.minLine && mousePosition.y < aimingLine.maxLine){
                boolFire = true;// bóng đã đc bắn đi
                lostGame.intMaxBall -= 1;
                pointLine = 1;
            }
        }
        if(boolFire){
            Vector3 targetPoint = aimingLine.pointHit[pointLine];
            // transform.position = Vector3.Lerp(transform.position, targetPoint, Time.deltaTime * ballSpeed);
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, ballSpeed * Time.deltaTime);

            // Kiểm tra nếu quả bóng đã đến gần điểm đích, thì di chuyển tiếp tới point tiếp theo
            if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
            {
                if(pointLine < aimingLine.pointHit.Length - 1){
                    if(PlayerPrefs.GetInt("Stask", -1) == 0){
                        int pass = PlayerPrefs.GetInt("pass", -1);
                        pass++;
                        PlayerPrefs.SetInt("pass", pass);
                    }
                    transform.position = targetPoint;
                    pointLine++;
                }
            }
        }
    }
    void TurnOffScript() {
        enabled = false;
    }
}
