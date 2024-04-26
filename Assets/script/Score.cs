using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int intScore = 0;
    public static int intCombo = 0;
    private Text scoreText;

    void Start()
    {
        intScore = 0;
        intCombo = 0;
        scoreText = GetComponent<Text>();
    }

    void Update()
    {
        scoreText.text = intScore.ToString();
    }
}
