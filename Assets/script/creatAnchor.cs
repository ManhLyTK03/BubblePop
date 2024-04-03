using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creatAnchor : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    public string[] tagsToSearch; // Mảng các tag cần tìm
    void Start(){
        // Lấy BoxCollider2D từ GameObject hiện tại
        boxCollider = GetComponent<BoxCollider2D>();
        tagsToSearch = new string[]{"ballMap", "ballStone"};
    }
    void Update(){
        if(ghiban.checkGhiban){
            setConnect();
        }
    }
    void setConnect(){
        ghiban.checkGhiban = false;
        // Duyệt qua mỗi tag trong mảng tagsToSearch
        foreach (string tagToSearch in tagsToSearch){
            // Tìm tất cả các game object có tag là "ball"
            GameObject[] ballMaps = GameObject.FindGameObjectsWithTag(tagToSearch);
            // reset connect ballMap
            foreach (GameObject ball in ballMaps)
            {
                ball.GetComponent<ballFall>().checkConnect = 0;
            }
        }
        setAnchor();
    }
    public void setAnchor(){
        // Tạo một mảng chứa tất cả các GameObject va chạm với BoxCollider2D
        Collider2D[] colliders = Physics2D.OverlapBoxAll(boxCollider.bounds.center, boxCollider.bounds.size, 0f);
        bool boolWin = true;
        // duyệt qua các gameObject va chạm
        foreach (Collider2D collider in colliders){
            if(collider.gameObject.tag == "ballMap"){
                boolWin = false;
            }
            // Duyệt qua mỗi tag trong mảng tagsToSearch
            foreach (string tagToSearch in tagsToSearch){
                if (collider.gameObject.CompareTag(tagToSearch)){
                    collider.gameObject.GetComponent<ballFall>()._checkConnect();
                }
            }
        }
        if(boolWin){
            mapRandom.checkWin = true;
        }
    }
}