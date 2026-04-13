using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingText : MonoBehaviour
{
    public TMP_Text Text;
    public List<string> MainTextOptions;
    private int _index = 0;
    public string PreText = "Preparing";
    public string MainText = "the loading screen";
    private string _postText = "";
    public AnimationCurve TextTransitionAnimationCurve;
    public float fadeDuration = 1;
    private Coroutine _carouselCoroutine;

    void Start()
    {
        Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, 0);
        MainTextOptions.Shuffle();
        StartCoroutine(ElipsesAnimation());
    }

    void Update()
    {
        Text.text = PreText + " " + MainText + _postText;
    }

    public void StartCarousel()
    {
        _carouselCoroutine ??= StartCoroutine(MainTextCarousel());
    }

    public void StopCarousel()
    {
        if (_carouselCoroutine != null)
        {
            StopCoroutine(_carouselCoroutine);
            _carouselCoroutine = null;
            Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, 1);
        }
    }

    public void Show()
    {
        StartCoroutine(FadeIn());
    }

    public void Hide()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator ElipsesAnimation()
    {
        while (true)
        {
            if (_postText == ". . . ")
            {
                _postText = "";
            }
            else
            {
                _postText += ". ";
            }

            yield return new WaitForSeconds(1);
        }
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
    }

    IEnumerator FadeOut()
    {
        float currentTime = 0f;
        Color textColor = new Color(Text.color.r, Text.color.g, Text.color.b, 1);
        while (currentTime <= fadeDuration)
        {
            currentTime += Time.deltaTime;
            float percent = Mathf.Clamp01(currentTime / fadeDuration);
            float curvePercent = TextTransitionAnimationCurve.Evaluate(percent);
            textColor.a = Mathf.Lerp(1, 0, curvePercent);
            Text.color = textColor;
            yield return null;
        }
    }

    IEnumerator MainTextCarousel()
    {
        while (true)
        {
            if (_index == MainTextOptions.Count)
            {
                _index = 0;
                MainTextOptions.Shuffle();
            }

            MainText = MainTextOptions[_index];
            _postText = "";
            _index++;
            yield return new WaitForSeconds(UnityEngine.Random.Range(5f, 12f));
        }
    }
}
