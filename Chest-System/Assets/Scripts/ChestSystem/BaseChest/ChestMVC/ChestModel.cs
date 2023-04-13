
namespace ChestSystem.BaseChest
{
    public class ChestModel
    {
        public float UnlockTimeInSecond { get; }
        public float UnlockTimeInMinute { get; }

        public ChestType ChestType { get; }
        public string ChestName { get; }

        private int coins;
        private int gems;

        public int Coins
        {
            get
            {
                return coins;
            }
        }

        public int Gems
        {
            get
            {
                return gems;
            }
        }

        public ChestModel(BaseChest chestSO)
        {
            ChestName = chestSO.ChestName;
            ChestType = chestSO.chestType;
            coins = chestSO.RandomNumber(chestSO.minCoins, chestSO.maxCoins);
            gems = chestSO.RandomNumber(chestSO.minGems, chestSO.maxGems);
            UnlockTimeInSecond = chestSO.unlockDurationInSeconds;
            UnlockTimeInMinute = chestSO.unlockDurationInSeconds / 60;

        }
    }
}
