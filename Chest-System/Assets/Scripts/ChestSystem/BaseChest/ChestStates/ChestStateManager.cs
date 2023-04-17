using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.BaseChest
{
    public class ChestStateManager : MonoBehaviour
    {
        public ChestView chestView { get; private set; }
        public ChestBaseState CurrentState { get; private set; }
        private Button chestButton;

        public ChestLockedState lockedState = new ChestLockedState();
        public ChestUnlockingState unlockingState = new ChestUnlockingState();
        public ChestUnlockedState unlockedState = new ChestUnlockedState();
        public ChestCollectedState collectedState = new ChestCollectedState();

        private Coroutine startTimerCoroutine;

        private void Start()
        {
            chestView = GetComponent<ChestView>();
            chestButton = GetComponent<Button>();
            CurrentState = lockedState;
            CurrentState.OnEnterState(this);
            chestButton.onClick.AddListener(OnButtonClicked);
        }

        public void SwitchState(ChestBaseState state)
        {
            CurrentState = state;
            state.OnEnterState(this);

            // If the new state is the unlocking state, start the coroutine
            if (state is ChestUnlockingState)
            {
                if(startTimerCoroutine != null)
                {
                    StopCoroutine(startTimerCoroutine);
                }
                startTimerCoroutine = StartCoroutine(((ChestUnlockingState)state).UnlockCoroutine(this));
            }
            // Otherwise, stop the coroutine if it's currently running
            else if (startTimerCoroutine != null)
            {
                StopCoroutine(startTimerCoroutine);
                startTimerCoroutine = null;
            }
        }

        private void OnButtonClicked()
        {
            CurrentState.OnButtonClick(this);
        }
    }
}
