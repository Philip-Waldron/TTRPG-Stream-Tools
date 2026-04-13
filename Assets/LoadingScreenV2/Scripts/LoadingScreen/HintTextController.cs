using TMPro;
using UnityEngine;

public class HintTextController : MonoBehaviour
{
    public DataManager Data;
    public TMP_Text HintText;
    public Animator Animator;

    public void Start()
    {
        SetTextToCentral();
    }

    public void HideHintText()
    {
        Animator.SetBool("ShowHintText", false);
    }

    public void ShowNextHintText(Enums.TextCategory textCategory)
    {
        SetTextToTopLeft();
        HintText.text = Data.GetNextText(textCategory);
        Animator.SetBool("ShowHintText", true);
    }

    private void SetTextToCentral()
    {
        HintText.alignment = TextAlignmentOptions.Center;
    }

    private void SetTextToTopLeft()
    {
        HintText.alignment = TextAlignmentOptions.TopLeft;
    }
}
