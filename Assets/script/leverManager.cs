using UnityEngine;

public class leverManager : MonoBehaviour
{
    private int lever;

    void Start()
    {
        // Load giá trị của biến lever từ PlayerPrefs khi game bắt đầu
        lever = PlayerPrefs.GetInt("lever", 1); // Nếu không có giá trị lưu trữ, mặc định lever = 1
    }

    // // Hàm để tăng giá trị của lever
    // void IncreaseLever()
    // {
    //     lever++;
    //     // Lưu giá trị mới của lever vào PlayerPrefs
    //     PlayerPrefs.SetInt("lever", lever);
    //     // Gọi Save() để lưu thay đổi xuống ổ đĩa
    //     PlayerPrefs.Save();
    // }
}
