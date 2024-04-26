using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LevelManager : MonoBehaviour
{
    public int[,] levelData;
    public int[] newMap;
    public string path;

    void Start()
    {
        // Khởi tạo mảng 2 chiều
        levelData = new int[0,0];
        newMap = new int[0];
    }

    // Hàm này được gọi khi bấm nút "Submit"
    public void SaveLevelData()
    {
        _checkMap();
        // Tạo chuỗi dữ liệu từ mảng 2 chiều
        string data = "";
        for (int i = 0; i <= newMap.Length - 1; i++)
        {
            data += newMap[i];
            Debug.Log(newMap[i]);
            if (i != newMap.Length - 1){
                data += ",";
            }
        }

        // Mở file để ghi, sử dụng StreamWriter với append = true
        using (StreamWriter writer = new StreamWriter(path, true))
        {
            // Ghi dữ liệu vào cuối file
            writer.WriteLine(data);
        }
    }
    void LoadLevelData()
    {
        path = Path.Combine(Application.persistentDataPath, "saveMap.txt");

        if (!File.Exists(path))
        {
            path = Path.Combine(Application.persistentDataPath, "saveMap.txt");

        }
    }
    public void _checkMap(){
        newMap = new int[0];
        LoadLevelData();
        foreach (GameObject ballFallObject in GameObject.FindGameObjectsWithTag("ballCreat"))
        {
            ballFallObject.tag = "ballMap";
        }
        GameObject[] ballMaps = GameObject.FindGameObjectsWithTag("ballMap");
        // Sắp xếp các GameObject trong mảng
        Array.Sort(ballMaps, CompareGameObjects);

        for(int i = 0; i < ballMaps.Length; i++)
        {
            int typeBall = 0;
            if(ballMaps[i].transform.childCount > 1){
                if(ballMaps[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name == "ballBlue"){
                    typeBall = 21;
                }
                else if(ballMaps[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name == "ballRed"){
                    typeBall = 22;
                }
                else if(ballMaps[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name == "ballYellow"){
                    typeBall = 23;
                }
                else if(ballMaps[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name == "ballViolet"){
                    typeBall = 24;
                }
            }
            else if(ballMaps[i].transform.childCount > 0){
                if(ballMaps[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name == "ballBlue"){
                    typeBall = 1;
                }
                else if(ballMaps[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name == "ballRed"){
                    typeBall = 2;
                }
                else if(ballMaps[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name == "ballYellow"){
                    typeBall = 3;
                }
                else if(ballMaps[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name == "ballViolet"){
                    typeBall = 4;
                }
                else if(ballMaps[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name == "ballStone"){
                    typeBall = 11;
                }
                else if(ballMaps[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name == "ballHole"){
                    typeBall = 12;
                }
            }
            else{
                if(ballMaps[i].name == "NewBallMap"){
                    typeBall = 10;
                }
            }
            AddElement(typeBall);
        }
    }
    private int CompareGameObjects(GameObject obj1, GameObject obj2)
    {
        // So sánh theo position y
        int compareY = obj1.transform.position.y.CompareTo(obj2.transform.position.y);
        
        // Nếu position y giống nhau, so sánh theo position x
        if (compareY == 0)
        {
            return obj1.transform.position.x.CompareTo(obj2.transform.position.x);
        }
        else
        {
            return compareY;
        }
    }
    void AddElement(int newElement)
    {
        // Tạo một mảng tạm thời có kích thước lớn hơn mảng ban đầu một phần tử
        int[] tempArray = new int[newMap.Length + 1];
        // Sao chép dữ liệu từ mảng ban đầu vào mảng tạm thời
        for (int i = 0; i < newMap.Length; i++)
        {
            tempArray[i] = newMap[i];
        }

        // Thêm phần tử mới vào cuối mảng tạm thời
        tempArray[tempArray.Length - 1] = newElement;

        // Gán mảng tạm thời vào mảng ban đầu
        newMap = tempArray;
    }
    public void tryMap(){
        mapRandom.typeMap = newMap;
        SceneManager.LoadScene("mainPlay");
    }
}