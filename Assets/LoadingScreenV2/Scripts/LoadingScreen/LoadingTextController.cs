using System.Collections;
using TMPro;
using UnityEngine;

public class LoadingTextController : MonoBehaviour
{
    public DataManager Data;
    public TMP_Text LoadingText;
    public bool EllipsesPaused = true;
    public bool CarouselPaused = true;

    public string LoadingTextVerb = "Taking";
    public string LoadingTextSubject = "a break";
    public Vector2 LoadingTextCarouselIntervalRange = new(5f, 12f);

    private string _loadingTextEllipses = ". . . ";
    private const int EllipsesAnimationInterval = 1;

    public void Start()
    {
        StartCoroutine(LoadingTextEllipsesAnimation());
        StartCoroutine(LoadingTextCarousel());
    }

    public void Update()
    {
        LoadingText.text = LoadingTextVerb + " " + LoadingTextSubject + _loadingTextEllipses;
    }

    IEnumerator LoadingTextEllipsesAnimation()
    {
        float currentEllipsesTime = 0;
        yield return null;

        while (true)
        {
            while (EllipsesPaused)
            {
                yield return null;
            }

            currentEllipsesTime += Time.deltaTime;

            if (currentEllipsesTime >= EllipsesAnimationInterval)
            {
                currentEllipsesTime -= EllipsesAnimationInterval;

                if (_loadingTextEllipses == ". . . ")
                {
                    _loadingTextEllipses = "";
                }
                else
                {
                    _loadingTextEllipses += ". ";
                }
            }

            yield return null;
        }
    }

    IEnumerator LoadingTextCarousel()
    {
        float currentSubjectTime = 0;
        float subjectDisplayTime = Random.Range(LoadingTextCarouselIntervalRange.x, LoadingTextCarouselIntervalRange.y);
        yield return null;

        while (true)
        {
            while (CarouselPaused)
            {
                yield return null;
            }

            currentSubjectTime += Time.deltaTime;

            if (currentSubjectTime >= subjectDisplayTime)
            {
                currentSubjectTime -= subjectDisplayTime;
                subjectDisplayTime = Random.Range(LoadingTextCarouselIntervalRange.x, LoadingTextCarouselIntervalRange.y);

                LoadingTextVerb = Data.GetNextText(Enums.TextCategory.LoadingVerb);
                LoadingTextSubject = Data.GetNextText(Enums.TextCategory.LoadingSubject);
            }

            yield return null;
        }
    }
}
