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
        overlapRadius = widthBall/2;
        // Lấy màu sắc của ball
        colorBall = transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
    }
    public void _ghiban(bool check){
        if (isProcessed)
            return; // Nếu đã xử lý thì không cần làm gì nữa

        // Đánh dấu là đã xử lý
        isProcessed = true;

        // Lấy tất cả các Collider2D nằm trong bán kính nhất định
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, overlapRadius);
        checkSoluong = 0;
        if(!check){
            // Duyệt qua từng Collider2D và in ra thông tin của GameObjects
            foreach (Collider2D collider in colliders)
            {
                // Kiểm tra xem GameObject
                if (collider.gameObject != gameObject && (collider.tag == "ballMap" || collider.tag == "ballIce")){
                    // Kiểm tra màu sắc của gameObject va chạm
                    objectColor = collider.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
                    if (objectColor.EndsWith("Ice"))
                    {
                        // Nếu có, loại bỏ "Ice" từ phần tử
                        objectColor = objectColor.Substring(0, objectColor.Length - 3);
                    }
                    if (objectColor == colorBall){
                        checkSoluong++;
                    }
                }
            }
        }
        if(checkSoluong >= 2){
            foreach (Collider2D collider in colliders){
                // Kiểm tra xem GameObject
                if (collider.gameObject != gameObject && (collider.tag == "ballMap" || collider.tag == "ballIce")){
                    // Kiểm tra màu sắc của gameObject va chạm
                    objectColor = collider.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
                    if (objectColor.EndsWith("Ice"))
                    {
                        // Nếu có, loại bỏ "Ice" từ phần tử
                        objectColor = objectColor.Substring(0, objectColor.Length - 3);
                    }
                    if (colorBall.EndsWith("Ice"))
                    {
                        // Nếu có, loại bỏ "Ice" từ phần tử
                        colorBall = colorBall.Substring(0, colorBall.Length - 3);
                    }
                    if (objectColor == colorBall){
                        if(collider.gameObject.GetComponent<ghiban>().isProcessed){
                            if(collider.tag == "ballIce"){
                                SetPosition.ballIces = SetPosition.ballIces.Concat(new[] { collider.gameObject }).ToArray();
                                collider.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = collider.GetComponent<SpriteRenderer>().sprite;
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
        else{
            if(check){
                foreach (Collider2D collider in colliders){
                    // Kiểm tra xem GameObject
                    if (collider.gameObject != gameObject && (collider.tag == "ballMap" || collider.tag == "ballIce")){
                        // Kiểm tra màu sắc của gameObject va chạm
                        objectColor = collider.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
                        if (objectColor.EndsWith("Ice"))
                        {
                            // Nếu có, loại bỏ "Ice" từ phần tử
                            objectColor = objectColor.Substring(0, objectColor.Length - 3);
                        }
                        if (colorBall.EndsWith("Ice"))
                        {
                            // Nếu có, loại bỏ "Ice" từ phần tử
                            colorBall = colorBall.Substring(0, colorBall.Length - 3);
                        }
                        if (objectColor == colorBall){
                            if(collider.gameObject.GetComponent<ghiban>().isProcessed){
                                if(collider.gameObject.GetComponent<ghiban>().isProcessed){
                                    if(collider.tag == "ballIce"){
                                        SetPosition.ballIces = SetPosition.ballIces.Concat(new[] { collider.gameObject }).ToArray();
                                        collider.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = collider.GetComponent<SpriteRenderer>().sprite;
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
                if(gameObject.GetComponent<ghiban>().isProcessed){
                    if(gameObject.tag == "ballIce"){
                        SetPosition.ballIces = SetPosition.ballIces.Concat(new[] { gameObject }).ToArray();
                        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
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
        }
        SetPosition.ballIces = new GameObject[0];
    }
    void resetCheck(){
        isProcessed = false;
    }
}