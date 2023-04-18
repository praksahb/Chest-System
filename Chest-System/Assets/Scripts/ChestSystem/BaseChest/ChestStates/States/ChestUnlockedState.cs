using UnityEngine;

namespace ChestSystem.BaseChest
{
    public class ChestUnlockedState : IChestBaseState
    {
        public ChestCurrentState State { get; } = ChestCurrentState.UnlockedState;

        public void OnButtonClick(ChestStateManager chest)
        {
            // generate reward
            ChestUnlockData chestUnlockData = new ChestUnlockData(chest.chestView.ChestController.ChestModel);
            // send to chestService
            ChestService.Instance.OnChestCollect?.Invoke(chestUnlockData);
        }

        public void OnEnterState(ChestStateManager chest)
        {
            Debug.Log("This is the unlocked state now");
        }
    }

    public class ChestUnlockData
    {
        public int Coins { get; private set; }
        public int Gems { get; private set; }
        public int ChestIndex { get; private set; }

        public ChestUnlockData(ChestModel chestModel)
        {
            Coins = chestModel.Coins;
            Gems = chestModel.Gems;
            ChestIndex = chestModel.ChestIndex;
        }
    }
}
