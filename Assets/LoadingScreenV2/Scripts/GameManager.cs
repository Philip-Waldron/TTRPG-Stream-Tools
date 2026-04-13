using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LoadingScreenController LoadingScreenController;

    void Start()
    {
        LoadingScreenController.ShowNextCharacterCard();
    }

    void Update()
    {

    }
}
