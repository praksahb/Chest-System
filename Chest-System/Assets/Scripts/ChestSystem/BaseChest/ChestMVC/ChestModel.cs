
namespace ChestSystem.BaseChest
{
    public class ChestModel
    {
        public float UnlockTimeInSecond { get; }
        public float UnlockTimeInMinute { get; }

        public ChestType ChestType { get; }
        public string ChestName { get; }

        public int Coins { get; }
        public int Gems { get; }

        public ChestModel(BaseChestData chestSO)
        {
            ChestName = chestSO.ChestName;
            ChestType = chestSO.chestType;
            Coins = chestSO.RandomNumber(chestSO.minCoins, chestSO.maxCoins);
            Gems = chestSO.RandomNumber(chestSO.minGems, chestSO.maxGems);
            UnlockTimeInSecond = chestSO.unlockDurationInSeconds;
            UnlockTimeInMinute = chestSO.unlockDurationInSeconds / 60;

        }
    }
}
