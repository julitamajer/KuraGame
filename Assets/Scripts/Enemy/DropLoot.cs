using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLoot : MonoBehaviour
{
    [System.Serializable]
    public class LootItem
    {
        public GameObject lootObject;
        public int weight;
    }

    [SerializeField] private List<LootItem> _lootItems = new List<LootItem>();

    private void OnEnable()
    {
        EnemyHP.onEnemyDead += DropLootOnDeath;
    }

    public void DropLootOnDeath()
    {
        if (_lootItems.Count == 0)
        {
            Debug.LogWarning("No loot items assigned to the DropLoot script.");
            return;
        }

        int totalWeight = 0;

        foreach (LootItem item in _lootItems)
        {
            totalWeight += item.weight;
        }

        int randomNumber = Random.Range(0, totalWeight);

        foreach (LootItem item in _lootItems)
        {
            if (randomNumber < item.weight)
            {
                Instantiate(item.lootObject, transform.position, Quaternion.identity);
                return;
            }

            randomNumber -= item.weight;
        }
    }

    private void OnDisable()
    {
        EnemyHP.onEnemyDead -= DropLootOnDeath;
    }
}
