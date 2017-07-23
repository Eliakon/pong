using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private RectTransform target;

    public void RandomShow()
    {
        var x = Random.Range(0, 100) / 100f;
        var y = Random.Range(0, 100) / 100f;
        var anchor = new Vector2(x, y);
        target.anchorMax = anchor;
        target.anchorMin = anchor;
        target.gameObject.SetActive(true);
    }
}