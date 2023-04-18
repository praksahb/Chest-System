using UnityEngine;

namespace ChestSystem.BaseChest
{
    public class ChestController
    {
        public ChestController(ChestModel chestModel, ChestView chestView)
        {
            ChestModel = chestModel;
            ChestView = Object.Instantiate(chestView);
            ChestView.ChestController = this;
        }

        public ChestModel ChestModel { get; }
        public ChestView ChestView { get; }

        public void ButtonClicked()
        {
            float unlockTimeMins = ChestModel.UnlockTimeInMinute;
            int chestIndex = ChestModel.ChestIndex;
            ChestCurrentState chestCurrentState = ChestView.chestStateManager.CurrentState.State;
            ChestService.Instance.OpenLockedScreenPanel?.Invoke(unlockTimeMins, chestIndex, chestCurrentState);
        }

        public void StartTimer()
        {
            ChestView.chestStateManager.SwitchState(ChestView.chestStateManager.unlockingState);
            ChestModel.TimeValueChange += ChestService.Instance.CountdownTimer;
        }

        public void UnlockChest()
        {
            if (ChestView.chestStateManager.CurrentState is ChestUnlockingState)
            {
                ChestModel.TimeValueChange -= ChestService.Instance.CountdownTimer;
            }
            ChestView.chestStateManager.SwitchState(ChestView.chestStateManager.unlockedState);
        }
    }
}