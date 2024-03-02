using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterMenuManager : MonoBehaviour
{
    public CharacterData characterData;
    public int gameStartScene;

    private const string SelectedCharacterKey = "selectedCharacter";

    private CharacterModel[] AvailableCharacters
    {
        get { return characterData.characterModels; }
    }

    private int SelectedOption
    {
        get { return selectedOption; }
        set
        {
            if (value >= AvailableCharacters.Length)
            {
                value = 0;
            }
            else if (value < 0)
            {
                value = AvailableCharacters.Length - 1;
            }
            selectedOption = value;
            UpdateCharacter();
        }
    }

    private int selectedOption;

    private void Awake()
    {
        // Clear the selected character if in debug mode
        if (Debug.isDebugBuild)
        {
            PlayerPrefs.DeleteKey(SelectedCharacterKey);
        }
    }

    private void Start()
    {
        SelectedOption = PlayerPrefs.GetInt(SelectedCharacterKey, 0);
    }

    public void StartGame()
    {
        if (Debug.isDebugBuild)
        {
            Debug.Log("Saving skin " + SelectedOption);
        }
        PlayerPrefs.SetInt(SelectedCharacterKey, SelectedOption);
        SceneManager.LoadScene(gameStartScene);
    }

    public void NextOption()
    {
        SelectedOption++;
    }

    public void PreviousOption()
    {
        SelectedOption--;
    }

    private void UpdateCharacter()
    {
        if (AvailableCharacters.Length > 0)
        {
            gameObject.GetComponent<Image>().sprite = AvailableCharacters[SelectedOption].characterSprite;
        }
    }
}
