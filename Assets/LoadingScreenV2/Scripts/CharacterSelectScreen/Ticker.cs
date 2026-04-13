using System;
using TMPro;
using UnityEngine;

public class Ticker : MonoBehaviour
{
    public float ItemDuration = 3f;
    public TMP_Text[] TextItems;

    public Vector3 TickerDirection = Vector3.left;

    private float _width;
    private float _pixelsPerSecond;

    void Start()
    {
        _width = GetComponent<RectTransform>().rect.width;
        _pixelsPerSecond = _width / ItemDuration;
    }

    void Update()
    {
        foreach (var textItem in TextItems)
        {
            UpdateTextItem(textItem.rectTransform);
        }
    }

    private void UpdateTextItem(RectTransform rectTransform)
    {
        rectTransform.position += rectTransform.rotation * TickerDirection * _pixelsPerSecond * Time.deltaTime;
        float wrapDistance = _width / 2 + rectTransform.rect.width / 2;

        if (rectTransform.anchoredPosition.x <= -wrapDistance)
        {
            rectTransform.anchoredPosition = new(wrapDistance, rectTransform.anchoredPosition.y);
        }
        else if (rectTransform.anchoredPosition.x >= wrapDistance)
        {
            rectTransform.anchoredPosition = new(-wrapDistance, rectTransform.anchoredPosition.y);
        }
    }
}
