using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setPositionMap : MonoBehaviour
{
    public Transform[] groundHoles;
    public Transform wallCeiling;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("_setPosition", 0.01f);
    }
    void _setPosition(){
        // Cập nhật kích thước của wallBottom
        float bottomEdgeY = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
        int i = 0 ;
        float leftEdgeX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        float screenWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x * 2;
        // Cập nhật kích thước của wallCeiling
        float topEdgeY = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        wallCeiling.position = new Vector3(wallCeiling.position.x, topEdgeY, wallCeiling.position.z);
        // Dùng vòng lặp để điều chỉnh offset của từng BoxCollider2D
        foreach (Transform groundHole in groundHoles)
        {
            groundHole.position = new Vector3(leftEdgeX + i*screenWidth/5f, bottomEdgeY + 0.5f, groundHole.position.z);
            i++;
        }
    }
}
