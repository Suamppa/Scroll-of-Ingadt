using UnityEngine;

// Dialogue system based on the article:
// https://gamedevbeginner.com/dialogue-systems-in-unity/
[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/DialogueAsset", order = 1)]
public class DialogueAsset : ScriptableObject
{
    [TextArea]
    public string[] dialogue;
}
