using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class creatMapLever : MonoBehaviour
{
    public GameObject buttonLever;
    public GameObject outLevel;
    public Text textLever;
    public float topEdgeY;
    public Vector3 pointTop,pointBottom;
    public GameObject TopY,BottomY;
    public float maxWall,distanceY;
    public float topX,bottomX;
    public int intLeverTop = 0;
    public int intLeverBottom = 0;
    public int intMaxLevel = 0;
    public string path;
    // Start is called before the first frame update
    
    void Start()
    {
        topEdgeY = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        maxWall = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x/2;
        distanceY = 1f;
        GameObject buttonLeverNew = Instantiate(buttonLever, new Vector3(0,0,0), Quaternion.identity);
        textLever = buttonLeverNew.GetComponentInChildren<Text>();
        intLeverTop = PlayerPrefs.GetInt("lever", 1);
        intLeverBottom = PlayerPrefs.GetInt("lever", 1);
        textLever.text = PlayerPrefs.GetInt("lever", 1) + "";
        buttonLeverNew.transform.parent = transform;
        buttonLeverNew.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        pointTop = new Vector3(maxWall,distanceY,0);
        pointBottom = new Vector3(-maxWall,-distanceY,0);
        topX = maxWall;
        bottomX = maxWall;
        TopY = null;
        BottomY = null;
        path = Path.Combine(Application.persistentDataPath, "saveMap.txt");

        if (!File.Exists(path))
        {
            path = Path.Combine(Application.persistentDataPath, "saveMap.txt");
        }
        intMaxLevel = MaxLevel();
    }
    void Update(){
        if(pointTop.y < topEdgeY + 3f){
            GameObject buttonLeverNew;
            intLeverTop++;
            if(intLeverTop <= intMaxLevel){
                buttonLeverNew = Instantiate(buttonLever, pointTop, Quaternion.identity);
            }
            else{
                buttonLeverNew = Instantiate(outLevel, pointTop, Quaternion.identity);
            }
            textLever = buttonLeverNew.GetComponentInChildren<Text>();
            textLever.text = intLeverTop + "";
            TopY = buttonLeverNew;
            buttonLeverNew.transform.parent = transform;
            buttonLeverNew.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            if(pointTop.x >= maxWall){
                topX = -maxWall;
            }
            else if(pointTop.x <= -maxWall){
                topX = maxWall;
            }
        }
        if(TopY != null){
            pointTop = TopY.transform.position + new Vector3(topX,distanceY,0);
        }
        if(pointBottom.y > -topEdgeY - 3f && intLeverBottom > 1){
            intLeverBottom--;
            GameObject buttonLeverNew = Instantiate(buttonLever, pointBottom, Quaternion.identity);
            textLever = buttonLeverNew.GetComponentInChildren<Text>();
            textLever.text = intLeverBottom + "";
            BottomY = buttonLeverNew;
            buttonLeverNew.transform.parent = transform;
            buttonLeverNew.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            if(pointBottom.x >= maxWall){
                bottomX = -maxWall;
            }
            else if(pointBottom.x <= -maxWall){
                bottomX = maxWall;
            }
        }
        if(BottomY != null){
            pointBottom = BottomY.transform.position + new Vector3(bottomX,-distanceY,0);
        }
    }
    int MaxLevel()
    {
        // Đọc dữ liệu từ tệp văn bản
        string data = File.ReadAllText(path);

        // Tách chuỗi thành các mảng con bằng dấu "\n"
        string[] arrays = data.Split('\n');

        // Số lượng mảng
        int arrayCount = arrays.Length;
        
        return arrayCount-1;
    }
}
