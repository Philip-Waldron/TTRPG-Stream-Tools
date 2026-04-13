using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LoadingCharacterBackground : MonoBehaviour
{
    public RectMask2D BackgroundMask;
    private float _initialMaskTopPadding;

    public Vector2 LoadingTimeRange = new(0.25f, 1.25f);
    public Vector2 LoadingPercentIncrementRange = new(0f, 0.1f);

    private const float DelayAtEndOfLoad = 4;
    public UnityEvent LoadFinished;

    public void Awake()
    {
        _initialMaskTopPadding = BackgroundMask.padding.w;
    }

    public void StartLoadingProgressAnimation()
    {
        StartCoroutine(LoadingProgressAnimation());
    }

    public void StopLoadingProgressAnimation()
    {
        StopAllCoroutines();
    }

    public void ResetToDefault()
    {
        BackgroundMask.padding = new Vector4(BackgroundMask.padding.x, BackgroundMask.padding.y, BackgroundMask.padding.z, _initialMaskTopPadding);
    }

    IEnumerator LoadingProgressAnimation()
    {
        float targetLoadPercent = 0f;
        yield return null;

        while (targetLoadPercent < 1f)
        {
            float currentTime = 0f;
            float loadingTime = Random.Range(LoadingTimeRange.x, LoadingTimeRange.y);

            float previousLoadPercent = targetLoadPercent;
            targetLoadPercent += Random.Range(LoadingPercentIncrementRange.x, LoadingPercentIncrementRange.y);
            targetLoadPercent = Mathf.Clamp01(targetLoadPercent);

            while (currentTime <= loadingTime)
            {
                currentTime += Time.deltaTime;
                float t = Mathf.Lerp(previousLoadPercent, targetLoadPercent, currentTime / loadingTime);
                BackgroundMask.padding = Vector4.Lerp(new Vector4(BackgroundMask.padding.x, BackgroundMask.padding.y, BackgroundMask.padding.z, _initialMaskTopPadding),
                                                      new Vector4(BackgroundMask.padding.x, BackgroundMask.padding.y, BackgroundMask.padding.z, 0),
                                                      t);
                yield return null;
            }
        }

        yield return new WaitForSeconds(DelayAtEndOfLoad);
        LoadFinished.Invoke();
    }
}
