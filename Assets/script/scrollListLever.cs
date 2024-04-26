using UnityEngine;

public class scrollListLever : MonoBehaviour
{
    private bool isMouseDown = false;
    private Vector3 mOffset;
    public float scrollSpeed = 0.1f; // Tốc độ cuộn


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
            mOffset = gameObject.transform.position - GetMouseWorldPos();
        }

        if (isMouseDown && Input.GetMouseButton(0))
        {
            Vector3 newPos = new Vector3(transform.position.x, GetMouseWorldPos().y + mOffset.y, transform.position.z);
            // Làm mềm di chuyển để tạo hiệu ứng liên tục
            transform.position = Vector3.Lerp(transform.position, newPos, scrollSpeed);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
