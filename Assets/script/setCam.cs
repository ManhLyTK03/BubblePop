using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setCam : MonoBehaviour
{
    private float widthBall;
    public Camera mainCamera;
    public SpriteRenderer ballRenderer;
    public int row;
    // Start is called before the first frame update
    void Start()
    {
        // chiều rộng của bóng
        widthBall = ballRenderer.bounds.size.x;
        mainCamera = Camera.main;
        mainCamera.orthographicSize = (widthBall * Screen.height / Screen.width * 0.5f) * row;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
