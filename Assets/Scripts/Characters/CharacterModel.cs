using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Model", menuName = "Character/Character Model")]
public class CharacterModel : ScriptableObject
{
    public Sprite characterSprite;
    public AnimatorController characterController;
}
