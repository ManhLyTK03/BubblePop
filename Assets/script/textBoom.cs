using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textBoom : MonoBehaviour
{
    // Khóa để truy cập số lượng bóng boom
    private string[] ballsKey = {
        "ballBoom",
        "ballLaze",
        "rainBow",
        "ballLine"
    };
    public int[] intBalls;
    public Text[] textBall;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < ballsKey.Length; i++){
            intBalls[i] = PlayerPrefs.GetInt(ballsKey[i], 0);
            textBall[i].text = intBalls[i] + "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < ballsKey.Length; i++){
            if(intBalls[i] != PlayerPrefs.GetInt(ballsKey[i], 0)){
                intBalls[i] = PlayerPrefs.GetInt(ballsKey[i], 0);
                textBall[i].text = intBalls[i] + "";
            }
        }
    }
}
