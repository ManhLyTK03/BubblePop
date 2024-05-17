using UnityEngine;
using UnityEngine.UI;

public class saveCoin : MonoBehaviour
{
    public int coin;
    private Text coinText;
    // Start is called before the first frame update
    void Start()
    {
        coinText = GetComponent<Text>();
        coin = PlayerPrefs.GetInt("coin", 0); // Nếu không có giá trị lưu trữ, mặc định coin = 0
        coinText.text = coin + "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
