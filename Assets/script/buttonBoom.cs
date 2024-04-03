using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonBoom : MonoBehaviour
{
    public GameObject ballFire;
    public GameObject ballFireMau; //bóng bắn mẫu
    public Transform pointFire; // Điểm tạo bóng bắn
    public GameObject ballBoom; // bóng boom mẫu
    public GameObject newBallBoom; // bóng boom
    public Sprite spriteOld; //img bóng cũ
    public static bool boolBoom; //xác định việc tạo bóng Boom
    // Update is called once per frame
    public void Start(){
        boolBoom = true;
    }
    public void creatBoom()
    {
        if(boolBoom){
            ballFire = GameObject.FindGameObjectWithTag("ballFire");
            newBallBoom = Instantiate(ballBoom, pointFire.position, Quaternion.identity); // Tạo bóng bắn mới
            newBallBoom.tag = "ballBoom";
            spriteOld = ballFire.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            Destroy(ballFire);
        }
        else{
            ballFire = newBallBoom;
            newBallBoom = Instantiate(ballFireMau, pointFire.position, Quaternion.identity); // Tạo bóng bắn mới
            newBallBoom.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spriteOld;
            Destroy(ballFire);
            foreach (GameObject ballMap in GameObject.FindGameObjectsWithTag("boomMap")){
                ballMap.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = ballMap.GetComponent<SpriteRenderer>().sprite;
                ballMap.tag = "ballMap";
            }
        }
        boolBoom = !boolBoom;
    }
    public void creatLine()
    {
        if(boolBoom){
            ballFire = GameObject.FindGameObjectWithTag("ballFire");
            newBallBoom = Instantiate(ballBoom, pointFire.position, Quaternion.identity); // Tạo bóng bắn mới
            newBallBoom.tag = "ballLine";
            spriteOld = ballFire.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            Destroy(ballFire);
        }
        else{
            ballFire = newBallBoom;
            newBallBoom = Instantiate(ballFireMau, pointFire.position, Quaternion.identity); // Tạo bóng bắn mới
            newBallBoom.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spriteOld;
            Destroy(ballFire);
            foreach (GameObject ballMap in GameObject.FindGameObjectsWithTag("boomMap")){
                ballMap.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = ballMap.GetComponent<SpriteRenderer>().sprite;
                ballMap.tag = "ballMap";
            }
        }
        boolBoom = !boolBoom;
    }
    public void creatlaze()
    {
        if(boolBoom){
            ballFire = GameObject.FindGameObjectWithTag("ballFire");
            newBallBoom = Instantiate(ballBoom, pointFire.position, Quaternion.identity); // Tạo bóng bắn mới
            newBallBoom.tag = "ballLaze";
            spriteOld = ballFire.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            Destroy(ballFire);
        }
        else{
            ballFire = newBallBoom;
            newBallBoom = Instantiate(ballFireMau, pointFire.position, Quaternion.identity); // Tạo bóng bắn mới
            newBallBoom.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spriteOld;
            Destroy(ballFire);
            foreach (GameObject ballMap in GameObject.FindGameObjectsWithTag("boomMap")){
                ballMap.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = ballMap.GetComponent<SpriteRenderer>().sprite;
                ballMap.tag = "ballMap";
            }
        }
        boolBoom = !boolBoom;
    }
    public void creatRainbow()
    {
        if(boolBoom){
            ballFire = GameObject.FindGameObjectWithTag("ballFire");
            newBallBoom = Instantiate(ballBoom, pointFire.position, Quaternion.identity); // Tạo bóng bắn mới
            newBallBoom.tag = "ballRainbow";
            spriteOld = ballFire.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            Destroy(ballFire);
        }
        else{
            ballFire = newBallBoom;
            newBallBoom = Instantiate(ballFireMau, pointFire.position, Quaternion.identity); // Tạo bóng bắn mới
            newBallBoom.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spriteOld;
            Destroy(ballFire);
            foreach (GameObject ballMap in GameObject.FindGameObjectsWithTag("boomMap")){
                ballMap.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = ballMap.GetComponent<SpriteRenderer>().sprite;
                ballMap.tag = "ballMap";
            }
        }
        boolBoom = !boolBoom;
    }
}
