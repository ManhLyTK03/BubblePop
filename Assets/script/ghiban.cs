// using UnityEngine;

// public class ghiban : MonoBehaviour
// {
//     public float overlapRadius = 0.2f; // Bán kính để tìm các GameObject khác va chạm với "ball"

//     private Color colorBall;

//     public void _ghiban(){
//         // Lấy màu sắc của ball
//         colorBall = GetComponent<SpriteRenderer>().color;
//         // Lấy tất cả các Collider2D nằm trong bán kính nhất định
//         Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, overlapRadius);

//         // Duyệt qua từng Collider2D và in ra thông tin của GameObjects
//         foreach (Collider2D collider in colliders)
//         {
//             // Kiểm tra xem GameObject
//             if (collider.gameObject != gameObject && !collider.gameObject.CompareTag("vienMH")){
//                 // Kiểm tra màu sắc của gameObject va chạm
//                 Debug.Log("object" + collider);
//                 Color objectColor = collider.gameObject.GetComponent<SpriteRenderer>().color;
//                 Debug.Log("color" + objectColor);
//                 if (colorBall.Equals(objectColor)){
//                     // collider.gameObject.GetComponent<ghiban>()._ghiban();
//                     // Destroy(gameObject);
//                     // Destroy(collider.gameObject);
//                     gameObject.DestroyImmediate();
//                     collider.gameObject.DestroyImmediate();
//                 }
//             }
//         }
//         enabled = false;
//     }
// }
using UnityEngine;

public class ghiban : MonoBehaviour
{
    public float overlapRadius = 0.25f; // Bán kính để tìm các GameObject khác va chạm với "ball"

    private Color colorBall;

    private bool isProcessed = false; // Biến cờ để đánh dấu đã xử lý

    public void _ghiban(){
        if (isProcessed)
            return; // Nếu đã xử lý thì không cần làm gì nữa

        // Đánh dấu là đã xử lý
        isProcessed = true;

        // Lấy màu sắc của ball
        colorBall = GetComponent<SpriteRenderer>().color;
        // Lấy tất cả các Collider2D nằm trong bán kính nhất định
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, overlapRadius);

        // Duyệt qua từng Collider2D và in ra thông tin của GameObjects
        foreach (Collider2D collider in colliders)
        {
            // Kiểm tra xem GameObject
            if (collider.gameObject != gameObject && !collider.gameObject.CompareTag("vienMH")){
                // Kiểm tra màu sắc của gameObject va chạm
                Debug.Log("object" + collider);
                Color objectColor = collider.gameObject.GetComponent<SpriteRenderer>().color;
                Debug.Log("color" + objectColor);
                if (colorBall.Equals(objectColor)){
                    collider.gameObject.GetComponent<ghiban>()._ghiban();
                }
            }
        }
        Destroy(gameObject);
        enabled = false;
        Invoke("resetCheck", 0.5f);
    }
    void resetChec(){
        Destroy(gameObject);
        isProcessed = false;
    }
}
