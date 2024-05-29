using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class saveStart : MonoBehaviour
{
    public static string path;
    // Start is called before the first frame update
    void Start()
    {
        path = Path.Combine(Application.persistentDataPath, "saveInfoMap.txt");

        if (!File.Exists(path))
        {
            // Tạo tệp tin mới
            string defaultContent = "";
            File.WriteAllText(path, defaultContent);
        }
    }
    public static void saveFile(int star)
    {
        string data = star + "";
        // Mở file để ghi, sử dụng StreamWriter với append = true
        using (StreamWriter writer = new StreamWriter(path, true))
        {
            // Ghi dữ liệu vào cuối file
            writer.WriteLine(data);
        }
    }
    public static void restartStart(int level, int star)
    {

        // Đọc toàn bộ nội dung file vào một mảng
        string[] lines = File.ReadAllLines(path);

        // Kiểm tra xem file có đủ số dòng hay không
        if (lines.Length >= level)
        {
            if(int.Parse(lines[level - 1]) < star){
                // Thay đổi dòng thứ 2 (index 1 vì mảng bắt đầu từ 0)
                lines[level - 1] = star + "";

                // Ghi lại toàn bộ nội dung vào file
                File.WriteAllLines(path, lines);
            }
        }
        else
        {
            Debug.Log("File không có đủ số dòng.");
        }
    }
    public static int _intStart(int level)
    {
        // Đọc tất cả nội dung từ file
        string[] lines = File.ReadAllLines(path);

        // Kiểm tra xem file có đủ dòng không
        if (lines.Length >= level)
        {
            string secondLine = lines[level - 1];
            return int.Parse(secondLine);
        }
        else
        {
            Debug.Log("File không có đủ dòng");
        }
        return 0;
    }

}
