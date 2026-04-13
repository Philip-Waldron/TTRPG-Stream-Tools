using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintText : MonoBehaviour
{
    public TMP_Text Text;
    public List<string> GeneralHints;
    public AnimationCurve TextTransitionAnimationCurve;
    public float fadeDuration;
    public bool SwappingText = false;

    void Start()
    {
        Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, 0);
        GeneralHints.Shuffle();
    }

    public void SwapToNewText(string newText)
    {
        StartCoroutine(SwapText(newText));
    }

    public void Show()
    {
        StartCoroutine(FadeIn());
    }

    public void Hide()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        float currentTime = 0f;
        Color textColor = new Color(Text.color.r, Text.color.g, Text.color.b, 0);
        while (currentTime <= fadeDuration)
        {
            currentTime += Time.deltaTime;
            float percent = Mathf.Clamp01(currentTime / fadeDuration);
            float curvePercent = TextTransitionAnimationCurve.Evaluate(percent);
            textColor.a = Mathf.Lerp(0, 1, curvePercent);
            Text.color = textColor;
            yield return null;
        }

        Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, 1);
    }

    IEnumerator FadeOut()
    {
        float currentTime = 0f;
        Color textColor = new Color(Text.color.r, Text.color.g, Text.color.b, 1);
        while (currentTime <= fadeDuration && Text.text != "")
        {
            currentTime += Time.deltaTime;
            float percent = Mathf.Clamp01(currentTime / fadeDuration);
            float curvePercent = TextTransitionAnimationCurve.Evaluate(percent);
            textColor.a = Mathf.Lerp(1, 0, curvePercent);
            Text.color = textColor;
            yield return null;
        }

        Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, 0);
    }

    IEnumerator SwapText(string newText)
    {
        SwappingText = true;
        yield return FadeOut();
        Text.text = newText;
        yield return FadeIn();
        SwappingText = false;
    }

    // Dummy to fix errors
    public void StartCarousel()
    {

    }

    // Dummy to fix errors
    public void StopCarousel()
    {

    }
}
