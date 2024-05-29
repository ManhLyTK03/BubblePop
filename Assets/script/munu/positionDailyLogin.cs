using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positionDailyLogin : MonoBehaviour
{
    public RectTransform panelMain;
    public bool widthHeight;
    public float size;
    public float delay = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        if(panelMain == null){
            panelMain = GetComponent<RectTransform>();
        }
        Invoke("setPosition", delay);
    }
    void setPosition(){
        if(widthHeight){
            panelMain.sizeDelta = new Vector2(0, panelMain.rect.width*size);
        }
        else{
            panelMain.sizeDelta = new Vector2(panelMain.rect.height*size, 0);
        }
    }
}
