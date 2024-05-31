using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ballCollider : MonoBehaviour
{
    public static bool isArrange = false; // Check va chạm
    public static bool daVacham = false; // Check va chạm công khai
    public float timeDestroy = 2f;
    public float timeDelay = 0.02f;
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "ballFire" && !isArrange){
            other.transform.position = transform.position;
            other.gameObject.layer = LayerMask.NameToLayer("hitCollider");
            isArrange = true;
            // Kiểm tra xem GameObject đã có Rigidbody2D chưa
            Rigidbody2D rb2D = other.GetComponent<Rigidbody2D>();
            if (rb2D == null){
                // Nếu không có, thêm Rigidbody2D vào GameObject
                rb2D = other.gameObject.AddComponent<Rigidbody2D>();
                rb2D.gravityScale = 0f;
                other.gameObject.GetComponent<CircleCollider2D>().radius = 0.5f;
                other.gameObject.GetComponent<Collider2D>().isTrigger = true;
            }
            SetPosition.ballDestroys.Clear();
            daVacham = true;
            enabled = false;
            gameObject.SetActive(false);
            // Gọi hàm tắt script
            other.SendMessage("TurnOffScript");
            other.gameObject.tag = "ballMap";
            other.transform.parent = GameObject.FindWithTag("mapRD").transform;
            Invoke("checkDestroy", 0.05f);
        }
        if((other.gameObject.tag == "ballBoom" || other.gameObject.tag == "ballLine" || other.gameObject.tag == "ballLaze" || other.gameObject.tag == "ballRainbow") && !isArrange){
            string tag = other.gameObject.tag;
            buttonBoom.boolBoom = true;
            if(tag == "ballBoom"){
                PlayerPrefs.SetInt("ballBoom", PlayerPrefs.GetInt("ballBoom", 0) - 1);
            }
            if(tag == "ballLine"){
                PlayerPrefs.SetInt("ballLine", PlayerPrefs.GetInt("ballLine", 0) - 1);
            }
            if(tag == "ballRainbow"){
                PlayerPrefs.SetInt("rainBow", PlayerPrefs.GetInt("rainBow", 0) - 1);
            }
            if(tag == "ballLaze"){
                PlayerPrefs.SetInt("ballLaze", PlayerPrefs.GetInt("ballLaze", 0) - 1);
            }
            isArrange = true;
            SetPosition.ballDestroys.Clear();
            ghiban.checkGhiban = true;
            gameObject.SetActive(false);
            GameObject[] ballMaps = GameObject.FindGameObjectsWithTag("boomMap");
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
            Score.intScore += ballMaps.Length*10*setCombo;
            foreach (GameObject ballMap in ballMaps){
                Destroy(ballMap);
                if(PlayerPrefs.GetInt("Stask", -1) == 3){
                    int pass = PlayerPrefs.GetInt("pass", -1);
                    pass++;
                    PlayerPrefs.SetInt("pass", pass);
                }
            }
            Destroy(other.gameObject);
            other.gameObject.tag = tag;
        }
    }
    void checkDestroy(){
        if(SetPosition.ballDestroys.Count >= 3){
            Debug.Log(SetPosition.ballDestroys.Count);
            timeDelay = timeDestroy/SetPosition.ballDestroys.Count;
            Invoke("endDestroy", timeDelay*SetPosition.ballDestroys.Count);
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
            // Sắp xếp các game object trong list theo khoảng cách tới targetPosition
            SetPosition.ballDestroys = SetPosition.ballDestroys.OrderBy(ball => Vector3.Distance(ball.transform.position, transform.position)).ToList();
            _Destroy();
        }
        else{
            ghiban.checkGhiban = true;
            Score.intCombo = 0;
            SetPosition.ballDestroys.Clear();
        }
    }
    void _Destroy(){
        int check = 0;
        while (SetPosition.ballDestroys.Count > 0)
        {
            if(SetPosition.ballDestroys[0].tag == "ballIce"){
                SetPosition.ballDestroys[0].tag = "ballMap";
                GameObject ballIceDestroy = SetPosition.ballDestroys[0].transform.GetChild(1).gameObject;
                Destroy(ballIceDestroy,timeDelay*check);
                SetPosition.ballDestroys.RemoveAt(0); // Xóa GameObject khỏi danh sách
            }
            else{
                Destroy(SetPosition.ballDestroys[0],timeDelay*check); // Hủy GameObject trước
                SetPosition.ballDestroys.RemoveAt(0); // Xóa GameObject khỏi danh sách
                if(PlayerPrefs.GetInt("Stask", -1) == 3){
                    int pass = PlayerPrefs.GetInt("pass", -1);
                    pass++;
                    PlayerPrefs.SetInt("pass", pass);
                }
            }
            check++;
        }
        SetPosition.ballDestroys.Clear();
    }
    public void endDestroy(){
        ghiban.checkGhiban = true;
        Debug.Log(SetPosition.ballDestroys.Count);
    }
}
