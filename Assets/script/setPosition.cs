using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosition : MonoBehaviour
{
    public Transform[] groundHoles;
    public Transform wallLeft, wallRight, wallBottom, wallCeiling;
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
        wallLeft.position = new Vector3(leftEdgeX - widthBall/2, wallLeft.position.y, wallLeft.position.z);
        // Cập nhật kích thước của wallRight
        wallRight.localScale = new Vector3(widthBall, wallRight.localScale.y, wallRight.localScale.z);
        float rightEdgeX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        wallRight.position = new Vector3(rightEdgeX + widthBall/2, wallRight.position.y, wallRight.position.z);
        // Cập nhật kích thước của wallCeiling
        float topEdgeY = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        wallCeiling.position = new Vector3(wallCeiling.position.x, topEdgeY, wallCeiling.position.z);
        ballBoom.maxCeiling = wallCeiling.position.y - wallCeiling.localScale.y/2 + widthBall/2;
        // Cập nhật kích thước của wallBottom
        float bottomEdgeY = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
        wallBottom.position = new Vector3(wallBottom.position.x, bottomEdgeY, wallBottom.position.z);
        int i = 0 ;
        float screenWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x * 2;
        // Dùng vòng lặp để điều chỉnh offset của từng BoxCollider2D
        foreach (Transform groundHole in groundHoles)
        {
            groundHole.localScale = new Vector3(screenWidth/5f, groundHole.localScale.y, groundHole.localScale.z);
            groundHole.position = new Vector3(leftEdgeX + groundHole.localScale.x/2f + groundHole.localScale.x*i, bottomEdgeY - groundHole.localScale.y/4f, groundHole.position.z);
            i++;
        }
    }
}
