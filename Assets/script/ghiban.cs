using UnityEngine;
using System.Linq;

public class ghiban : MonoBehaviour
{
    public float overlapRadius; // Bán kính để tìm các GameObject khác va chạm với "ball"

    private string colorBall;
    public float widthBall;
    public SpriteRenderer ballRenderer;
    public static bool checkGhiban = false;
    public string objectColor;
    public int checkSoluong;

    private bool isProcessed = false; // Biến cờ để đánh dấu đã xử lý
    void Start(){
        ballRenderer = GetComponent<SpriteRenderer>();
        // chiều rộng của bóng
        widthBall = ballRenderer.bounds.size.x;
        overlapRadius = (widthBall/2)*1.5f;
        // Lấy màu sắc của ball
        colorBall = transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
    }
    public int _checkGhiban(){
        // Lấy tất cả các Collider2D nằm trong bán kính nhất định
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, overlapRadius);
        int soluong = 0;
        // Duyệt qua từng Collider2D và in ra thông tin của GameObjects
        foreach (Collider2D collider in colliders)
        {
            // Kiểm tra xem GameObject
            if (collider.gameObject != gameObject && (collider.tag == "ballMap" || collider.tag == "ballIce")){
                if(collider.gameObject.transform.position.y < ballBoom.maxCeiling){
                    // Kiểm tra màu sắc của gameObject va chạm
                    objectColor = collider.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
                    if (objectColor == colorBall){
                        soluong++;
                    }
                }
            }
        }
        return soluong;
    }
    public void _ghiban(bool check){
        if (isProcessed)
            return; // Nếu đã xử lý thì không cần làm gì nữa

        // Đánh dấu là đã xử lý
        isProcessed = true;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, overlapRadius);
        if(!check){
            checkSoluong = 0;
            checkSoluong = _checkGhiban();
        }
        
        if(checkSoluong >= 2){
            foreach (Collider2D collider in colliders){
                // Kiểm tra xem GameObject
                if (collider.gameObject != gameObject && (collider.tag == "ballMap" || collider.tag == "ballIce")){
                    if(collider.gameObject.transform.position.y < ballBoom.maxCeiling){
                        // Kiểm tra màu sắc của gameObject va chạm
                        objectColor = collider.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
                        if (objectColor == colorBall){
                            if(collider.gameObject.GetComponent<ghiban>().isProcessed){
                                if(collider.tag == "ballIce"){
                                    if (!SetPosition.ballIces.Contains(collider.gameObject)){
                                        SetPosition.ballIces = SetPosition.ballIces.Concat(new[] { collider.gameObject }).ToArray();
                                    }
                                }
                                else{
                                    Destroy(collider.gameObject);
                                }
                            }
                            else{
                                collider.gameObject.GetComponent<ghiban>()._ghiban(true);
                            }
                        }
                    }
                }
            }
        }
        else{
            if(check){
                foreach (Collider2D collider in colliders){
                    // Kiểm tra xem GameObject
                    if (collider.gameObject != gameObject && (collider.tag == "ballMap" || collider.tag == "ballIce")){
                        if(collider.gameObject.transform.position.y < ballBoom.maxCeiling){
                            // Kiểm tra màu sắc của gameObject va chạm
                            objectColor = collider.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
                            if (objectColor == colorBall){
                                if(collider.gameObject.GetComponent<ghiban>().isProcessed){
                                    if(collider.gameObject.GetComponent<ghiban>().isProcessed){
                                        if(collider.tag == "ballIce"){
                                            if (!SetPosition.ballIces.Contains(collider.gameObject)){
                                                SetPosition.ballIces = SetPosition.ballIces.Concat(new[] { collider.gameObject }).ToArray();
                                            }
                                        }
                                        else{
                                            Destroy(collider.gameObject);
                                        }
                                    }
                                }
                                else{
                                    collider.gameObject.GetComponent<ghiban>()._ghiban(true);
                                }
                            }
                        }
                    }
                }
                if(gameObject.GetComponent<ghiban>().isProcessed){
                    if(gameObject.tag == "ballIce"){
                        if (!SetPosition.ballIces.Contains(gameObject)){
                            SetPosition.ballIces = SetPosition.ballIces.Concat(new[] { gameObject }).ToArray();
                        }
                    }
                    else{
                        Destroy(gameObject);
                    }
                }
            }
        }
        checkGhiban = true;
        Invoke("setBallIce", 0.1f);
        Invoke("resetCheck", 0.5f);
    }
    void setBallIce(){
        foreach (GameObject ballIce in SetPosition.ballIces){
            ballIce.tag = "ballMap";
            // Xóa phần tử con thứ hai
            DestroyImmediate(ballIce.gameObject.transform.GetChild(1).gameObject);
        }
        SetPosition.ballIces = new GameObject[0];
    }
    void resetCheck(){
        isProcessed = false;
    }
}