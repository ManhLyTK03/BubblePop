using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class loadFileMap : MonoBehaviour
{
    public TextAsset fileMap;
    // Start is called before the first frame update
    void Start()
    {
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
    }
}
