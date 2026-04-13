using System.Collections;
using UnityEngine;

public class HintTextPanel : MonoBehaviour
{
    public RectTransform panel;
    public AnimationCurve HintTextPanelAnimationCurve;

    void Start()
    {
        panel.sizeDelta = new Vector2(0, 0);
    }

    public void Show()
    {
        StartCoroutine(TransitionIn());
    }

    public void Hide()
    {
        StartCoroutine(TransitionOut());
    }

    IEnumerator TransitionIn()
    {
        float currentTime = 0f;
        while (currentTime <= 1f)
        {
            currentTime += Time.deltaTime;
            float percent = Mathf.Clamp01(currentTime / 1f);
            float curvePercent = HintTextPanelAnimationCurve.Evaluate(percent);
            panel.sizeDelta = new Vector2(0, 156 * curvePercent);
            yield return null;
        }
    }

    IEnumerator TransitionOut()
    {
        float currentTime = 0f;
        while (currentTime <= 1f)
        {
            currentTime += Time.deltaTime;
            float percent = Mathf.Clamp01(1 - currentTime / 1f);
            float curvePercent = HintTextPanelAnimationCurve.Evaluate(percent);
            panel.sizeDelta = new Vector2(0, 156 * curvePercent);
            yield return null;
        }
    }
}
