using System.Collections;
using UnityEngine;

namespace ChestSystem.BaseChest
{
    public class ChestUnlockingState : ChestBaseState
    {
        public override ChestCurrentState State { get; } = ChestCurrentState.UnlockingState;

        public override void OnButtonClick(ChestStateManager chest)
        {
            chest.chestView.ChestController.ButtonClicked();
        }

        public override void OnEnterState(ChestStateManager chest)
        {

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
            ChestService.Instance.UnlockOnTimerEnd?.Invoke(chestIndex);
        }
    }
}