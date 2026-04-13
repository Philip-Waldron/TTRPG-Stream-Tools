using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public Dictionary<Enums.TextCategory, List<string>> Texts = DataReader.ReadTextDataJson();
    public Dictionary<Enums.TextCategory, int> TextIndexes;
    public Dictionary<Enums.Character, CharacterData> Stats = DataReader.ReadCharacterDataJson();

    public void Awake()
    {
        InitTextIndexes();
        ShuffleTexts();
    }

    public string GetNextText(Enums.TextCategory textCategory)
    {
        string text = Texts[textCategory][TextIndexes[textCategory]];
        TextIndexes[textCategory]++;
        if (TextIndexes[textCategory] == Texts[textCategory].Count)
        {
            TextIndexes[textCategory] = 0;
            Texts[textCategory].Shuffle();
        }

        return text;
    }

    private void InitTextIndexes()
    {
        TextIndexes = Texts.ToDictionary(x => x.Key, x => 0);
    }

    private void ShuffleTexts()
    {
        foreach (List<string> textCategory in Texts.Values)
        {
            textCategory.Shuffle();
        }
    }
}
