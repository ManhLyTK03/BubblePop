using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballManager : MonoBehaviour
{
    // Khóa để truy cập số lượng bóng boom
    private const string ballBoomKey = "ballBoom";
    // Khóa để truy cập số lượng bóng Laze
    private const string ballLazeKey = "ballLaze";
    // Khóa để truy cập số lượng bóng cầu vồng
    private const string rainBowKey = "rainBow";
    // Khóa để truy cập số lượng bóng Line
    private const string ballLineKey = "ballLine";
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.GetInt(ballBoomKey, 0);
        PlayerPrefs.GetInt(ballLazeKey, 0);
        PlayerPrefs.GetInt(rainBowKey, 0);
        PlayerPrefs.GetInt(ballLineKey, 0);
    }
}
