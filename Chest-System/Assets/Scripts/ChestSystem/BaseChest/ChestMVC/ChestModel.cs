
using System;

namespace ChestSystem.BaseChest
{
    public class ChestModel
    {
        private float unlockTimeInSecond;

        public Action<float, int> TimeValueChange;
        public float UnlockTimeInSecond
        {
            get { return unlockTimeInSecond; }
            set
            {
                unlockTimeInSecond = value;
                //can invoke more events here.
                TimeValueChange?.Invoke(UnlockTimeInSecond, ChestIndex);
            }
        }

        public float UnlockTimeInMinute
        {
            get
            {
                return unlockTimeInSecond / 60;
            }
        }

        public ChestType ChestType { get; }
        public int ChestIndex { get; }
        public string ChestName { get; }

        public int Coins { get; }
        public int Gems { get; }

        public ChestModel(BaseChestData chestSO, int index)
        {
            ChestIndex = index;
            ChestName = chestSO.ChestName;
            ChestType = chestSO.chestType;
            Coins = chestSO.RandomNumber(chestSO.minCoins, chestSO.maxCoins);
            Gems = chestSO.RandomNumber(chestSO.minGems, chestSO.maxGems);
            unlockTimeInSecond = chestSO.unlockDurationInSeconds;
        }
    }
}
