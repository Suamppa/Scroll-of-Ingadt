using UnityEngine;

// This class defines a drop table for an entity
public class DropTable : MonoBehaviour
{
    // An array of prefabs for the items that can be dropped
    public GameObject[] items = new GameObject[0];

    // An array of the normalized drop rates for the items
    private float[] normalizedDropRates;

    private void Start()
    {
        if (items is null || items.Length == 0) return;
        try
        {
            normalizedDropRates = NormalizeDropRates();
        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("DropTable on " + gameObject.name + " is missing items or drop rates.");
        }
    }

    // Normalize the drop rates so that they add up to 1
    private float[] NormalizeDropRates()
    {
        // Gather and sum up the drop rates
        float[] dropRates = new float[items.Length];
        float sum = 0f;
        for (int i = 0; i < items.Length; i++)
        {
            float dropChance = items[i].GetComponent<Collectable>().dropChance;
            dropRates[i] = dropChance;
            sum += dropChance;
        }

        // Normalize the drop rates
        normalizedDropRates = new float[dropRates.Length];
        for (int i = 0; i < dropRates.Length; i++)
        {
            normalizedDropRates[i] = dropRates[i] / sum;
        }

        return normalizedDropRates;
    }

    // Return a random item from the drop table
    public GameObject GetItem()
    {
        // Get a random number between 0 and 1
        float random = Random.value;

        // Go through the normalized drop rates and subtract them from the random number
        // until the random number is less than 0
        for (int i = 0; i < normalizedDropRates.Length; i++)
        {
            random -= normalizedDropRates[i];
            if (random < 0)
            {
                return items[i];
            }
        }

        // If the random number is still greater than 0 (which should not happen), return null
        return null;
    }
}
