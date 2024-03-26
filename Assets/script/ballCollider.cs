using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballCollider : MonoBehaviour
{
    public bool isArrange = false; // Check va chạm
    public static bool daVacham = false; // Check va chạm công khai
    public SpriteRenderer ballRenderer;
    public float distanceX;
    public float distanceY;
    public bool check;
    // Start is called before the first frame update
    void Start(){
        ballRenderer = GetComponent<SpriteRenderer>();
        distanceX = ballRenderer.bounds.size.x/2;
        distanceY = ballRenderer.bounds.size.y/2*Mathf.Sqrt(3f);
        check = true;
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "ballMap" && !isArrange){
            if(check){
                check = false;
                Debug.Log("sdfghjkl;lkjhgfdfghjkllkjhgfdfghjk " + this.gameObject.GetInstanceID()+" "+check);
                fireBall.boolFire = false;
                // Gọi hàm tắt script
                SendMessage("TurnOffScript");
                if(transform.position.y > other.transform.position.y && transform.position.x > other.transform.position.x){
                    transform.position = new Vector3(other.transform.position.x + distanceX, other.transform.position.y + distanceY, transform.position.z);
                }
                if(transform.position.y > other.transform.position.y && transform.position.x < other.transform.position.x){
                    transform.position = new Vector3(other.transform.position.x - distanceX, other.transform.position.y + distanceY, transform.position.z);
                }
                if(transform.position.y == other.transform.position.y && transform.position.x > other.transform.position.x){
                    transform.position = new Vector3(other.transform.position.x + 2f*distanceX, other.transform.position.y, transform.position.z);
                }
                if(transform.position.y == other.transform.position.y && transform.position.x < other.transform.position.x){
                    transform.position = new Vector3(other.transform.position.x - 2f*distanceX, other.transform.position.y, transform.position.z);
                }
                if(transform.position.y < other.transform.position.y && transform.position.x > other.transform.position.x){
                    transform.position = new Vector3(other.transform.position.x + distanceX, other.transform.position.y - distanceY, transform.position.z);
                }
                if(transform.position.y < other.transform.position.y && transform.position.x < other.transform.position.x){
                    transform.position = new Vector3(other.transform.position.x - distanceX, other.transform.position.y - distanceY, transform.position.z);
                }
                gameObject.tag = "ballMap";
                // Kiểm tra xem GameObject đã có Rigidbody2D chưa
                Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
                if (rb2D == null)
                {
                    // Nếu không có, thêm Rigidbody2D vào GameObject
                    rb2D = gameObject.AddComponent<Rigidbody2D>();
                    rb2D.gravityScale = 0f;
                    gameObject.GetComponent<Collider2D>().isTrigger = true;
                }
                isArrange = true;
                daVacham = true;
                enabled = false;
                creatBall.isCollider = true;
            }
        }
        if(other.gameObject.tag == "ground"){
            creatBall.isCollider = true;
            Destroy(gameObject);
            fireBall.boolFire = false;
        }
        if(other.gameObject.tag == "ceiling"){
            creatBall.isCollider = true;
            Destroy(gameObject);
            fireBall.boolFire = false;
        }
    }
}
