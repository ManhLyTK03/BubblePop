using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinPanel : MonoBehaviour
{
    public GameObject panelSpin;
    public bool clostPanel;
    // Update is called once per frame
    public void spinOn()
    {
        panelSpin.SetActive(clostPanel);
    }
}
