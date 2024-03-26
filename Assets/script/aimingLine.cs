using UnityEngine;

public class aimingLine : MonoBehaviour
{
    public Transform startPoint;
    public LineRenderer lineRenderer;
    public static float limitLine; // độ cao tối thiểu quả bóng đc bắn
    public bool checkClick = false;
    public SpriteRenderer ballRenderer;

    void Start(){
        lineRenderer = GetComponent<LineRenderer>();
        startPoint = GetComponent<Transform>();
        // chiều rộng của bóng
        float widthBall = ballRenderer.bounds.size.x;
        limitLine = startPoint.position.y + widthBall *1.5f;
        Debug.Log(limitLine);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Kiểm tra xem người dùng đã nhấn chuột trái chưa
        {
            checkClick = true;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Lấy vị trí của chuột trong không gian thế giới
            if(mousePosition.y > limitLine){
                // Kiểm tra xem có GameObject nào có tag "ballFire" không
                GameObject ballFireObject = GameObject.FindWithTag("ballFire");

                if(ballFireObject != null){
                    // Bật hiển thị của line renderer
                    lineRenderer.enabled = true;
                    SpriteRenderer spriteBall = ballFireObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
                    // Chuyển đổi chuỗi màu thành màu Color
                    Color color = GetColorFromString(spriteBall.sprite.name);
                    
                    // Thiết lập màu cho đường lineRenderer
                    lineRenderer.startColor = color;
                    lineRenderer.endColor = color;
                }
            }
        }
        if(checkClick){
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            // Tính toán vector hướng từ điểm bắt đầu đến vị trí chuột
            Vector2 direction = (mousePosition - startPoint.position).normalized;
            
            // Tạo một ray từ vị trí của đối tượng đến hướng bên phải
            RaycastHit2D hit = Physics2D.Raycast(startPoint.position, direction,9999f, ~LayerMask.GetMask(new string[]{"ballFire"})); 

            lineRenderer.enabled = true;

            //Thiết lập đỉnh của đường thẳng
            if (mousePosition.y >= limitLine){
                // Kiểm tra xem ray có chạm vào một đối tượng không
                if (hit.collider != null){
                    lineRenderer.SetPosition(0, startPoint.position);
                    lineRenderer.SetPosition(1, hit.point);
                    // Phản chiếu hướng của đường thẳng
                    if(hit.collider.tag == "vienMH"){
                        Vector2 reflectedDirection = Vector2.Reflect(direction, hit.normal);
                        RaycastHit2D hitPX = Physics2D.Raycast(hit.point, reflectedDirection);
                        lineRenderer.positionCount = 3;
                        lineRenderer.SetPosition(2, hitPX.point);
                    }
                    else{
                        lineRenderer.positionCount = 2;
                    }
                }
            }
            else{
                lineRenderer.enabled = false;
            }
        }
        if (Input.GetMouseButtonUp(0)) // Kiểm tra xem người dùng đã nhấn chuột trái chưa
        {
            checkClick = false;
            lineRenderer.enabled = false;
        }
    }
    Color GetColorFromString(string colorName)
    {
        switch (colorName)
        {
            case "ballRed":
                return Color.red;
            case "ballBlue":
                return Color.blue;
            case "ballGreen":
                return Color.green;
            case "ballYellow":
                return Color.yellow;
            // Thêm các màu khác nếu cần
            default:
                return Color.white; // Màu mặc định
        }
    }
}
