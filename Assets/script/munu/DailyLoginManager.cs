using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;

public class DailyLoginManager : MonoBehaviour
{
    // Khóa để truy cập ngày đăng nhập cuối cùng trong PlayerPrefs
    private const string LastLoginDateKey = "LastLoginDate";
    
    // Khóa để check nhận quà đăng nhập
    private const string ReceiveLoginGiftKey = "receivedLogin";
    private const string ReceiveLoginGiftDayKey = "receivedLoginDay";

    // Khóa để truy cập số ngày đăng nhập liên tiếp
    private const string DailyLoginCountKey = "DailyLoginCount";
    
    public Image[] panelImages;
    public Button[] giftImages;
    public Sprite spritePanel;
    public Sprite spritePanel1;
    public Sprite giftSprite;
    public Sprite giftSprite1;
    public Image buttonImage;
    public Sprite spriteButton;
    public Text timeText;
    public TimeSpan countdown;
    public RectTransform rectTransform;
    public RectTransform rectTransformDay;
    public float maxWidth;
    public int intDay;

    void Start()
    {
        // Kiểm tra xem khóa đã tồn tại hay chưa
        if (!PlayerPrefs.HasKey(DailyLoginCountKey))
        {
            // Nếu chưa tồn tại (lần đăng nhập đầu tiên), đặt giá trị là 1
            PlayerPrefs.SetInt(DailyLoginCountKey, 1);
        }
        maxWidth = rectTransform.rect.width;
        CheckDailyLogin();
    }

