using ChestSystem.BaseChest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.BaseChest
{
    public class ChestUnlockedState : ChestBaseState
    {
        public override void OnButtonClick(ChestStateManager chest)
        {
            // generate reward and switch to the collected state
            ChestUnlockData chestUnlockData = new ChestUnlockData(chest.chestView.ChestController.ChestModel);
            ChestSlotService.Instance.OnChestUnlock(chestUnlockData);
            chest.SwitchState(chest.collectedState);
        }

        public override void OnEnterState(ChestStateManager chest)
        {
            Debug.Log("This is the unlocked state now");
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
