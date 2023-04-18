using UnityEngine;

namespace ChestSystem.BaseChest
{

    // might not be requiring  this state afterall
    public class ChestEnqueuedState : IChestBaseState
    {
        public ChestCurrentState State { get => ChestCurrentState.EnqueuedState; }

        public void OnButtonClick(ChestStateManager chest)
        {
            chest.chestView.ChestController.ButtonClicked();
        }

        public void OnEnterState(ChestStateManager chest)
        {

        }
    }
}
