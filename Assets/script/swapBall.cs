using UnityEngine;
using UnityEngine.UI;

public class swapBall : MonoBehaviour
{
    public Transform gameObjectTransform;
    public SpriteRenderer ballRenderer;
    private Button button;
    public float widthBall;

    void Start(){
        // chiều rộng của bóng
        widthBall = ballRenderer.bounds.size.x;
        button = GetComponent<Button>();
        Invoke("buttonPosition", 0.1f);
    }
    void Update()
    {

    }
    void buttonPosition(){
        // Kiểm tra nếu GameObject và Button đã được thiết lập
        if (gameObjectTransform != null){
            // Lấy vị trí của GameObject
            Vector3 gameObjectPosition = gameObjectTransform.position;
            
            // Chuyển vị trí của GameObject từ World Space sang Screen Space
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(gameObjectPosition);

            // Cập nhật vị trí của Button
            transform.position = gameObjectPosition;
            // cập nhật chiều rộng button
        }
    }
}
