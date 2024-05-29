using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextDisplay : MonoBehaviour
{
    public GameObject textPrefab; // Prefab của Text để hiển thị
    public Vector3 textSpawnPoint; // Vị trí để hiển thị Text
    public float moveSpeed = 1.0f; // Tốc độ di chuyển của Text
    public float fadeSpeed = 1.0f; // Tốc độ mờ dần của Text
    public int intHole;

    void Start(){
        textSpawnPoint = new Vector3(0,0,0);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ballFall") || other.CompareTag("ballMap")) // Kiểm tra xem có va chạm với gameObject có tag "Ball" không
        {
            textSpawnPoint = other.gameObject.transform.position;
            other.gameObject.tag = "ground";
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            Destroy(other.gameObject, 1f);
            if(PlayerPrefs.GetInt("Stask", -1) == 4){
                int pass = PlayerPrefs.GetInt("pass", -1);
                pass++;
                PlayerPrefs.SetInt("pass", pass);
            }
            StartCoroutine(DisplayTextAndFade(intHole + "",textSpawnPoint)); // Bắt đầu coroutine để hiển thị và mờ dần Text
        }
    }

    IEnumerator DisplayTextAndFade(string content, Vector3 point)
    {
        Score.intScore += intHole;
        GameObject textObject = Instantiate(textPrefab, textSpawnPoint, Quaternion.identity); // Tạo một instance mới của Text
        Text newText = textObject.GetComponentInChildren<Text>();
        newText.text = content; // Đặt nội dung của Text là số được truyền vào

        // Di chuyển Text lên trên
        while (newText.color.a > 0)
        {
            newText.transform.Translate(Vector3.up * moveSpeed * Time.deltaTime); // Di chuyển lên trên
            float newAlpha = Mathf.MoveTowards(newText.color.a, 0, fadeSpeed * Time.deltaTime); // Mờ dần alpha
            newText.color = new Color(newText.color.r, newText.color.g, newText.color.b, newAlpha); // Cập nhật màu của Text
            yield return null; // Chờ một frame
        }

        Destroy(textObject.gameObject); // Hủy Text khi hoàn thành hiệu ứng
    }
}
