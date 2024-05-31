using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hienthisao : MonoBehaviour
{
    public Text textLevel;
    public Image[] spriteStar;
    public Sprite[] starImages;
    public Sprite[] starImages1;
    // Start is called before the first frame update
    void OnEnable()
    {
        Invoke("_setStar", 0.01f);
    }
    void OnDisable(){
        for(int i = 0; i < 3; i++){
            spriteStar[i].sprite = starImages1[i];
        }
    }
    void _setStar(){
        string[] parts = textLevel.text.Split(' ');
        int level = int.Parse(parts[parts.Length - 1]);
        for(int i = 0; i < saveStart._intStart(level); i++){
            spriteStar[i].sprite = starImages[i];
        }
    }
}
