using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;

public class Countdown : MonoBehaviour
{
    public Text timeText;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(_Countdown());
    }
    private IEnumerator _Countdown()
    {
        // Tạo thời điểm nửa đêm của ngày tiếp theo
        DateTime midnight = DateTime.Now.Date.AddDays(1);
        // Tính khoảng thời gian đếm ngược đến nửa đêm
        TimeSpan countdown = midnight - DateTime.Now;
        while (countdown > TimeSpan.Zero)
        {
            countdown = midnight - DateTime.Now;
            timeText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", countdown.Hours, countdown.Minutes, countdown.Seconds);
            yield return new WaitForSeconds(1);
        }
    }
}
