using UnityEngine;

namespace ChestSystem.BaseChest
{
    public class ChestLockedState : IChestBaseState
    {
        public ChestCurrentState State { get; } = ChestCurrentState.LockedState;

        public void OnButtonClick(ChestStateManager chest)
        {
            chest.chestView.ChestController.ButtonClicked();
        }

        public void OnEnterState(ChestStateManager chest)
        {
            Debug.Log("The chest has been instantiated in locked state");
        }
    }
}
