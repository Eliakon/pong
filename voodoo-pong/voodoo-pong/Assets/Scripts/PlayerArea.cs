using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerArea : MonoBehaviour
{
    [SerializeField]
    private RectTransform area;

    [SerializeField]
    private RectTransform paddle;

    [SerializeField]
    private RectTransform ball;

    [SerializeField]
    private CanvasGroup group;

    private bool isIa;

    public void MovePaddle(BaseEventData baseEventData)
    {
        var eventData = baseEventData as PointerEventData;
        var newY = eventData.position.y / Screen.height;
        paddle.anchorMin = new Vector2(paddle.anchorMin.x, newY);
        paddle.anchorMax = new Vector2(paddle.anchorMax.x, newY);
    }

    public void EnableInput(bool enable)
    {
        if (isIa)
        {
            enable = false;
        }

        group.blocksRaycasts = enable;
        group.interactable = enable;
    }

    public void SetIa(bool ia)
    {
        isIa = ia;

        if (isIa)
        {
            EnableInput(false);
        }
    }

    public void Update()
    {
        if (isIa)
        {
            paddle.localPosition = new Vector3(
                paddle.localPosition.x,
                ball.localPosition.y,
                paddle.localPosition.z
            );
        }
    }
}
