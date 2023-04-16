using System.Collections;
using UnityEngine;

namespace ChestSystem.BaseChest
{
    public class ChestUnlockingState : ChestBaseState
    {
        public override void OnButtonClick(ChestStateManager chest)
        {
            float unlockTimeMins = chest.chestView.ChestController.ChestModel.UnlockTimeInMinute;
            int chestIndex = chest.chestView.ChestController.ChestModel.ChestIndex;
            ChestSlotService.Instance.OpenLockedScreenPanel?.Invoke(unlockTimeMins, chestIndex);
        }

        public override void OnEnterState(ChestStateManager chest)
        {
            Debug.Log("timer will start now.");
        }

        public IEnumerator UnlockCoroutine(ChestStateManager chest)
        {
            float timeLeft = chest.chestView.ChestController.ChestModel.UnlockTimeInSecond;

            WaitForSecondsRealtime waitTime = new WaitForSecondsRealtime(1f);
            while (timeLeft > 0)
            {
                timeLeft -= waitTime.waitTime;
                chest.chestView.ChestController.ChestModel.UnlockTimeInSecond = timeLeft;

                yield return waitTime;
            }
            chest.SwitchState(chest.unlockedState);
        }
    }
}
