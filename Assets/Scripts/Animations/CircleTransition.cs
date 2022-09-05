using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleTransition : MonoBehaviour
{
    private Canvas canvas;
    private Image blackScreen;

    private Vector2 playerCanvasPosition;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        blackScreen = GetComponentInChildren<Image>();
    }

    private void DrawBlackScreen()
    {
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(GameManager.instance.GetPlayer().transform.position);

        var canvasRect = canvas.GetComponent<RectTransform>().rect;
        float canvasWidth = canvasRect.width;
        float canvasHeight = canvasRect.height;

        playerCanvasPosition = new Vector2
        {
            x = (playerScreenPosition.x / screenWidth) * canvasWidth,
            y = (playerScreenPosition.y / screenHeight) * canvasHeight,
        };

        float squareValue = 0f;

        if(canvasWidth > canvasHeight)
        {
            squareValue = canvasWidth;
            playerCanvasPosition.y += (canvasWidth - canvasHeight) * 0.5f;
        }
        else
        {
            squareValue = canvasHeight;
            playerCanvasPosition.y += (canvasHeight - canvasWidth) * 0.5f;
        }

        playerCanvasPosition /= squareValue;

        var material = blackScreen.material;
        material.SetFloat("_CenterX", playerCanvasPosition.x);
        material.SetFloat("_CenterY", playerCanvasPosition.y);

        blackScreen.rectTransform.sizeDelta = new Vector2(squareValue, squareValue);
    }

    public void OpenBlackScreen()
    {
        DrawBlackScreen();
        StartCoroutine(Transition(0.5f, 0, 1));
    }

    public void CloseBlackScreen()
    {
        DrawBlackScreen();
        StartCoroutine(Transition(0.5f, 1, 0));
    }


    private IEnumerator Transition(float duration, float beginRadius, float endRadius)
    {
        var material = blackScreen.material;

        float time = 0f;
        while (time <= duration)
        {
            time += Time.deltaTime;

            float t = time / duration;
            float radius = Mathf.Lerp(beginRadius, endRadius, t);

            blackScreen.material.SetFloat("_Radius", radius);

            yield return null;
        }
    }
}
