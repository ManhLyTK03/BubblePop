using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonBoom : MonoBehaviour
{
    public GameObject ballFire;
    public GameObject ballFireOld;
    public GameObject ballFireMau; //bóng bắn mẫu
    public Transform pointFire; // Điểm tạo bóng bắn
    public GameObject ballBoom; // bóng boom mẫu
    public GameObject newBallBoom; // bóng boom
    public static bool boolBoom; //xác định việc tạo bóng Boom
    public string[] tagsMap; // Mảng các tag cần tìm
    // Update is called once per frame
    public void Start(){
        boolBoom = true;
        tagsMap = new string[]{"ballHole", "ballStone","ballMap"};
    }
    public void creatBoom()
    {
        if(PlayerPrefs.GetInt("ballBoom", 0) > 0){
            if(boolBoom){
                ballFire = GameObject.FindGameObjectWithTag("ballFire");
                if(ballFire == null){
                    return;
                }
                newBallBoom = Instantiate(ballBoom, pointFire.position, Quaternion.identity); // Tạo bóng bắn mới
                newBallBoom.tag = "ballBoom";
                ballFireOld = ballFire;
                ballFire.SetActive(false);
            }
            else{
                ballFire = newBallBoom;
                Destroy(ballFire);
                ballFireOld.SetActive(true);
            }
            boolBoom = !boolBoom;
        }
    }
    public void creatLine()
    {
        if(PlayerPrefs.GetInt("ballLine", 0) > 0){
            if(boolBoom){
                ballFire = GameObject.FindGameObjectWithTag("ballFire");
                if(ballFire == null){
                    return;
                }
                newBallBoom = Instantiate(ballBoom, pointFire.position, Quaternion.identity); // Tạo bóng bắn mới
                newBallBoom.tag = "ballLine";
                ballFireOld = ballFire;
                ballFire.SetActive(false);
            }
            else{
                ballFire = newBallBoom;
                Destroy(ballFire);
                ballFireOld.SetActive(true);
            }
            boolBoom = !boolBoom;
        }
    }
    public void creatlaze()
    {
        if(PlayerPrefs.GetInt("ballLaze", 0) > 0){
            if(boolBoom){
                ballFire = GameObject.FindGameObjectWithTag("ballFire");
                if(ballFire == null){
                    return;
                }
                newBallBoom = Instantiate(ballBoom, pointFire.position, Quaternion.identity); // Tạo bóng bắn mới
                newBallBoom.tag = "ballLaze";
                ballFireOld = ballFire;
                ballFire.SetActive(false);
            }
            else{
                ballFire = newBallBoom;
                Destroy(ballFire);
                ballFireOld.SetActive(true);
            }
            boolBoom = !boolBoom;
        }
    }
    public void creatRainbow()
    {
        if(PlayerPrefs.GetInt("rainBow", 0) > 0){
            if(boolBoom){
                ballFire = GameObject.FindGameObjectWithTag("ballFire");
                if(ballFire == null){
                    return;
                }
                newBallBoom = Instantiate(ballBoom, pointFire.position, Quaternion.identity); // Tạo bóng bắn mới
                newBallBoom.tag = "ballRainbow";
                ballFireOld = ballFire;
                ballFire.SetActive(false);
            }
            else{
                ballFire = newBallBoom;
                Destroy(ballFire);
                ballFireOld.SetActive(true);
            }
            boolBoom = !boolBoom;
        }
    }
}
