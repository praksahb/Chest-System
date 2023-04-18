using UnityEngine;

namespace ChestSystem.BaseChest
{

    // might not be requiring  this state afterall
    public class ChestEnqueuedState : ChestBaseState
    {
        public override ChestCurrentState State { get => ChestCurrentState.EnqueuedState; }

        public override void OnButtonClick(ChestStateManager chest)
        {
            chest.chestView.ChestController.ButtonClicked();
        }

        public override void OnEnterState(ChestStateManager chest)
        {

        }
    }
}
