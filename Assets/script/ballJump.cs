using UnityEngine;
using System.Linq;
using System.Collections;

public class ballJump : MonoBehaviour
{
    public float jumpForceMin = 10f; // Lực bật lên tối thiểu
    public float jumpForceMax = 20f; // Lực bật lên tối đa
    public float jumpForce = 10f; // Lực bật lên
    public float maxAngleVariation = 30f; // Biến thể góc tối đa
    private Rigidbody2D rb;
    public float fadeDuration = 1f; // Thời gian để hiệu ứng biến mất
    void Start(){
        Invoke("setPosition", 0.01f);
    }
    void setPosition(){
        
        // Lấy danh sách các BoxCollider2D trong GameObject này
        BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
        colliders[0].offset = new Vector2(-gameObject.transform.localScale.x/2, colliders[0].offset.y);
        colliders[1].offset = new Vector2(gameObject.transform.localScale.x/2, colliders[0].offset.y);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ballFall") || collision.gameObject.CompareTag("ballMap"))
        {
            rb = collision.gameObject.GetComponent<Rigidbody2D>();

            float ballBounciness = 0.2f; // hệ số đàn hồi

            // Lấy vận tốc của quả bóng khi va chạm
            Vector2 relativeVelocity = collision.relativeVelocity;

            // Tính toán lực nảy dựa trên vận tốc và hệ số đàn hồi
            Vector2 bounceForce = -relativeVelocity * ballBounciness;

            // Áp dụng lực nảy lên quả bóng
            rb.AddForce(bounceForce, ForceMode2D.Impulse);
        }
    }
}
