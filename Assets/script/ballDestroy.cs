using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballDestroy : MonoBehaviour
{
    private Rigidbody2D rb;
    // void OnTriggerEnter2D(Collider2D
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ballFall") || collision.gameObject.CompareTag("ballMap")) // Kiểm tra va chạm với đối tượng được gán tag là "Ground"
        {
            rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0f, 0f);
            Destroy(collision.gameObject, 1f);
        }
    }
}
