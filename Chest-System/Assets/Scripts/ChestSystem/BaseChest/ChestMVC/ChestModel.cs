using UnityEngine;

namespace ChestSystem.BaseChest
{
    public class ChestModel
    {

        // chestModel takes input data from chestScriptable object
        public float UnlockTimeInSecond { get;}
        public int UnlockTimeInMinute 
        { 
            get 
            {
                return (int)UnlockTimeInSecond / 60; 
            }

            set
            {
                // 
            }
        }

        private int minCoins;
        private int maxCoins;
        private int minGems;
        private int maxGems;

        public int Coins
        {
            get
            {
                return Random.Range(minCoins, maxCoins);
            }
        }

        public ChestModel(ChestSO chestSO)
        {

        }
    }
}
