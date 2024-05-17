using UnityEngine;
using System.Linq;
using System.Collections;

public class ghiban : MonoBehaviour
{
    public float overlapRadius; // Bán kính để tìm các GameObject khác va chạm với "ball"

    private string colorBall;
    public float widthBall;
    public SpriteRenderer ballRenderer;
    public static bool checkGhiban;
    public string objectColor;
    public int checkSoluong;

    private bool isProcessed = false; // Biến cờ để đánh dấu đã xử lý
    void Start()
    {
        ballRenderer = GetComponent<SpriteRenderer>();
        // chiều rộng của bóng
        widthBall = ballRenderer.bounds.size.x;
        overlapRadius = (widthBall / 2) * 1.5f;
    }
    bool _checkIsProcessed()
    {
        return isProcessed;
    }
    public void _ghiban()
    {
        if (isProcessed)
            return; // Nếu đã xử lý thì không cần làm gì nữa

        // Đánh dấu là đã xử lý
        isProcessed = true;
        // Lấy màu sắc của ball
        colorBall = transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, overlapRadius);
        foreach (Collider2D collider in colliders)
        {
            // Kiểm tra xem GameObject
            if (collider.gameObject != gameObject && (collider.tag == "ballMap" || collider.tag == "ballIce"))
            {
                if (collider.gameObject.transform.position.y < ballBoom.maxCeiling)
                {
                    // Kiểm tra màu sắc của gameObject va chạm
                    objectColor = collider.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
                    if (objectColor == colorBall)
                    {
                        collider.gameObject.GetComponent<ghiban>()._ghiban();
                    }
                }
            }
        }
        AddGameObject(gameObject);
        Invoke("resetCheck", 0.1f);
    }
    // Phương thức để thêm GameObject vào mảng (nếu chưa tồn tại)
    public void AddGameObject(GameObject obj)
    {
        if (!SetPosition.ballDestroys.Contains(obj))
        {
            SetPosition.ballDestroys.Add(obj);
        }
    }
    void resetCheck()
    {
        isProcessed = false;
    }

}