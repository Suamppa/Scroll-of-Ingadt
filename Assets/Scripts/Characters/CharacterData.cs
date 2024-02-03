using UnityEngine;

[CreateAssetMenu(fileName = "Character Data", menuName = "Character Data")]
public class CharacterData : ScriptableObject
{
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    public float MoveSpeed { get; set; }
    public float AttackDelay { get; set; }
    public int Damage { get; set; }
    public int Defense { get; set; }
    public int Shield { get; set; }
}
