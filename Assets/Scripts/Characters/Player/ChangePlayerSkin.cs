using UnityEngine;

public class ChangePlayerSkin : MonoBehaviour
{
    private int selectedOption;
    
    void Start()
    {
        Load();
        GetSelectedSkin(selectedOption);
    }


    public void GetSelectedSkin(int index)
    {
        CharacterData characterDB = gameObject.GetComponent<PlayerStats>().GetCharacterData();
        if(index <= characterDB.characterModel.Length && index >= 0)
        {
            SetPlayerSkin(characterDB.characterModel[index].characterController);
        }        
    }
    
    private void SetPlayerSkin(RuntimeAnimatorController animator)
    {
        gameObject.GetComponent<Animator>().runtimeAnimatorController = animator;
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
        if (Debug.isDebugBuild)
        {
            Debug.Log("Load skin " + selectedOption);
        }
    }
}
