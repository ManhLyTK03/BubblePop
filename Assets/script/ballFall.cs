using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballFall : MonoBehaviour
{
    public int checkConnect = 1;
    public float overlapRadius;
    private bool isProcessed = false; // Biến cờ để đánh dấu đã xử lý
    public string[] tagsToSearch; // Mảng các tag cần tìm
    
    void Start(){
        // chiều rộng của bóng
        SpriteRenderer ballRenderer = GetComponent<SpriteRenderer>();
        overlapRadius = ballRenderer.bounds.size.x/2;
        tagsToSearch = new string[]{"ballMap", "ballStone","ballHole", "ballIce"};
    }
    public void _checkConnect()
    {
        if (isProcessed)
            return; // Nếu đã xử lý thì không cần làm gì nữa

        // Đánh dấu là đã xử lý
        isProcessed = true;
        checkConnect = 1;
        // Lấy tất cả các Collider2D nằm trong bán kính nhất định
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, overlapRadius*1.2f);
        // Duyệt qua từng Collider2D
        foreach (Collider2D collider in colliders){
            foreach (string tagToSearch in tagsToSearch){
                if(collider.gameObject.CompareTag(tagToSearch)){
                    collider.gameObject.GetComponent<ballFall>()._checkConnect();
                }
            }
        }
        Invoke("resetCheck", 0.01f);
    }
    void resetCheck(){
        isProcessed = false;
        foreach (string tagToSearch in tagsToSearch){
            // Tìm tất cả các game object có tag là "ballMap"
            GameObject[] ballMaps = GameObject.FindGameObjectsWithTag(tagToSearch);
            
            foreach (GameObject ball in ballMaps)
            {
                if(ball.GetComponent<ballFall>().checkConnect == 0){
                    DisableAllScripts(ball);
                    ball.tag = "ballFall";
                    ball.layer = LayerMask.NameToLayer("ballFall");
                    // Tắt collider trigger
                    ball.GetComponent<Collider2D>().isTrigger = false;
                    ball.layer = LayerMask.NameToLayer("ballFall");
                    Rigidbody2D ballRB = ball.GetComponent<Rigidbody2D>();
                    ballRB.gravityScale = 1f;
                    // Gán vận tốc và hướng rơi ban đầu cho quả bóng
                    ballRB.velocity = new Vector2(Random.Range(-1f,1f),Random.Range(0f,1f)).normalized * Random.Range(0f,2f);
                }
            }
        }
    }
    // Hàm để tắt tất cả các script trên một GameObject
    private void DisableAllScripts(GameObject obj)
    {
        MonoBehaviour[] scripts = obj.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            // Kiểm tra xem script có phải là MonoBehaviour không (để không tắt những script cần thiết)
            if (script.GetType() != typeof(Transform))
            {
                script.enabled = false;
            }
        }
    }
}
