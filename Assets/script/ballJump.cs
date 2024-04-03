using UnityEngine;
using System.Collections;

public class ballJump : MonoBehaviour
{
    public float jumpForceMin = 10f; // Lực bật lên tối thiểu
    public float jumpForceMax = 20f; // Lực bật lên tối đa
    public float jumpForce = 10f; // Lực bật lên
    public float maxAngleVariation = 30f; // Biến thể góc tối đa
    private Rigidbody2D rb;
    public float fadeDuration = 1f; // Thời gian để hiệu ứng biến mất

    void Start()
    {
        // Lấy ra collider của đối tượng hiện tại
        Collider2D currentCollider = GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground")) // Kiểm tra va chạm với đối tượng được gán tag là "Ground"
        {
            rb = GetComponent<Rigidbody2D>();
            // Lựa chọn một lực bật lên ngẫu nhiên cho quả bóng
            float randomJumpForce = Random.Range(jumpForceMin, jumpForceMax);
            // Tạo một góc bật lên ngẫu nhiên
            float randomAngle = Random.Range(-maxAngleVariation, maxAngleVariation);
            // Chuyển đổi góc thành vector hướng
            Vector2 jumpDirection = Quaternion.Euler(0, 0, randomAngle) * Vector2.up;
            // Áp dụng lực bật lên theo hướng này
            rb.AddForce(jumpDirection * randomJumpForce, ForceMode2D.Impulse);
            Invoke("destroyBall", 1f);
        }
    }
    void destroyBall(){
        Destroy(gameObject);
    }
}
