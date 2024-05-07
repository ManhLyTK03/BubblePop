using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LevelManager : MonoBehaviour
{
    public int[,] levelData;
    public int[] newMap;
    public string path;
    private int lever;
    GameObject[] ballMaps;

    void Start()
    {
        // PlayerPrefs.SetInt("lever", 1);
        lever = PlayerPrefs.GetInt("lever", 1); // Nếu không có giá trị lưu trữ, mặc định lever = 1
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
            ballFallObject.tag = "ballFall";
        }
        foreach (GameObject ballFallObject in GameObject.FindGameObjectsWithTag("ballMap"))
        {
            ballFallObject.tag = "ballFall";
        }
        ballMaps = GameObject.FindGameObjectsWithTag("ballFall");
        // Sắp xếp các GameObject trong mảng
        SortByPosition();
        Debug.Log(ballMaps.Length);
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
    void SortByPosition()
    {
        // Sắp xếp mảng theo tọa độ y (tăng dần), nếu tọa độ y bằng nhau thì sắp xếp theo tọa độ x (tăng dần)
        System.Array.Sort(ballMaps, (x, y) =>
        {
            float xPosition = x.transform.position.y * 1000 + x.transform.position.x;
            float yPosition = y.transform.position.y * 1000 + y.transform.position.x;
            return xPosition.CompareTo(yPosition);
        });
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