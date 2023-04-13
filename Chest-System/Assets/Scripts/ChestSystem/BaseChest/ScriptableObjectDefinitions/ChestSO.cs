using UnityEngine;

namespace ChestSystem.BaseChest
{
    [CreateAssetMenu(fileName = "ChestScriptableObject", menuName = "MyMenu/ChestsSO")]
    public class ChestSO : ScriptableObject
    {
        public BaseChestData[] allChests;
    }

    //Enums - can be shifted to a separate file for storing all enums if using more
    public enum ChestType
    {
        EmptySlot,
        Common,
        Rare,
        Epic,
        Legendary,
    }

    // Base Class definition
    [System.Serializable]
    public class BaseChestData
    {
        public ChestView chestViewPrefab;
        public string ChestName;
        public ChestType chestType;
        public float unlockDurationInSeconds;
        public int minCoins;
        public int maxCoins;
        public int minGems;
        public int maxGems;

        public int RandomNumber(int min, int max)
        {
            return Random.Range(min, max);
        }
    }
}

