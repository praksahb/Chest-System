using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.BaseChest
{
    public class ChestStateManager : MonoBehaviour
    {
        public ChestView chestView { get; private set; }
        public IChestBaseState CurrentState { get; private set; }
        private Button chestButton;

        public ChestLockedState lockedState = new ChestLockedState();
        public ChestUnlockingState unlockingState = new ChestUnlockingState();
        public ChestEnqueuedState enqueuedState = new ChestEnqueuedState();
        public ChestUnlockedState unlockedState = new ChestUnlockedState();

        private Coroutine startTimerCoroutine;

        private void Start()
        {
            chestView = GetComponent<ChestView>();
            chestButton = GetComponent<Button>();
            CurrentState = lockedState;
            CurrentState.OnEnterState(this);
            chestButton.onClick.AddListener(OnButtonClicked);
        }

        public void SwitchState(IChestBaseState state)
        {
            CurrentState = state;
            state.OnEnterState(this);

            // If the new state is the unlocking state, start the coroutine
            if (state is ChestUnlockingState)
            {
                if (startTimerCoroutine != null)
                {
                    StopCoroutine(startTimerCoroutine);
                }
                startTimerCoroutine = StartCoroutine(((ChestUnlockingState)state).UnlockCoroutine(this));
            }

            // if in unlocked state then stop any coroutine
            if (state is ChestUnlockedState)
            {
                if (startTimerCoroutine != null)
                {
                    StopCoroutine(startTimerCoroutine);
                    startTimerCoroutine = null;
                }
            }
        }

        private void OnButtonClicked()
        {
            CurrentState.OnButtonClick(this);
        }
    }
}
