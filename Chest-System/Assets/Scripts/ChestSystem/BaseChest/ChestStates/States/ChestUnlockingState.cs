using System.Collections;
using UnityEngine;

namespace ChestSystem.BaseChest
{
    public class ChestUnlockingState : ChestBaseState
    {
        public override void OnButtonClick(ChestStateManager chest)
        {
            chest.chestView.ChestController.ButtonClickedUnlockingState();
        }

        public override void OnEnterState(ChestStateManager chest)
        {
            Debug.Log("timer will start now.");
        }

        public IEnumerator UnlockCoroutine(ChestStateManager chest)
        {
            float timeLeft = chest.chestView.ChestController.ChestModel.UnlockTimeInSecond;
            int chestIndex = chest.chestView.ChestController.ChestModel.ChestIndex;

            WaitForSecondsRealtime waitTime = new(1f);
            while (timeLeft > 0)
            {
                timeLeft -= waitTime.waitTime;
                chest.chestView.ChestController.ChestModel.UnlockTimeInSecond = timeLeft;

                yield return waitTime;
            }
            ChestService.Instance.UnlockChest(chestIndex);
        }
    }
}