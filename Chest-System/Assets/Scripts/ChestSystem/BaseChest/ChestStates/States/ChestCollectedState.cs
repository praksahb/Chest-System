using UnityEngine;

namespace ChestSystem.BaseChest
{

    // might not be requiring  this state afterall
    public class ChestCollectedState : ChestBaseState
    {
        public override void OnButtonClick(ChestStateManager chest)
        {
            throw new System.NotImplementedException();
        }

        public override void OnEnterState(ChestStateManager chest)
        {
            Debug.Log("end of state now.");
        }
    }
}
