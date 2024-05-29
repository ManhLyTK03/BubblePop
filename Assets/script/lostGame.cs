using UnityEngine;
using UnityEngine.UI;

public class lostGame : MonoBehaviour
{
    public static int intMaxBall = 0;
    public Text numberBallText;
    public Text scoreWin;
    private int numBalls = 0;
    public GameObject panelLost;
    public static bool checkWin = false;
    public SpriteRenderer imgPanel;
    public Sprite spriteWin;
    public SpriteRenderer[] imgStart;
    public Sprite[] spriteStart;
    public GameObject buttonWin;
    public RectTransform buttonHome;
    public RectTransform buttonRestart;

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
        panelLost.SetActive(true);
        buttonWin.SetActive(true);
        scoreWin.text = Score.intScore.ToString();
        float width = buttonWin.GetComponent<RectTransform>().rect.width/2f;
        buttonHome.anchoredPosition = new Vector2(-width - buttonHome.rect.width/2f, buttonHome.anchoredPosition.y);
        buttonRestart.anchoredPosition = new Vector2(width + buttonHome.rect.width/2f, buttonHome.anchoredPosition.y);
        imgPanel.sprite = spriteWin;
        for(int i = 0; i < starScore.intStart; i++){
            imgStart[i].sprite = spriteStart[i];
            if(PlayerPrefs.GetInt("Stask", -1) == 1){
                int pass = PlayerPrefs.GetInt("pass", -1);
                pass++;
                PlayerPrefs.SetInt("pass", pass);
            }
        }
        SaveWin();
    }
    void SaveWin(){
        int lever = PlayerPrefs.GetInt("lever", 1);
        if(LoadLevel.levelPlay == lever){
            lever++;
            PlayerPrefs.SetInt("lever", lever);
            PlayerPrefs.Save();
            saveStart.saveFile(starScore.intStart);
        }
        else{
            saveStart.restartStart(LoadLevel.levelPlay,starScore.intStart);
        }
    }
}
