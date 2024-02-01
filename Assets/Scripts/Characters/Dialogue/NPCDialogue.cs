using TMPro;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField]
    private DialogueAsset dialogueAsset;
    private GameObject dialogueBox;
    private TMP_Text dialogueText;
    private int dialogueIndex = 0;
    
    private void Start() {
        dialogueBox = GetComponentInChildren<Canvas>().gameObject;
        dialogueText = dialogueBox.GetComponentInChildren<TMP_Text>();
        dialogueBox.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (dialogueAsset != null && other.CompareTag("Player"))
        {
            dialogueBox.SetActive(true);
            if (dialogueIndex < dialogueAsset.dialogue.Length)
            {
                dialogueText.text = dialogueAsset.dialogue[dialogueIndex];
                dialogueIndex++;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (dialogueAsset != null && other.CompareTag("Player"))
        {
            dialogueBox.SetActive(false);
        }
    }
}
