using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionOutline : MonoBehaviour
{
    public Image SelectionOutlineImage;
    public AnimationCurve AnimationCurve;
    public float fadeDuration;

    public void Show()
    {
        StartCoroutine(FadeIn());
    }

    public void Select()
    {
        StartCoroutine(SelectionAnimation());
    }

    public void Hide()
    {
        SelectionOutlineImage.color = new Color(SelectionOutlineImage.color.r, SelectionOutlineImage.color.g, SelectionOutlineImage.color.b, 0);
    }

    public void SetPosition(int index)
    {
        if (index < 6)
        {
            SelectionOutlineImage.rectTransform.localPosition = new Vector3(-177.675f + (71.07f * index), 178, 0);
        }
        else
        {
            SelectionOutlineImage.rectTransform.localPosition = new Vector3(-133.86f + (71.07f * (index - 6)), 14.47f, 0);
        }
    }

    IEnumerator FadeIn()
    {
        float currentTime = 0f;
        Color color = new Color(SelectionOutlineImage.color.r, SelectionOutlineImage.color.g, SelectionOutlineImage.color.b, 0);
        while (currentTime <= fadeDuration)
        {
            currentTime += Time.deltaTime;
            float percent = Mathf.Clamp01(currentTime / fadeDuration);
            float curvePercent = AnimationCurve.Evaluate(percent);
            color.a = Mathf.Lerp(0, 1, curvePercent);
            SelectionOutlineImage.color = color;
            yield return null;
        }
    }

    IEnumerator SelectionAnimation()
    {
        int blinkCount = 0;
        int blinkCountTarget = 4;
        float waitDuration = 0.06f;

        while (blinkCount < blinkCountTarget)
        {
            SelectionOutlineImage.color = new Color(SelectionOutlineImage.color.r, SelectionOutlineImage.color.g, SelectionOutlineImage.color.b, 0);
            yield return new WaitForSeconds(waitDuration);
            SelectionOutlineImage.color = new Color(SelectionOutlineImage.color.r, SelectionOutlineImage.color.g, SelectionOutlineImage.color.b, 1);
            yield return new WaitForSeconds(waitDuration);
            blinkCount++;
        }

        SelectionOutlineImage.color = new Color(SelectionOutlineImage.color.r, SelectionOutlineImage.color.g, SelectionOutlineImage.color.b, 0);
    }
}
