using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosition : MonoBehaviour
{
    public Transform wallLeft, wallRight, wallBottom;
    public SpriteRenderer ballRenderer;
    public static GameObject[] ballIces;

    // Start is called before the first frame update
    void Start()
    {
        ballIces = new GameObject[0];
        // chiều rộng của bóng
        float widthBall = ballRenderer.bounds.size.x;

        // Cập nhật kích thước của wallLeft
        wallLeft.localScale = new Vector3(widthBall, wallLeft.localScale.y, wallLeft.localScale.z);
        float leftEdgeX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        wallLeft.position = new Vector3(leftEdgeX, wallLeft.position.y, wallLeft.position.z);
        // Cập nhật kích thước của wallRight
        wallRight.localScale = new Vector3(widthBall, wallRight.localScale.y, wallRight.localScale.z);
        float rightEdgeX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        wallRight.position = new Vector3(rightEdgeX, wallRight.position.y, wallRight.position.z);
        float bottomEdgeY = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
        // Cập nhật kích thước của wallBottom
        wallBottom.position = new Vector3(wallBottom.position.x, bottomEdgeY, wallBottom.position.z);
    }
}
