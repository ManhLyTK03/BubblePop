using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class setbuttonMenu : MonoBehaviour
{
    public GameObject panel;
    public GameObject[] panels;
    public RectTransform[] panelRects;
    public GameObject[] buttons;
    private Image panelImage;
    private RectTransform panelRectTransform;
    public RectTransform heightTransform;
    private float height;
    void Start(){
        panelImage = panel.GetComponent<Image>();
        panelRectTransform = panel.GetComponent<RectTransform>();
        height = heightTransform.sizeDelta.y;
    }
    public void setPosition(){
        panelImage.color = new Color(
            panelImage.color.r,
            panelImage.color.g,
            panelImage.color.b,
            1f
        );
        // Đặt chiều rộng của panel
        panelRectTransform.sizeDelta = new Vector2(height*1.5f, panelRectTransform.sizeDelta.y);
        GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().anchoredPosition.x, 100f);
        foreach (GameObject p in panels)
        {
            Image pImage = p.GetComponent<Image>();
            pImage.color = new Color(
                pImage.color.r,
                pImage.color.g,
                pImage.color.b,
                0f
            );
            RectTransform pRectTransform = p.GetComponent<RectTransform>();
            pRectTransform.sizeDelta = new Vector2(height, pRectTransform.sizeDelta.y);
        }
        foreach (GameObject b in buttons)
        {
            b.GetComponent<RectTransform>().anchoredPosition = new Vector2(b.GetComponent<RectTransform>().anchoredPosition.x, 0f);
        }
        float newXPosition = 0f;
        foreach (RectTransform setPanel in panelRects){
            setPanel.anchoredPosition = new Vector2(newXPosition + setPanel.sizeDelta.x/2f, setPanel.anchoredPosition.y);
            newXPosition += setPanel.sizeDelta.x;
        }
    }
}
