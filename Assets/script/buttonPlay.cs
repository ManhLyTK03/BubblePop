using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonPlay : MonoBehaviour
{
    public static bool boolPanel = false;
    public Text textLevel;
    void Update(){
        textLevel.text = LoadLevel.level + "";
    }
    public void buttonLever(){
        mapRandom.typeMap = LoadLevel.levelInfo;
        SceneManager.LoadScene("mainPlay");
    }
    public void Play(){
        mapRandom.typeMap = LoadLevel.levelInfo;
        SceneManager.LoadScene("mainPlay");
    }
    public void CreateMap(){
        SceneManager.LoadScene("CreateMap");
    }
}
