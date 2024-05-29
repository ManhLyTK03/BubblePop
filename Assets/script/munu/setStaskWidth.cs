
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setStaskWidth : MonoBehaviour
{
    public RectTransform[] rectTransformItems;
    public RectTransform rectTransformBack1;
    public RectTransform rectTransformMain1;
    public float maxWidth1;
    public Sprite newSprite;
    // Khóa để truy cập số lượng nv hoàn thành
    private const string passKey = "pass";
    public static int pass;
    // Khóa để truy cập check Random
    private const string checkRanDomKey = "checkRanDom";
    public bool checkRanDom;
    // Khóa để truy cập nhiệm vụ hiện tại
    private const string StaskIntKey = "StaskInt";
    public int StaskInt;
    // Khóa để truy cập nhiệm vụ
    private const string StaskKey = "Stask";
    public int Stask;
    // Khóa để truy cập phần thưởng
    private const string RewardKey = "reward";
    public int Reward;
    private string[] StaskList = new string[] {
        "Nay khoi tuong {0} lan",
        "Tich luy {0} sao",
        "Tao chuoi combo x{0}",
        "Pha huy {0} qua bong",
        "Lam roi {0} qua bong"
    };
    private int[] IntStaskList = new int[] { 
        // 5,
        // 2,
        // 3,
        // 100,
        // 100
        1,
        1,
        1,
        1,
        1
    };
    private int[] IntRewardList = new int[] { 
        5,
        1,
        1,
        1,
        1
    };
    private string[] giftList = new string[] { 
        "coin",
        "ballBoom",
        "ballLine",
        "ballLaze",
        "rainBow"
    };
    public int StaskMain;
    public int RewardMain;
    public Sprite[] SpriteStaskList;
    public Sprite[] SpriteRewardList;

    public Image StaskImage;
    public Image RewardImage;
    public RectTransform rectTransformBack;
    public RectTransform rectTransformMain;
    public Text mainText;
    public Text passText;
    public float maxWidth;
    public GameObject[] endDutys;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("setPosition", 0.01f);
        Invoke("_setTaskWidth1", 0.02f);
        if(PlayerPrefs.GetInt(StaskIntKey, 1) == 0){
            PlayerPrefs.SetInt(StaskIntKey, 1);
        }
        StaskInt = PlayerPrefs.GetInt(StaskIntKey, 1);
        Stask = PlayerPrefs.GetInt(StaskKey, 1);
        StaskMain = IntStaskList[Stask] * StaskInt;
        if(StaskInt > 7){
            foreach (GameObject endDuty in endDutys)
            {
                endDuty.SetActive(false);
            }
            mainText.text = "Ban da hoan thanh het nhiem vu hom nay";
        }
        else{
            if(PlayerPrefs.GetInt(checkRanDomKey, 1) == 1){
                PlayerPrefs.SetInt(checkRanDomKey, 0);
                _RandomStask();
            }
            else{
                _setStask();
            }
        }
    }
    void _RandomStask(){
        int random = Random.Range(0, StaskList.Length-1);
        PlayerPrefs.SetInt(StaskKey, random);
        if(StaskInt <= 3){
            PlayerPrefs.SetInt(RewardKey, 0);
        }
        if(StaskInt == 4){
            PlayerPrefs.SetInt(RewardKey, 1);
        }
        if(StaskInt == 5){
            PlayerPrefs.SetInt(RewardKey, 2);
        }
        if(StaskInt == 6){
            random = Random.Range(3,4);
            PlayerPrefs.SetInt(RewardKey, random);
        }
        _setStask();
    }
    void _setStask(){
        StaskInt = PlayerPrefs.GetInt(StaskIntKey, 1);
        Stask = PlayerPrefs.GetInt(StaskKey, 0);
        StaskMain = IntStaskList[Stask] * StaskInt;
        StaskImage.sprite = SpriteStaskList[Stask];
        StaskImage.GetComponentInChildren<Text>().text = "x" + StaskMain;
        Reward = PlayerPrefs.GetInt(RewardKey, 0);
        RewardMain = IntRewardList[Reward] * StaskInt;
        if(StaskInt>4){
            RewardMain = 1;
        }
        RewardImage.sprite = SpriteRewardList[Reward];
        RewardImage.GetComponentInChildren<Text>().text = "x" + RewardMain;
        mainText.text = string.Format(StaskList[Stask], StaskMain);
        pass = PlayerPrefs.GetInt(passKey, 0);
        passText.text = pass + "/" + StaskMain;
        Invoke("_setTaskWidth", 0.01f);
    }
    void _setTaskWidth()
    {
        maxWidth = rectTransformBack.rect.width/StaskMain;
        int width = pass;
        if(width > StaskMain){
            width = StaskMain;
        }
        rectTransformMain.sizeDelta = new Vector2(width * maxWidth, rectTransformMain.sizeDelta.y);
    }
    public void _NhanQua(){
        if(PlayerPrefs.GetInt(passKey, 0) >= StaskMain){
            PlayerPrefs.SetInt(giftList[Reward], PlayerPrefs.GetInt(giftList[Reward], 0) + RewardMain);
            PlayerPrefs.SetInt(passKey, 0);
            if(StaskInt >= 7){
                foreach (GameObject endDuty in endDutys)
                {
                    endDuty.SetActive(false);
                }
                mainText.text = "Ban da hoan thanh het nhiem vu hom nay";
            }
            else{
                PlayerPrefs.SetInt(StaskIntKey, PlayerPrefs.GetInt(StaskIntKey, 1) + 1);
                _RandomStask();
                _setStask();
                _setTaskWidth1();
            }
        }
    }
    void setPosition(){
        maxWidth1 = rectTransformBack1.rect.width/(rectTransformItems.Length-1);
        for(int i = 0; i < rectTransformItems.Length; i++){
            rectTransformItems[i].anchoredPosition = new Vector2(i*maxWidth1, rectTransformItems[i].anchoredPosition.y);
        }
    }

    void _setTaskWidth1()
    {
        rectTransformMain1.sizeDelta = new Vector2((PlayerPrefs.GetInt(StaskIntKey, 1)-1) * maxWidth1, rectTransformMain1.sizeDelta.y);
        for(int i = 0; i < PlayerPrefs.GetInt(StaskIntKey, 1); i++){
            rectTransformItems[i].GetComponent<Image>().sprite = newSprite;
        }
    }
}
