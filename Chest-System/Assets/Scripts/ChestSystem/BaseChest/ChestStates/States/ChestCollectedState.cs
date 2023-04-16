using ChestSystem.BaseChest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.BaseChest
{
    public class ChestCollectedState : ChestBaseState
    {
        public override void OnButtonClick(ChestStateManager chest)
        {
            throw new System.NotImplementedException();
        }

        public override void OnEnterState(ChestStateManager chest)
        {
            Debug.Log("Boohoo... end of state now.");
        }
    }
}
