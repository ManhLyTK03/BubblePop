using UnityEngine;
using System.Collections;

public class spin : MonoBehaviour
{
    
    public float minSpeed;
    public float maxSpeed;
    public float rotationSpeed;
    public float autoSpeed;
    public float rotationTime = 10f;
    public float timeEnd;

    private bool isRotating = false;
    void Start(){
        if (!isRotating)
        {
            StartCoroutine(autoSpin());
        }
    }
    public void StartSpin(){
        if (!isRotating)
        {
            StartCoroutine(RotateAndStop());
        }
    }

    IEnumerator RotateAndStop()
    {
        rotationSpeed = Random.Range(minSpeed, maxSpeed);
        isRotating = true;
        for (float t = rotationTime; t > 0f; t -= Time.deltaTime)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            yield return null;
        }
        StartCoroutine(endSpeed());
        isRotating = false;
    }
    IEnumerator endSpeed()
    {
        for (float speed = rotationSpeed; speed > 0f; speed -= (rotationSpeed/timeEnd)*Time.deltaTime)
        {
            transform.Rotate(Vector3.forward, speed * Time.deltaTime);
            yield return null;
        }
    }
    IEnumerator autoSpin()
    {
        for (;;){
            transform.Rotate(Vector3.forward, autoSpeed * Time.deltaTime);
            if(isRotating){
                break;
            }
            yield return null;
        }
    }
}
