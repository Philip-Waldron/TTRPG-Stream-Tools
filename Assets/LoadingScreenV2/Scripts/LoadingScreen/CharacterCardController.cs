using UnityEngine;
using UnityEngine.Events;

public class CharacterCardController : MonoBehaviour
{
    [Header("Character Settings")]
    public Enums.TextCategory CharacterTextCategory;
    [Range(0, 1)]
    public float SelfHintChance = 0.95f;

    [Header("References")]
    public Animator Animator;
    public LoadingCharacterBackground LoadingCharacterBackground;

    [Header("Events")]
    public UnityEvent CharacterTextShowing;
    public UnityEvent CharacterCardHidden;

    public void ShowCharacterCard()
    {
        Animator.SetBool("ShowCharacterCard", true);
    }

    public void HideCharacterCard()
    {
        Animator.SetBool("ShowCharacterCard", false);
    }

    private void CharacterCardFinishedHiding()
    {
        LoadingCharacterBackground.ResetToDefault();
        CharacterCardHidden.Invoke();
    }

    private void CharacterTextStartedShowing()
    {
        CharacterTextShowing.Invoke();
    }
}
