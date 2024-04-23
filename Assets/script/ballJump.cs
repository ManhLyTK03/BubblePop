using UnityEngine;
using System.Linq;
using System.Collections;

public class ballJump : MonoBehaviour
{
    public Rigidbody2D rb;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ballFall") || collision.gameObject.CompareTag("ballMap"))
        {
            rb = collision.gameObject.GetComponent<Rigidbody2D>();

            float ballBounciness = 0.5f; // hệ số đàn hồi

            // Lấy vận tốc của quả bóng khi va chạm
            Vector2 relativeVelocity = collision.relativeVelocity;

            // Tính toán lực nảy dựa trên vận tốc và hệ số đàn hồi
            Vector2 bounceForce = -relativeVelocity * ballBounciness;

            // Áp dụng lực nảy lên quả bóng
            rb.AddForce(bounceForce, ForceMode2D.Impulse);
        }
    }
}
