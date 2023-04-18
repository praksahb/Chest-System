using UnityEngine;

namespace ChestSystem.BaseChest
{
    public class ChestUnlockedState : ChestBaseState
    {
        public override ChestCurrentState State { get; } = ChestCurrentState.UnlockedState;

        public override void OnButtonClick(ChestStateManager chest)
        {
            // generate reward and switch to the collected state
            ChestUnlockData chestUnlockData = new ChestUnlockData(chest.chestView.ChestController.ChestModel);
            ChestService.Instance.OnChestCollect?.Invoke(chestUnlockData);
            chest.SwitchState(chest.collectedState);
        }

        public override void OnEnterState(ChestStateManager chest)
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
