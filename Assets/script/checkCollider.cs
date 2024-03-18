using UnityEngine;

public class CheckCollider : MonoBehaviour
{
    public float overlapRadius = 0.25f; // Bán kính để tìm các GameObject khác va chạm với "ball"

    private Color colorBall;

    void Update()
    {
        if(ballCollider.daVacham){
            ballCollider.daVacham = false;
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
                    Color objectColor = collider.gameObject.GetComponent<SpriteRenderer>().color;
                    if (colorBall.Equals(objectColor)){
                        collider.gameObject.GetComponent<ghiban>()._ghiban();
                    }
                }
            }
            Destroy(gameObject);
            enabled = false;
        }
    }
}
