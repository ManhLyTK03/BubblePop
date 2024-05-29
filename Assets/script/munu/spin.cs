using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class spin : MonoBehaviour
{
    public GameObject panelThuong;
    public Sprite[] spritePanels;
    public float minSpeed;
    public float maxSpeed;
    public float rotationSpeed;
    public float autoSpeed;
    public float rotationTime = 10f;
    public float timeEnd;
    public bool isSpin = false;
    public int soluongThuong = 8;
    private float alphaThuong;
    void Start(){
        alphaThuong = 360/soluongThuong;
    }
    public void StartSpin(GameObject button){
        if(!isSpin){
            isSpin = true;
            StartCoroutine(RotateAndStop());
            button.SetActive(false);
        }
    }

    IEnumerator RotateAndStop()
    {
        rotationSpeed = Random.Range(minSpeed, maxSpeed);
        for (float t = rotationTime; t > 0f; t -= Time.deltaTime)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            yield return null;
        }
        StartCoroutine(endSpeed());
    }
    IEnumerator endSpeed()
    {
        for (float speed = rotationSpeed; speed > 0f; speed -= (rotationSpeed/timeEnd)*Time.deltaTime)
        {
            transform.Rotate(Vector3.forward, speed * Time.deltaTime);
            yield return null;
        }
        _NhanQuaSpin(transform.eulerAngles.z);
    }
    void _NhanQuaSpin(float alpha){
        Image panelImage = panelThuong.transform.GetChild(1).GetComponent<Image>();
        if(alpha <= alphaThuong*1){
            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin", 0) + 10);
            panelImage.sprite = spritePanels[0];
        }
        if(alpha > alphaThuong*1 && alpha <= alphaThuong*2){
            PlayerPrefs.SetInt("ballBoom", PlayerPrefs.GetInt("ballBoom", 0) + 1);
            panelImage.sprite = spritePanels[1];
        }
        if(alpha > alphaThuong*2 && alpha <= alphaThuong*3){
            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin", 0) + 20);
            panelImage.sprite = spritePanels[0];
        }
        if(alpha > alphaThuong*3 && alpha <= alphaThuong*4){
            PlayerPrefs.SetInt("ballLaze", PlayerPrefs.GetInt("ballLaze", 0) + 1);
            panelImage.sprite = spritePanels[2];
        }
        if(alpha > alphaThuong*4 && alpha <= alphaThuong*5){
            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin", 0) + 50);
            panelImage.sprite = spritePanels[0];
        }
        if(alpha > alphaThuong*5 && alpha <= alphaThuong*6){
            PlayerPrefs.SetInt("ballLine", PlayerPrefs.GetInt("ballLine", 0) + 1);
            panelImage.sprite = spritePanels[3];
        }
        if(alpha > alphaThuong*6 && alpha <= alphaThuong*7){
            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin", 0) + 100);
            panelImage.sprite = spritePanels[0];
        }
        if(alpha > alphaThuong*7 && alpha <= alphaThuong*8){
            PlayerPrefs.SetInt("rainBow", PlayerPrefs.GetInt("rainBow", 0) + 1);
            panelImage.sprite = spritePanels[4];
        }
        panelThuong.SetActive(true);
        isSpin = false;
    }
}
