using TMPro;
using UnityEngine;

public class BasicStat : MonoBehaviour, IStat
{
    public TMP_Text StatText;

    public void SetStat(int value)
    {
        StatText.text = value.ToString();
    }
}
