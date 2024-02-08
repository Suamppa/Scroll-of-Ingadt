using UnityEngine;

[CreateAssetMenu(fileName = "Character Data", menuName = "Character Data")]
public class CharacterData : ScriptableObject
{
    // Max health of the entity
    public int maxHealth = 6;
    // Movement speed of the entity
    public float moveSpeed = 10f;
    // Attack speed of the entity as the delay between attacks
    public float attackDelay = 1f;
    // Damage dealt by the entity
    public int damage = 1;
    // Defense is subtracted from incoming damage
    public int defense = 0;
    // Shield prevents hits until depleted
    public int shield = 0;
}
