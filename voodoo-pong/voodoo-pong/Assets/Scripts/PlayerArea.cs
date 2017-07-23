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

    public void MovePaddle(BaseEventData baseEventData)
    {
        var eventData = baseEventData as PointerEventData;
        var newY = eventData.position.y / Screen.height;
        paddle.anchorMin = new Vector2(paddle.anchorMin.x, newY);
        paddle.anchorMax = new Vector2(paddle.anchorMax.x, newY);
    }
}
