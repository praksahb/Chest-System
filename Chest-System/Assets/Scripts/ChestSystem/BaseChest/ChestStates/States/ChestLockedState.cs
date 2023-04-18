using UnityEngine;

namespace ChestSystem.BaseChest
{
    public class ChestLockedState : ChestBaseState
    {
        public override ChestCurrentState State { get; } = ChestCurrentState.LockedState;

        public override void OnButtonClick(ChestStateManager chest)
        {
            chest.chestView.ChestController.ButtonClicked();
        }

        public override void OnEnterState(ChestStateManager chest)
        {
            Debug.Log("The chest has been instantiated in locked state");
        }
    }
}
