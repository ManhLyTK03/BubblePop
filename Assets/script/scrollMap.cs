using UnityEngine;
using UnityEngine.SceneManagement;

public class scrollMap : MonoBehaviour
{
    private bool isMouseDown = false;
    private Vector3 mOffset;
    public Vector3 macDinh;
    void Start(){
        macDinh = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && creatBallMap.boolScroll)
        {
            isMouseDown = true;
            mOffset = gameObject.transform.position - GetMouseWorldPos();
        }

        if (isMouseDown && Input.GetMouseButton(0))
        {
            if(macDinh.y > GetMouseWorldPos().y + mOffset.y){
                transform.position = new Vector3(transform.position.x, GetMouseWorldPos().y + mOffset.y, transform.position.z);
            }
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
    public void Restart(){
        SceneManager.LoadScene("CreatMap");
    }
}
