using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class lostGame : MonoBehaviour
{
    public static int intMaxBall = 0;
    public Text numberBallText;
    private int numBalls = 0;
    public GameObject panelLost;
    public static bool checkWin = false;

    void Update()
    {
        if(checkWin){
            checkWin = false;
            _WinCheck();
        }
        if (numBalls != intMaxBall)
        {
            numBalls = intMaxBall;
            numberBallText.text = intMaxBall.ToString();
        }
        if (intMaxBall == 0)
        {
            _Lost();
        }
    }

    void _Lost()
    {
        panelLost.SetActive(true);
    }
    void _WinCheck(){
        foreach (GameObject ballMap in GameObject.FindGameObjectsWithTag("ballMap")){
            ballMap.tag = "ballFall";
            // Tắt collider trigger
            ballMap.GetComponent<Collider2D>().isTrigger = false;
            ballMap.layer = LayerMask.NameToLayer("ballFall");
            Rigidbody2D ballRB = ballMap.GetComponent<Rigidbody2D>();
            ballRB.gravityScale = 1f;
            // Gán vận tốc và hướng rơi ban đầu cho quả bóng
            ballRB.velocity = new Vector2(Random.Range(-1f,1f),Random.Range(0f,1f)).normalized * Random.Range(0f,2f);
        }
        GameObject[] balls = GameObject.FindGameObjectsWithTag("ballFall");
        if (balls.Length == 0) {
            Score.intScore += intMaxBall*200;
            Invoke("loadWin", 0.1f);
        }
        else{
            Invoke("_WinCheck", 0.1f);
        }
    }
    void loadWin(){
        int lever = PlayerPrefs.GetInt("lever", 1);
        lever++;
        // Lưu giá trị mới của lever vào PlayerPrefs
        PlayerPrefs.SetInt("lever", lever);
        // Gọi Save() để lưu thay đổi xuống ổ đĩa
        PlayerPrefs.Save();
        SceneManager.LoadScene("mainMenu");
    }
}
