using System.Collections;
using UnityEngine;

public class LoadingScreenController : MonoBehaviour
{
    public DataManager Data;
    public CharacterCardController[] Characters;
    private int _characterIndex;
    private const float CharacterCardShowDelay = 4;

    public HintTextController HintTextController;

    public void Awake()
    {
        Characters.Shuffle();
    }

    public void ShowNextCharacterCard()
    {
        Characters[_characterIndex].ShowCharacterCard();
    }

    public void ShowNextHintText()
    {
        if (Random.Range(0f, 1f) <= Characters[_characterIndex].SelfHintChance)
        {
            HintTextController.ShowNextHintText(Characters[_characterIndex].CharacterTextCategory);
        }
        else
        {
            HintTextController.ShowNextHintText(Enums.TextCategory.General);
        }

        UpdateNextCharacterReference();
    }

    public void ShowNextCharacterCardDelayed()
    {
        StartCoroutine(DelayNextCharacterCard());
    }

    public IEnumerator DelayNextCharacterCard()
    {
        yield return new WaitForSeconds(CharacterCardShowDelay);
        ShowNextCharacterCard();
    }

    private void UpdateNextCharacterReference()
    {
        _characterIndex++;
        if (_characterIndex == Characters.Length)
        {
            _characterIndex = 0;
            Characters.Shuffle();
        }
    }
}
