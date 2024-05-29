using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setPositionMenu : MonoBehaviour
{
    public RectTransform panelRectTransform;
    public RectTransform[] panelMenus;
    public RectTransform[] buttonMenus;
    public RectTransform[] widthButtons;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        //set panel main
        int intPos = -3;
        foreach (RectTransform panelMenu in panelMenus)
        {
            panelMenu.anchoredPosition = new Vector2(Screen.width*intPos, panelMenu.anchoredPosition.y);
            intPos ++;
        }
        // set panel menu
        panelRectTransform.sizeDelta = new Vector2(panelRectTransform.sizeDelta.x, Screen.width/5.5f);
        panelRectTransform.anchoredPosition = new Vector2(panelRectTransform.anchoredPosition.x, panelRectTransform.sizeDelta.y/2f);

        foreach (RectTransform widthButton in widthButtons)
        {
            widthButton.sizeDelta = new Vector2(panelRectTransform.sizeDelta.y, widthButton.sizeDelta.y);
        }
        //set button
        float positionButton = 0f;
        intPos = 0;
        foreach (RectTransform buttonMenu in buttonMenus)
        {
            if(intPos == 2){
                buttonMenu.sizeDelta = new Vector2(panelRectTransform.sizeDelta.y*1.5f, buttonMenu.sizeDelta.y);
            }
            else{
                buttonMenu.sizeDelta = new Vector2(panelRectTransform.sizeDelta.y, buttonMenu.sizeDelta.y);
            }
            buttonMenu.anchoredPosition = new Vector2(positionButton + buttonMenu.sizeDelta.x/2f, buttonMenu.anchoredPosition.y);
            positionButton += buttonMenu.sizeDelta.x;
            intPos ++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
