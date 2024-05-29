using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starScore : MonoBehaviour
{
    public static int intStart;
    public RectTransform maxScore;
    public float widthMaxScore;
    public int intMaxScore = 0;
    public RectTransform scoreObject;
    private int scoreCheck = 0;
    public SpriteRenderer[] starImage;
    public Sprite spriteStar;
    // Start is called before the first frame update
    void Start()
    {
        intStart = 0;
        widthMaxScore = maxScore.sizeDelta.x;
        Invoke("inputCol", 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(scoreCheck != Score.intScore){
            scoreCheck = Score.intScore;
            if(scoreCheck >= intMaxScore/2){
                if(starImage[0].sprite != spriteStar){
                    starImage[0].sprite = spriteStar;
                }
                if(intStart < 1){
                    intStart = 1;
                }
            }
            if(scoreCheck >= intMaxScore*3/4){
                if(starImage[1].sprite != spriteStar){
                    starImage[1].sprite = spriteStar;
                }
                if(intStart < 2){
                    intStart = 2;
                }
            }
            if(scoreCheck >= intMaxScore){
                if(starImage[2].sprite != spriteStar){
                    starImage[2].sprite = spriteStar;
                }
                if(intStart < 3){
                    intStart = 3;
                }
            }
            float width = Score.intScore * widthMaxScore / intMaxScore;
            if(width > widthMaxScore){
                width = widthMaxScore;
            }
            // Thay đổi chiều rộng
            scoreObject.sizeDelta = new Vector2(width, scoreObject.sizeDelta.y);
            scoreObject.anchoredPosition = new Vector2(width/2f, scoreObject.anchoredPosition.y);
        }
    }
    void inputCol(){
        int col = mapRandom.typeMap.Length/20;
        intMaxScore = mapRandom.typeMap.Length*90;
    }
}