    void CheckDailyLogin()
    {
        // Lấy ngày đăng nhập cuối cùng từ PlayerPrefs, mặc định là giá trị tối thiểu nếu không được đặt
        DateTime lastLoginDate = DateTime.Parse(PlayerPrefs.GetString(LastLoginDateKey, DateTime.MinValue.ToString()));
        // Lấy ngày hiện tại dưới dạng chuỗi.
        DateTime currentDate = DateTime.Now;

        // Lấy số lần đăng nhập hàng ngày hiện tại
        int dailyLoginCount = PlayerPrefs.GetInt(DailyLoginCountKey, 1);
        
        // Kiểm tra xem người chơi đã đăng nhập hôm nay chưa.
        if (lastLoginDate != currentDate)
        {
            // Tính khoảng cách ngày giữa lần đăng nhập cuối cùng và ngày hiện tại.
            TimeSpan difference = DateTime.Now.Date - lastLoginDate.Date;

            if (difference.Days > 0)
            {
                PlayerPrefs.SetInt(ReceiveLoginGiftDayKey, 1);
                PlayerPrefs.SetInt("checkRanDom", 1);
                PlayerPrefs.SetInt("StaskInt", 0);
                PlayerPrefs.SetInt("pass", 0);
                PlayerPrefs.SetInt(ReceiveLoginGiftKey, 1);
            }

            // Nếu khoảng cách là 1 ngày, tăng số ngày đăng nhập liên tục.
            if (difference.Days == 1)
            {
                dailyLoginCount++; // Tăng số ngày đăng nhập liên tục.
                PlayerPrefs.SetInt(DailyLoginCountKey, dailyLoginCount);
            }
            else if(difference.Days > 1)
            {
                dailyLoginCount = 1;
                // Nếu khoảng cách lớn hơn 1 ngày, reset số ngày đăng nhập liên tục.
                PlayerPrefs.SetInt(DailyLoginCountKey, 1);
            }
            //lưu ngày đăng nhập
            PlayerPrefs.SetString(LastLoginDateKey, currentDate.ToString());

            rectTransformDay.sizeDelta = new Vector2((maxWidth/intDay)*dailyLoginCount + 1f, rectTransformDay.sizeDelta.y);
            if(rectTransformDay.sizeDelta.x > maxWidth){
                rectTransformDay.sizeDelta = new Vector2(maxWidth, rectTransformDay.sizeDelta.y);
            }
            for(int i = 0; i < giftImages.Length; i++){
                giftImages[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(maxWidth/intDay*int.Parse(giftImages[i].GetComponentInChildren<Text>().text) - maxWidth/2f, giftImages[i].GetComponent<RectTransform>().anchoredPosition.y);
            }

            // Hiển thị phần thưởng
            DisplayReward(dailyLoginCount);
        }

    }

    // Phương thức để hiển thị phần thưởng dựa trên số lần đăng nhập hàng ngày
    private void DisplayReward(int dailyLoginCount)
    {
        int day = (dailyLoginCount - 1)%7;
        //phần thưởng theo ngày
        if(day > 0){
            for(int i = 0; i < day; i++){
                panelImages[i].sprite = spritePanel1;
            }
        }
        // hiển thị phần thưởng dựa trên số lần đăng nhập hàng ngày
        if(PlayerPrefs.GetInt(ReceiveLoginGiftDayKey, 1) == 1){
            panelImages[day].sprite = spritePanel;
        }
        else{
            panelImages[day].sprite = spritePanel1;
            buttonImage.sprite = spriteButton;
            StartCoroutine(Countdown());
        }
        //phần thưởng theo mốc
        for(int i = 0; i < giftImages.Length; i++){
            if(rectTransformDay.sizeDelta.x - maxWidth/2f > giftImages[i].GetComponent<RectTransform>().anchoredPosition.x){
                Debug.Log(rectTransformDay.sizeDelta.x - maxWidth/2f + " " + giftImages[i].GetComponent<RectTransform>().anchoredPosition.x);
                Debug.Log(i);
                giftImages[i].image.sprite = giftSprite1;
            }
        }
        if((dailyLoginCount/7)-1>=0){
            if(PlayerPrefs.GetInt(ReceiveLoginGiftKey, 1) == 1 && dailyLoginCount == int.Parse(giftImages[(dailyLoginCount/7)-1].GetComponentInChildren<Text>().text)){
                giftImages[(dailyLoginCount/7)-1].image.sprite = giftSprite;
            }
        }
    }
    //nhận thưởng
    public void CollectLogin()
    {
        int dailyLoginCount = PlayerPrefs.GetInt(DailyLoginCountKey, 1);
        int day = (dailyLoginCount-1)%7;
        if(PlayerPrefs.GetInt(ReceiveLoginGiftDayKey, 1) == 1){
            PlayerPrefs.SetInt(ReceiveLoginGiftDayKey, 0);
            panelImages[day].sprite = spritePanel1;
            if(day+1 == 1){
                PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin", 0) + 10);
            }
            if(day+1 == 2){
                PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin", 0) + 20);
            }
            if(day+1 == 3){
                PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin", 0) + 30);
            }
            if(day+1 == 4){
                PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin", 0) + 40);
            }
            if(day+1 == 5){
                PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin", 0) + 50);
            }
            if(day+1 == 6){
                PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin", 0) + 60);
            }
            if(day+1 == 7){
                PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin", 0) + 70);
            }
            buttonImage.sprite = spriteButton;
            StartCoroutine(Countdown());
        }
    }
    private IEnumerator Countdown()
    {
        // Tạo thời điểm nửa đêm của ngày tiếp theo
        DateTime midnight = DateTime.Now.Date.AddDays(1);
        // Tính khoảng thời gian đếm ngược đến nửa đêm
        countdown = midnight - DateTime.Now;
        while (countdown > TimeSpan.Zero)
        {
            countdown = midnight - DateTime.Now;
            timeText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", countdown.Hours, countdown.Minutes, countdown.Seconds);
            yield return new WaitForSeconds(1);
        }
        PlayerPrefs.SetInt(ReceiveLoginGiftDayKey, 1);
        CheckDailyLogin();
    }
    public void _giftButton(int day)
    {
        int dailyLoginCount = PlayerPrefs.GetInt(DailyLoginCountKey, 1);
        if(PlayerPrefs.GetInt(ReceiveLoginGiftKey, 1) == 1 && dailyLoginCount == int.Parse(giftImages[(dailyLoginCount/7)-1].GetComponentInChildren<Text>().text)){
            PlayerPrefs.SetInt(ReceiveLoginGiftKey, 0);
            giftImages[day-1].image.sprite = giftSprite1;
            if(dailyLoginCount == 8){
                PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin", 0) + 80);
            }
            if(dailyLoginCount == 15){
                PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin", 0) + 150);
            }
            if(dailyLoginCount == 22){
                PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin", 0) + 220);
            }
            if(dailyLoginCount == 30){
                PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin", 0) + 300);
            }
        }
    }
}
