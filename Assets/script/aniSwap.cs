using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class aniSwap : MonoBehaviour
{
    public GameObject targetObject;
    public float displayTime = 10f; // Thời gian hiển thị
    public float fadeTime = 1.5f;     // Thời gian mờ
    public float rotationSpeed = 100f; // Tốc độ xoay

    IEnumerator Start()
    {
        while (true)
        {
            yield return StartCoroutine(ShowObject());
            yield return StartCoroutine(HideObject());
            yield return new WaitForSeconds(displayTime);
        }
    }

    IEnumerator ShowObject()
    {
        StartCoroutine(rotateObject());
        float currentTime = 0f;
        Color objectColor = targetObject.GetComponent<SpriteRenderer>().color;
        while (currentTime < fadeTime)
        {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, currentTime / fadeTime);
            objectColor.a = alpha;
            targetObject.GetComponent<SpriteRenderer>().color = objectColor;
            yield return null;
        }
    }
    IEnumerator rotateObject()
    {
        float currentTime = 0f;
        while (currentTime < fadeTime *2f)
        {
            currentTime += Time.deltaTime;
            targetObject.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime); // Xoay GameObject
            yield return null;
        }
    }

    IEnumerator HideObject()
    {
        float currentTime = 0f;
        Color objectColor = targetObject.GetComponent<SpriteRenderer>().color;
        while (currentTime < fadeTime)
        {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, currentTime / fadeTime);
            objectColor.a = alpha;
            targetObject.GetComponent<SpriteRenderer>().color = objectColor;
            yield return null;
        }
    }

}
