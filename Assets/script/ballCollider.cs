using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballCollider : MonoBehaviour
{
    public static bool isArrange = false; // Check va chạm
    public static bool daVacham = false; // Check va chạm công khai
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "ballFire" && !isArrange){
            other.transform.position = transform.position;
            isArrange = true;
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
            Invoke("setCreatBall", 0.3f);
            gameObject.SetActive(false);
            // Gọi hàm tắt script
            other.SendMessage("TurnOffScript");
            other.gameObject.tag = "ballMap";
            other.transform.parent = GameObject.FindWithTag("mapRD").transform;
        }
        if((other.gameObject.tag == "ballBoom" || other.gameObject.tag == "ballLine" || other.gameObject.tag == "ballLaze" || other.gameObject.tag == "ballRainbow") && !isArrange){
            string tag = other.gameObject.tag;
            buttonBoom.boolBoom = true;
            isArrange = true;
            daVacham = true;
            ghiban.checkGhiban = true;
            gameObject.SetActive(false);
            foreach (GameObject ballMap in GameObject.FindGameObjectsWithTag("boomMap")){
                Destroy(ballMap);
            }
            Invoke("setCreatBall", 0.3f);
            Destroy(other.gameObject);
            other.gameObject.tag = tag;
        }
    }
    void setCreatBall(){
        creatBall.isCreat = true;
    }
}
