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
        for (int i = 0; i < levelData.GetLength(0); i++)
        {
            for (int j = 0; j < levelData.GetLength(1); j++)
            {
                data += levelData[i, j];
                if (j < levelData.GetLength(1) - 1)
                    data += ",";
            }
            if (i < levelData.GetLength(0) - 1)
                data += "\n"; // Thêm ký tự xuống dòng giữa các hàng
        }

        // Ghi dữ liệu vào file
        File.WriteAllText(path, data);
    }
    void LoadLevelData()
    {
        path = Path.Combine(Application.persistentDataPath, "saveMap.txt");

        if (File.Exists(path))
        {
            string data = File.ReadAllText(path);
            string[] rows = data.Split('\n');

            // Tách dữ liệu và gán cho mảng 2 chiều
            levelData = new int[rows.Length, rows[0].Split(',').Length];
            for (int i = 0; i < rows.Length; i++)
            {
                string[] rowData = rows[i].Split(',');
                for (int j = 0; j < rowData.Length; j++)
                {
                    levelData[i, j] = int.Parse(rowData[j]);
                }
            }
        }
        else
        {
            string path = Path.Combine(Application.persistentDataPath, "saveMap.txt");
        }

        if (File.Exists(path))
        {
            string data = File.ReadAllText(path);
            string[] rows = data.Split('\n');

            // Tách dữ liệu và gán cho mảng 2 chiều
            levelData = new int[rows.Length, rows[0].Split(',').Length];
            for (int i = 0; i < rows.Length; i++)
            {
                string[] rowData = rows[i].Split(',');
                for (int j = 0; j < rowData.Length; j++)
                {
                    levelData[i, j] = int.Parse(rowData[j]);
                }
            }
        }
        else
        {
            Debug.LogError("No level data file found!");
        }
    }
    public void _checkMap(){
        levelData = new int[0,0];
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
                    typeBall = 0;
                }
            }
            AddElement(typeBall);
        }
        
        int[] newArray = newMap;
        // Tạo một mảng mới có kích thước lớn hơn một hàng so với mảng ban đầu
        int[,] newLevelData = new int[levelData.GetLength(0) + 1, newArray.Length];
        // Sao chép dữ liệu từ mảng ban đầu vào mảng mới
        for (int i = 0; i < levelData.GetLength(0); i++)
        {
            for (int j = 0; j < levelData.GetLength(1); j++)
            {
                newLevelData[i, j] = levelData[i, j];
            }
        }

        // Thêm mảng mới vào cuối mảng
        for (int j = 0; j < newArray.Length; j++)
        {
            newLevelData[levelData.GetLength(0), j] = newArray[j];
        }

        // Gán mảng mới vào mảng cũ
        levelData = newLevelData;
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
        mapRandom.typeMap = new int[levelData.GetLength(1)];
        for (int j = 0; j < levelData.GetLength(1); j++)
        {
            mapRandom.typeMap[j] = levelData[levelData.GetLength(0) - 1, j];
        }
        SceneManager.LoadScene("mainPlay");
    }
}

