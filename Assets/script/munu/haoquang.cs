using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public GameObject gameObject;
    public float rotationSpeed = 100f; // Tốc độ xoay, đơn vị là độ trên giây

    void Update()
    {
        // Xoay GameObject quanh trục Z (trục hướng vào màn hình)
        gameObject.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
