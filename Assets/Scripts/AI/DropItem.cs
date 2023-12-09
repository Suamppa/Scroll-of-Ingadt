using UnityEngine;

// This class holds a drop table and handles dropping items
public class DropItem : MonoBehaviour
{
    // The chance to drop any item at all
    public float chanceToDrop = 0.5f;

    // Reference to the drop table
    private DropTable dropTable;

    private void Start() {
        dropTable = GetComponent<DropTable>();
    }

    // Roll for a drop and drop a random item from the drop table
    public void Drop() {
        if (Random.value < chanceToDrop) return;
        GameObject item = dropTable.GetItem();
        if (item != null) {
            Instantiate(item, transform.position, Quaternion.identity);
        }
        Debug.Log(gameObject.name + " dropped " + item.name);
    }
}
