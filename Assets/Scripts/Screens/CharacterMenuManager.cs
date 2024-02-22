using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterMenuManager : MonoBehaviour
{
    public CharacterData characterDB;
    public Image entityAnimation;
    private int selectedOption = 0;
    public int gameStartScene;


    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
            Save();
        }
        else
        {
            Load();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameStartScene);
    }

    public void NextOption()
    {
        selectedOption++;
        if (selectedOption >= characterDB.characterModel.Length)
        {
            selectedOption = 0;
        }
        UpdateCharacter(selectedOption);
        Save();
    }

    public void PreviousOption()
    {
        selectedOption--;
        if (selectedOption < 0)
        {
            selectedOption = characterDB.characterModel.Length - 1;
        }
        UpdateCharacter(selectedOption);
        Save();
    }

    private void UpdateCharacter(int selectedOption)
    {
        CharacterModel characterModel = characterDB.GetCharacter(selectedOption);
        gameObject.GetComponent<Image>().sprite = characterModel.characterSprite;
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }

    private void Save()
    {
        if (Debug.isDebugBuild)
        {
            Debug.Log("Saving skin " + selectedOption);
        }
        PlayerPrefs.SetInt("selectedOption", selectedOption);
    }
}