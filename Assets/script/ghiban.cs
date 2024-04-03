using UnityEngine;

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
                if (collider.gameObject != gameObject && collider.tag == "ballMap"){
                    // Kiểm tra màu sắc của gameObject va chạm
                    objectColor = collider.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
                    if (objectColor == colorBall){
                        checkSoluong++;
                    }
                }
            }
        }
        if(checkSoluong >= 2){
            foreach (Collider2D collider in colliders){
                // Kiểm tra xem GameObject
                if (collider.gameObject != gameObject && collider.tag == "ballMap"){
                    // Kiểm tra màu sắc của gameObject va chạm
                    objectColor = collider.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
                    if (objectColor == colorBall){
                        if(collider.gameObject.GetComponent<ghiban>().isProcessed){
                            Destroy(collider.gameObject);
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
                    if (collider.gameObject != gameObject && collider.tag == "ballMap"){
                        // Kiểm tra màu sắc của gameObject va chạm
                        objectColor = collider.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
                        if (objectColor == colorBall){
                            if(collider.gameObject.GetComponent<ghiban>().isProcessed){
                                Destroy(collider.gameObject);
                            }
                            else{
                                collider.gameObject.GetComponent<ghiban>()._ghiban(true);
                            }
                        }
                    }
                }
                Destroy(gameObject);
            }
        }
        checkGhiban = true;
        Invoke("resetCheck", 0.5f);
    }
    void resetCheck(){
        isProcessed = false;
    }
}