using UnityEngine;
using UnityEngine.EventSystems;

public class fireBall : MonoBehaviour
{
    public float ballSpeed = 5f; // Tốc độ của quả bóng
    public static bool boolFire = false;    
    private Vector3 direction;
    private float leftEdgeX,rightEdgeX;
    private float withBall;

    void Start(){
        // Lấy ra chiều rộng và chiều cao của màn hình
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        // Lấy ra tọa độ của cạnh trái màn hình trong không gian thế giới
        Vector3 leftEdge = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        // Lấy ra tọa độ của cạnh phải màn hình trong không gian thế giới
        Vector3 rightEdge = Camera.main.ScreenToWorldPoint(new Vector3(screenWidth, 0, Camera.main.nearClipPlane));
        leftEdgeX = leftEdge.x;
        rightEdgeX = rightEdge.x;
        // chiều rộng của bóng
        SpriteRenderer ballRenderer = GetComponent<SpriteRenderer>();
        withBall = ballRenderer.bounds.size.x/2;
    }
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && !boolFire) // Kiểm tra xem người dùng đã nhấn chuột trái chưa
        {
            // Kiểm tra xem chuột có đang trên một button không
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Lấy vị trí của chuột trong không gian thế giới
                
                boolFire = true;// bóng đã đc bắn đi
                // Tính toán vector hướng từ vị trí hiện tại của quả bóng đến vị trí của chuột
                direction = mousePosition - transform.position;
                direction.z = 0f;
                direction.Normalize(); // Chuẩn hóa vector hướng để có độ dài là 1
            }
        }
        if(boolFire){
            // Di chuyển quả bóng theo hướng của chuột với tốc độ được chỉ định
            transform.Translate(direction * ballSpeed * Time.deltaTime);
            
            // Kiểm tra nếu quả bóng chạm vào cạnh trái của màn hình
            if (transform.position.x <= leftEdgeX + withBall){
                // Đảo ngược hướng di chuyển của quả bóng
                direction.x = Mathf.Abs(direction.x);
            }
            // Kiểm tra nếu quả bóng chạm vào cạnh phải của màn hình
            if (transform.position.x >= rightEdgeX - withBall){
                // Đảo ngược hướng di chuyển của quả bóng
                direction.x = -Mathf.Abs(direction.x);
            }
        }
    }
    void TurnOffScript() {
        enabled = false;
    }
}
