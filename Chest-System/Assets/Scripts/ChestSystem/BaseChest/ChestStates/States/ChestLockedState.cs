using ChestSystem.BaseChest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.BaseChest
{
    public class ChestLockedState : ChestBaseState
    {
        public override void OnButtonClick(ChestStateManager chest)
        {
            float unlockTimeMins = chest.chestView.ChestController.ChestModel.UnlockTimeInMinute;
            int chestIndex = chest.chestView.ChestController.ChestModel.ChestIndex;
            ChestSlotService.Instance.OpenLockedScreenPanel?.Invoke(unlockTimeMins, chestIndex);
        }

        public override void OnEnterState(ChestStateManager chest)
        {
            Debug.Log("The chest has been instantiated in locked state");
        }

        public override void OnExitState(ChestStateManager chest)
        {
            throw new System.NotImplementedException();
        }

        public override void OnUpdateState(ChestStateManager chest)
        {
            throw new System.NotImplementedException();
        }
    }
}
