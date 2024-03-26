// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class creatAnchor : MonoBehaviour
// {
//     void OnTriggerEnter2D(Collider2D collider)
//     {
//         if (collider.gameObject.CompareTag("ballMap"))
//         {
//             StartCoroutine(DelayedCheckConnect(collider.gameObject.GetComponent<ballFall>()));
//             StartCoroutine(CheckConnectWhenReady(collider.gameObject.GetComponent<ballFall>()));
//         }
//     }
//     IEnumerator DelayedCheckConnect(ballFall ball)
//     {
//         yield return new WaitForSeconds(0.1f);

//         ball._checkConnect(); // Thực thi hàm _checkConnect của ballFall
//     }
//     IEnumerator CheckConnectWhenReady(ballFall ball)
//     {
//         while (!ghiban.checkGhiban) // Kiểm tra nếu check là false
//         {
//             yield return null; // Chờ một frame
//         }
//         // Khi check trở thành true
//         setConnect();
//         ball._checkConnect(); // Thực thi hàm _checkConnect của ballFall
//         ghiban.checkGhiban = false;
//     }
//     void setConnect(){
//         // Tìm tất cả các game object có tag là "ball"
//         GameObject[] ballMaps = GameObject.FindGameObjectsWithTag("ballMap");

//         // In ra tên của các game object có tag "ball"
//         foreach (GameObject ball in ballMaps)
//         {
//             ball.GetComponent<ballFall>().checkConnect = 0;
//         }
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creatAnchor : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    void Start(){
        // Lấy BoxCollider2D từ GameObject hiện tại
        boxCollider = GetComponent<BoxCollider2D>();

        if (boxCollider == null)
        {
            Debug.LogError("Không tìm thấy BoxCollider2D trên GameObject này.");
            return;
        }
    }
    void Update(){
        if(ghiban.checkGhiban){
            setConnect();
        }
    }
    void setConnect(){
        ghiban.checkGhiban = false;

        // Tìm tất cả các game object có tag là "ball"
        GameObject[] ballMaps = GameObject.FindGameObjectsWithTag("ballMap");

        // In ra tên của các game object có tag "ball"
        foreach (GameObject ball in ballMaps)
        {
            ball.GetComponent<ballFall>().checkConnect = 0;
        }
        setAnchor();
    }
    public void setAnchor(){
        // Tạo một mảng chứa tất cả các GameObject va chạm với BoxCollider2D
        Collider2D[] colliders = Physics2D.OverlapBoxAll(boxCollider.bounds.center, boxCollider.bounds.size, 0f);

        // duyệt qua các gameObject va chạm
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("ballMap")){
                collider.gameObject.GetComponent<ballFall>()._checkConnect();
            }
        }
    }
}
