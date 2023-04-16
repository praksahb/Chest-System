using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.BaseChest
{
    public class ChestStateManager : MonoBehaviour
    {
        public ChestView chestView { get; private set; }
        private ChestBaseState currentState;
        private Button chestButton;

        public ChestLockedState lockedState = new ChestLockedState();
        public ChestUnlockingState unlockingState = new ChestUnlockingState();
        public ChestUnlockedState unlockedState = new ChestUnlockedState();
        public ChestCollectedState collectedState = new ChestCollectedState();



        private void Start()
        {
            chestView = GetComponent<ChestView>();
            chestButton = GetComponent<Button>();
            currentState = lockedState;
            currentState.OnEnterState(this);
            chestButton.onClick.AddListener(OnButtonClicked);
        }

        public void SwitchState(ChestBaseState state)
        {
            currentState = state;
            state.OnEnterState(this);
        }

        private void OnButtonClicked()
        {
            Debug.Log($"Curent state: {currentState}");
            currentState.OnButtonClick(this);
        }
    }
}
