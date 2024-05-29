using UnityEngine;
using System.Collections;

public class CheckCollider : MonoBehaviour
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
        if(ballCollider.daVacham){
            ballCollider.daVacham = false;
            // Lấy màu sắc của ball
            colorBall = transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
            // Lấy tất cả các Collider2D nằm trong bán kính nhất định
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, overlapRadius);
            // Duyệt qua từng Collider2D và in ra thông tin của GameObjects
            foreach (Collider2D collider in colliders)
            {
                if(collider.gameObject.CompareTag("ballHole")){
                    Destroy(gameObject);
                }
                else if(collider.gameObject.CompareTag("ballMap") || collider.gameObject.CompareTag("ballIce")){
                    // Kiểm tra màu sắc của gameObject va chạm
                    string objectColor = collider.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
                    if (objectColor == colorBall){
                        collider.gameObject.GetComponent<ghiban>()._ghiban();
                    }
                }
            }
            enabled = false;
            Invoke("checkDestroy", 0.1f);
        }
    }
    void checkDestroy(){
        if(SetPosition.ballDestroys.Count >= 3){
            Score.intCombo += 1;
            if(PlayerPrefs.GetInt("Stask", -1) == 2){
                int pass = PlayerPrefs.GetInt("pass", -1);
                if(pass < Score.intCombo){
                    pass = Score.intCombo;
                }
                PlayerPrefs.SetInt("pass", pass);
            }
            int setCombo = Score.intCombo;
            if(Score.intCombo > 5){
                setCombo = 5;
            }
            Score.intScore += (SetPosition.ballDestroys.Count-1)*10*setCombo;
            _Destroy();
        }
        else{
            ghiban.checkGhiban = true;
            Score.intCombo = 0;
            SetPosition.ballDestroys.Clear();
        }
    }
    void _Destroy(){
        while (SetPosition.ballDestroys.Count > 0)
        {
            if(SetPosition.ballDestroys[0].tag == "ballIce"){
                SetPosition.ballDestroys[0].tag = "ballMap";
                GameObject ballIceDestroy = SetPosition.ballDestroys[0].transform.GetChild(1).gameObject;
                Destroy(ballIceDestroy);
                SetPosition.ballDestroys.RemoveAt(0); // Xóa GameObject khỏi danh sách
            }
            else{
                Destroy(SetPosition.ballDestroys[0]); // Hủy GameObject trước
                SetPosition.ballDestroys.RemoveAt(0); // Xóa GameObject khỏi danh sách
                if(PlayerPrefs.GetInt("Stask", -1) == 3){
                    int pass = PlayerPrefs.GetInt("pass", -1);
                    pass++;
                    PlayerPrefs.SetInt("pass", pass);
                }
            }
        }
        SetPosition.ballDestroys.Clear();
        ghiban.checkGhiban = true;
    }
}