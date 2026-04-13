using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [Header("CHA, WIS, INT, CON, DEX, STR, HP, AC")]
    public List<MonoBehaviour> Stats = new List<MonoBehaviour>();
    public AnimationCurve StatsAnimationCurve;
    public float StaggerDuration = 0.025f;
    public TMP_Text CharacterName;
    public AnimationCurve CharacterNameAnimationCurve;
    public float CharacterNameAnimationDuration = 0.5f;
    public TMP_Text CharacterClass;
    public RectTransform CharacterClassPanel;
    public AnimationCurve CharacterClassAnimationCurve;


    public void Show()
    {
        StartCoroutine(CharacterNameFadeIn());

        for (int index = 0; index < Stats.Count; index++)
        {
            RectTransform stat = Stats[index].GetComponent<RectTransform>();
            float xStart = -1300;
            float xEnd = -340;
            if (index != Stats.Count - 1)
            {
                xStart = -1250 + (250 - index * 100);
                xEnd = 310 - index * 100;
            }

            StartCoroutine(TransitionIn(stat, new Vector3(xStart, stat.localPosition.y, stat.localPosition.z), new Vector3(xEnd, stat.localPosition.y, stat.localPosition.z), index * StaggerDuration));
        }

        StartCoroutine(CharacterClassTransitionIn(CharacterClassPanel, new Vector3(-530, CharacterClassPanel.localPosition.y, CharacterClassPanel.localPosition.z), new Vector3(-445, CharacterClassPanel.localPosition.y, CharacterClassPanel.localPosition.z), Stats.Count * StaggerDuration + 0.5f));
    }

    public void Hide()
    {
        StartCoroutine(CharacterNameFadeOut());
        StartCoroutine(CharacterClassTransitionOut(CharacterClassPanel, new Vector3(-530, CharacterClassPanel.localPosition.y, CharacterClassPanel.localPosition.z), new Vector3(-445, CharacterClassPanel.localPosition.y, CharacterClassPanel.localPosition.z), 0));

        for (int index = 0; index < Stats.Count; index++)
        {
            RectTransform stat = Stats[index].GetComponent<RectTransform>();
            float xStart = 850;
            float xEnd = -340;
            if (index != Stats.Count - 1)
            {
                xStart = 1250 + (250 - index * 100);
                xEnd = 310 - index * 100;
            }

            StartCoroutine(TransitionOut(stat, new Vector3(xStart, stat.localPosition.y, stat.localPosition.z), new Vector3(xEnd, stat.localPosition.y, stat.localPosition.z), index * StaggerDuration));
        }
    }

    public void SetStats(string name, string characterClass, int ac, int hp, int strength, int dexterity, int constitution, int inteligence, int wisdom, int charisma)
    {
        CharacterName.text = name;
        CharacterClass.text = characterClass;
        Stats[0].GetComponent<IStat>().SetStat(charisma);
        Stats[1].GetComponent<IStat>().SetStat(wisdom);
        Stats[2].GetComponent<IStat>().SetStat(inteligence);
        Stats[3].GetComponent<IStat>().SetStat(constitution);
        Stats[4].GetComponent<IStat>().SetStat(dexterity);
        Stats[5].GetComponent<IStat>().SetStat(strength);
        Stats[6].GetComponent<IStat>().SetStat(hp);
        Stats[7].GetComponent<IStat>().SetStat(ac);
    }

    IEnumerator TransitionIn(RectTransform rectTransform, Vector3 startPos, Vector3 endPos, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        float currentTime = 0f;
        while (currentTime <= 1f)
        {
            currentTime += Time.deltaTime;
            float percent = Mathf.Clamp01(currentTime / 1f);
            float curvePercent = StatsAnimationCurve.Evaluate(percent);
            rectTransform.localPosition = startPos + (endPos - startPos) * curvePercent;
            yield return null;
        }
    }
    IEnumerator TransitionOut(RectTransform rectTransform, Vector3 startPos, Vector3 endPos, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        float currentTime = 0f;
        while (currentTime <= 1f)
        {
            currentTime += Time.deltaTime;
            float percent = Mathf.Clamp01(1 - currentTime / 1f);
            float curvePercent = StatsAnimationCurve.Evaluate(percent);
            rectTransform.localPosition = startPos + (endPos - startPos) * curvePercent;
            yield return null;
        }
    }

    IEnumerator CharacterNameFadeIn()
    {
        float currentTime = 0f;
        Color textColor = new Color(CharacterName.color.r, CharacterName.color.g, CharacterName.color.b, 0);
        while (currentTime <= CharacterNameAnimationDuration)
        {
            currentTime += Time.deltaTime;
            float percent = Mathf.Clamp01(currentTime / CharacterNameAnimationDuration);
            float curvePercent = CharacterNameAnimationCurve.Evaluate(percent);
            textColor.a = Mathf.Lerp(0, 1, curvePercent);
            CharacterName.color = textColor;
            yield return null;
        }
    }

    IEnumerator CharacterNameFadeOut()
    {
        float currentTime = 0f;
        Color textColor = new Color(CharacterName.color.r, CharacterName.color.g, CharacterName.color.b, 1);
        while (currentTime <= CharacterNameAnimationDuration)
        {
            currentTime += Time.deltaTime;
            float percent = Mathf.Clamp01(currentTime / CharacterNameAnimationDuration);
            float curvePercent = CharacterNameAnimationCurve.Evaluate(percent);
            textColor.a = Mathf.Lerp(1, 0, curvePercent);
            CharacterName.color = textColor;
            yield return null;
        }
    }

    IEnumerator CharacterClassTransitionIn(RectTransform rectTransform, Vector3 startPos, Vector3 endPos, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        float currentTime = 0f;
        while (currentTime <= 1f)
        {
            currentTime += Time.deltaTime;
            float percent = Mathf.Clamp01(currentTime / 1f);
            float curvePercent = CharacterClassAnimationCurve.Evaluate(percent);
            rectTransform.localPosition = startPos + (endPos - startPos) * curvePercent;
            yield return null;
        }
    }
    IEnumerator CharacterClassTransitionOut(RectTransform rectTransform, Vector3 startPos, Vector3 endPos, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        float currentTime = 0f;
        while (currentTime <= 1f)
        {
            currentTime += Time.deltaTime;
            float percent = Mathf.Clamp01(1 - currentTime / 1f);
            float curvePercent = CharacterClassAnimationCurve.Evaluate(percent);
            rectTransform.localPosition = startPos + (endPos - startPos) * curvePercent;
            yield return null;
        }
    }
}
