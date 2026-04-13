using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class DataReader
{
    private const string StatsJson = "stats.json";
    private const string TextsJson = "texts.json";

    public static Dictionary<Enums.Character, CharacterData> ReadCharacterDataJson()
    {
        string statsJson = File.ReadAllText(Path.Combine(Application.dataPath, StatsJson));
        Dictionary<Enums.Character, CharacterData> characters = JsonConvert.DeserializeObject<Dictionary<Enums.Character, CharacterData>>(statsJson);

        return characters;
    }

    public static Dictionary<Enums.TextCategory, List<string>> ReadTextDataJson()
    {
        string textsJson = File.ReadAllText(Path.Combine(Application.dataPath, TextsJson));
        Dictionary<Enums.TextCategory, List<string>> texts = JsonConvert.DeserializeObject<Dictionary<Enums.TextCategory, List<string>>>(textsJson);

        return texts;
    }
}
