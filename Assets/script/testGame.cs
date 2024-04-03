using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testGame : MonoBehaviour
{
    public float overlapRadius; // Bán kính để tìm các GameObject khác va chạm với "ball"

    private string colorBall;
    public float widthBall;
    public SpriteRenderer ballRenderer;
    void Start(){
        ballRenderer = GetComponent<SpriteRenderer>();
        // chiều rộng của bóng
        widthBall = ballRenderer.bounds.size.x;
        overlapRadius = widthBall/2;
    }
    void Update(){
    }
    void OnTriggerEnter2D(Collider2D other){
            Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag == "ballFire"){
            colorBall = GameObject.FindWithTag("ballFire").transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
            // Lấy tất cả các Collider2D nằm trong bán kính nhất định
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, overlapRadius);

            // Duyệt qua từng Collider2D và in ra thông tin của GameObjects
            foreach (Collider2D collider in colliders)
            {
                // Kiểm tra xem GameObject
                if(collider.gameObject.CompareTag("ballMap")){
                    // Kiểm tra màu sắc của gameObject va chạm
                    string objectColor = collider.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
                    if (objectColor == colorBall){
                        collider.gameObject.GetComponent<ghiban>()._ghiban(false);
                    }
                }
            }
        }
    }
}
