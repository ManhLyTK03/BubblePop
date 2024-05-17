using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class moveMainMenu : MonoBehaviour
{
    public int intMove;
    public RectTransform mainMenu;
    public float duration = 0.3f;
    public void moveMenu()
    {
        StartCoroutine(MoveMenuCoroutine());
    }

    private IEnumerator MoveMenuCoroutine()
    {
        Vector2 startPos = mainMenu.anchoredPosition;
        Vector2 endPos = new Vector2(intMove * Screen.width, mainMenu.anchoredPosition.y);

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            mainMenu.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainMenu.anchoredPosition = endPos; // Ensure the final position is accurate
    }
}
