using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LoadLevel : MonoBehaviour
{
    public static int level = 1;
    public static int levelPlay = 1;
    public string saveFileName = "saveMap.txt";
    public static int[] levelInfo;
    public float speed = 0.8f;
    private float startTime;
    private float journeyLength;
    public GameObject panelObject;
    public bool movePanel = false;
    public string filePath;
    public TextAsset fileMap;
    void Start(){
        // Lấy đường dẫn đầy đủ đến file lưu trữ
        string fileMapSave = Path.Combine(Application.persistentDataPath, "saveMap.txt");
        // Kiểm tra xem file đã tồn tại chưa
        if (!File.Exists(fileMapSave))
        {
            // Nếu không tồn tại, tạo file mới và ghi dữ liệu từ TextAsset vào đó
            using (StreamWriter writer = new StreamWriter(fileMapSave))
            {
                writer.Write(fileMap.text);
            }
        }
        startTime = Time.time;
        filePath = Path.Combine(Application.persistentDataPath, saveFileName);
        level = PlayerPrefs.GetInt("lever", 1);
        LeverMacDinh();
    }
    void Update(){
        if(movePanel){
            journeyLength = Vector3.Distance(panelObject.transform.position, new Vector3(panelObject.transform.position.x,0,0));
            float distanceCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distanceCovered / journeyLength;
            panelObject.transform.position = Vector3.Lerp(panelObject.transform.position, new Vector3(panelObject.transform.position.x,0,0), fractionOfJourney);
        }
    }
    public void LoadLevelMap()
    {
        Text textLevel = GetComponentInChildren<Text>();
        levelPlay = int.Parse(textLevel.text);
        if (textLevel != null)
        {
            if (int.TryParse(textLevel.text, out level))
            {   
                // Đọc nội dung từ file
                string[] lines = File.ReadAllLines(filePath);

                // Kiểm tra nếu level nằm trong phạm vi của mảng lines
                if (level >= 1 && level <= lines.Length)
                {
                    // Tạo mảng int để lưu trữ thông tin của map
                    string[] mapString = lines[level - 1].Split(','); // Tách chuỗi thành các phần tử bằng dấu phẩy
                    levelInfo = new int[mapString.Length];
                    
                    // Chuyển đổi từ string sang int và lưu vào mảng levelInfo
                    for (int i = 0; i < mapString.Length; i++)
                    {
                        int.TryParse(mapString[i], out levelInfo[i]);
                    }
                }
                else
                {
                    // Nếu level không hợp lệ, in ra thông báo lỗi
                    Debug.LogError("Level không hợp lệ!");
                }
            }
            else
            {
                Debug.LogError("Unable to parse integer: " + textLevel.text);
            }
        }
        else
        {
            Debug.LogError("Text component not found.");
        }
        panelObject = GameObject.FindWithTag("boomMap");
        movePanel = true;
    }
    public void LeverMacDinh()
    {
        // Đọc nội dung từ file
        string[] lines = File.ReadAllLines(filePath);

        // Kiểm tra nếu level nằm trong phạm vi của mảng lines
        if (level >= 1 && level <= lines.Length)
        {
            // Tạo mảng int để lưu trữ thông tin của map
            string[] mapString = lines[level - 1].Split(','); // Tách chuỗi thành các phần tử bằng dấu phẩy
            levelInfo = new int[mapString.Length];
            
            // Chuyển đổi từ string sang int và lưu vào mảng levelInfo
            for (int i = 0; i < mapString.Length; i++)
            {
                int.TryParse(mapString[i], out levelInfo[i]);
            }
        }
    }
}
