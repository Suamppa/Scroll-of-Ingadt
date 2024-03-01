using UnityEngine;

public class ChangePlayerSkin : MonoBehaviour
{
    private void OnEnable()
    {
        SetPlayerSkin();
    }

    public void SetPlayerSkin()
    {
        CharacterModel characterModel = gameObject.GetComponent<Stats>().characterData.characterModels[PlayerPrefs.GetInt("selectedCharacter")];
        if (characterModel == null)
        {
            if (Debug.isDebugBuild)
            {
                Debug.LogError("No character model found");
            }
            return;
        }

        gameObject.GetComponent<SpriteRenderer>().sprite = characterModel.characterSprite;
        gameObject.GetComponent<Animator>().runtimeAnimatorController = characterModel.characterController;
    }
}
