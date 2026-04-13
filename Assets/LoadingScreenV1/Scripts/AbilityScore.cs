using TMPro;
using UnityEngine;

public class AbilityScore : MonoBehaviour, IStat
{
    public TMP_Text AbilityScoreText;
    public TMP_Text AbilityScoreModifierText;

    public void SetStat(int value)
    {
        AbilityScoreText.text = value.ToString();
        int modifier = (value - 10) / 2;
        if (modifier < 0)
        {
            AbilityScoreModifierText.text = modifier.ToString();
        }
        else
        {
            AbilityScoreModifierText.text = "+" + modifier.ToString();
        }
    }
}
