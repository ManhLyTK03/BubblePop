using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonPlay : MonoBehaviour
{
    public GameObject panelObject;
    public Text textLevel;
    void Update(){
        if(LoadLevel.Click){
            LoadLevel.Click = false;
            panelObject.SetActive(true);
            textLevel.text = "LEVEL " + LoadLevel.levelPlay;
        }
    }
    public void buttonLever(){
        mapRandom.typeMap = LoadLevel.levelInfo;
        SceneManager.LoadScene("mainPlay");
    }
    public void Play(){
        mapRandom.typeMap = LoadLevel.levelInfo;
        panelObject.SetActive(false);
        SceneManager.LoadScene("mainPlay");
    }
    public void CreateMap(){
        SceneManager.LoadScene("CreateMap");
    }
}
