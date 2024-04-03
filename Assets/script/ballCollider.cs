using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballCollider : MonoBehaviour
{
    public static bool isArrange = false; // Check va chạm
    public static bool daVacham = false; // Check va chạm công khai
    // Start is called before the first frame update
    void Start(){
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "ballFire" && !isArrange){
            other.transform.position = transform.position;
            isArrange = true;
            fireBall.boolFire = false;
            // Kiểm tra xem GameObject đã có Rigidbody2D chưa
            Rigidbody2D rb2D = other.GetComponent<Rigidbody2D>();
            if (rb2D == null){
                // Nếu không có, thêm Rigidbody2D vào GameObject
                rb2D = other.gameObject.AddComponent<Rigidbody2D>();
                rb2D.gravityScale = 0f;
                other.gameObject.GetComponent<Collider2D>().isTrigger = true;
            }
            daVacham = true;
            enabled = false;
            creatBall.isCollider = true;
            gameObject.SetActive(false);
            // Gọi hàm tắt script
            other.SendMessage("TurnOffScript");
            other.gameObject.tag = "ballMap";
        }
        if((other.gameObject.tag == "ballBoom" || other.gameObject.tag == "ballLine" || other.gameObject.tag == "ballLaze" || other.gameObject.tag == "ballRainbow") && !isArrange){
            buttonBoom.boolBoom = true;
            isArrange = true;
            fireBall.boolFire = false;
            creatBall.isCollider = true;
            gameObject.SetActive(false);
            ghiban.checkGhiban = true;
            foreach (GameObject ballMap in GameObject.FindGameObjectsWithTag("boomMap")){
                Destroy(ballMap);
            }
            Destroy(other.gameObject);
            other.gameObject.tag = "ballMap";
        }
    }
}
