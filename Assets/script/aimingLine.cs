using UnityEngine;

public class aimingLine : MonoBehaviour
{
    public Transform startPoint;
    public LineRenderer lineRenderer;
    public float limitLine; // độ cao tối thiểu quả bóng đc bắn
    public bool checkClick = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Kiểm tra xem người dùng đã nhấn chuột trái chưa
        {
            checkClick = true;
        }
        if(checkClick){
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            // Tính toán vector hướng từ điểm bắt đầu đến vị trí chuột
            Vector2 direction = (mousePosition - startPoint.position).normalized;
            
            // Tạo một ray từ vị trí của đối tượng đến hướng bên phải
            RaycastHit2D hit = Physics2D.Raycast(startPoint.position, direction,9999f, ~LayerMask.GetMask(new string[]{"ballFire"})); 

            // Kiểm tra xem ray có chạm vào một đối tượng không
            if (hit.collider != null)
            {
                //Thiết lập đỉnh của đường thẳng
                if (mousePosition.y >= limitLine){
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
        }
        if (Input.GetMouseButtonUp(0)) // Kiểm tra xem người dùng đã nhấn chuột trái chưa
        {
            checkClick = false;
        }
    }
}
