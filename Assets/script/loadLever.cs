using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LoadLevel : MonoBehaviour
{
    public static int level = 1; // Biến static lưu trữ level hiện tại
    public static int levelPlay = 1; // Biến static lưu trữ level đang chơi
    public static int[] levelInfo; // Mảng static lưu trữ thông tin của level
    public float speed = 0.8f; // Tốc độ di chuyển của panel
    private float startTime; // Thời gian bắt đầu di chuyển
    private float journeyLength; // Độ dài hành trình di chuyển
    public GameObject panelObject; // Đối tượng panel sẽ di chuyển
    public bool movePanel = false; // Biến kiểm tra trạng thái di chuyển của panel
    public TextAsset fileMap; // Tệp TextAsset chứa dữ liệu map

    void Start()
    {
        startTime = Time.time;
        level = PlayerPrefs.GetInt("level", 1); // Lấy giá trị level từ PlayerPrefs, nếu không có thì mặc định là 1
        LeverMacDinh(); // Gọi hàm thiết lập level mặc định
    }

    void Update()
    {
        if (movePanel)
        {
            journeyLength = Vector3.Distance(panelObject.transform.position, new Vector3(panelObject.transform.position.x, 0, 0)); // Tính độ dài hành trình di chuyển
            float distanceCovered = (Time.time - startTime) * speed; // Tính khoảng cách đã di chuyển
            float fractionOfJourney = distanceCovered / journeyLength; // Tính phần trăm của hành trình đã đi được
            panelObject.transform.position = Vector3.Lerp(panelObject.transform.position, new Vector3(panelObject.transform.position.x, 0, 0), fractionOfJourney); // Di chuyển panel theo hành trình đã tính

            if (panelObject.transform.position.y <= 0) // Kiểm tra nếu panel đã đến vị trí đích
            {
                movePanel = false; // Dừng di chuyển panel
            }
        }
    }

    public void LoadLevelMap()
    {
        Text textLevel = GetComponentInChildren<Text>(); // Lấy thành phần Text con
        if (textLevel != null && int.TryParse(textLevel.text, out levelPlay)) // Kiểm tra và chuyển đổi giá trị từ Text sang int
        {
            if (LoadLevelData(levelPlay)) // Gọi hàm load dữ liệu map với level tương ứng
            {
                panelObject = GameObject.FindWithTag("boomMap"); // Tìm đối tượng panel theo tag "boomMap"
                if (panelObject != null) // Kiểm tra nếu tìm thấy đối tượng panel
                {
                    movePanel = true; // Bắt đầu di chuyển panel
                    startTime = Time.time; // Đặt lại thời gian bắt đầu khi bắt đầu di chuyển
                }
                else
                {
                    Debug.LogError("Panel object with tag 'boomMap' not found."); // In ra lỗi nếu không tìm thấy panel
                }
            }
            else
            {
                Debug.LogError("Level không hợp lệ!"); // In ra lỗi nếu level không hợp lệ
            }
        }
        else
        {
            Debug.LogError("Không thể phân tích số nguyên: " + (textLevel != null ? textLevel.text : "Không tìm thấy thành phần Text")); // In ra lỗi nếu không thể phân tích số nguyên hoặc không tìm thấy thành phần Text
        }
    }

    public void LeverMacDinh()
    {
        LoadLevelData(level); // Gọi hàm load dữ liệu map với level hiện tại
    }

    private bool LoadLevelData(int levelToLoad)
    {
        // Đọc nội dung từ file
        string[] lines = fileMap.text.Split('\n'); // Sử dụng Split để tách nội dung tệp thành các dòng

        // Kiểm tra nếu level nằm trong phạm vi của mảng lines
        if (levelToLoad >= 1 && levelToLoad <= lines.Length)
        {
            // Tạo mảng int để lưu trữ thông tin của map
            string[] mapString = lines[levelToLoad - 1].Split(','); // Tách chuỗi thành các phần tử bằng dấu phẩy
            levelInfo = new int[mapString.Length]; // Khởi tạo mảng levelInfo với kích thước tương ứng

            // Chuyển đổi từ string sang int và lưu vào mảng levelInfo
            for (int i = 0; i < mapString.Length; i++)
            {
                if (!int.TryParse(mapString[i], out levelInfo[i])) // Kiểm tra và chuyển đổi từng phần tử từ string sang int
                {
                    Debug.LogError("Không thể phân tích dữ liệu map tại index " + i); // In ra lỗi nếu không thể chuyển đổi
                    return false; // Trả về false nếu có lỗi
                }
            }
            return true; // Trả về true nếu load thành công
        }
        else
        {
            // Nếu level không hợp lệ, in ra thông báo lỗi
            Debug.LogError("Level không hợp lệ!"); // In ra lỗi nếu level không hợp lệ
            return false; // Trả về false nếu level không hợp lệ
        }
    }
}
