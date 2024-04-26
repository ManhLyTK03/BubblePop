using UnityEngine;

public class MouseInputManager2D : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 rayPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero);

            if (hit.collider != null)
            {
                hit.collider.gameObject.SendMessage("OnMouseClick", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
