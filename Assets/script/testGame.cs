using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testGame : MonoBehaviour
{
    public Transform ball;
    void Start(){
        float leftEdgeX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        ball.position = new Vector3(leftEdgeX, ball.position.y, ball.position.z);
    }
}
