using ChestSystem.ChestSlot;
using ChestSystem.BaseChest;
using System;
using UnityEngine;
using UnityEngine.UI;
using ChestSystem.UI;

namespace ChestSystem
{
    public class ChestSlotService : GenericMonoSingleton<ChestSlotService>
    {
        [SerializeField] private GameObject chestSlot;
        [SerializeField] private Button createChestButton;
        [SerializeField] private int chestSlotMaxCount;
        [SerializeField] private ChestSO chestSO;
        [SerializeField] private int coins;
        [SerializeField] private int gems;

        protected override void Awake()
        {
            base.Awake();
            createChestButton.onClick.AddListener(OnAddChestButtonClick);
        }

        private ChestSlotsController chestSlotsController;
        private void Start()
        {
            chestSlotsController = new ChestSlotsController(chestSlot, chestSlotMaxCount, chestSO);
            UIManager.Instance.OnUnlockImmediateClick += LaunchUnlockOnChestView;
            UIManager.Instance.StartTimerForUnlock += LaunchStartTimerEvent;
        }

        private void LaunchStartTimerEvent(int chestIndex)
        {
            ChestController chestController = chestSlotsController.FindChest(chestIndex);
            if(chestController != null)
            {
                chestController.ChestView.chestStateManager.SwitchState(chestController.ChestView.chestStateManager.unlockingState);
                chestController.ChestModel.TimeValueChange += OnTimeValueChange;
            }
        }

        private void OnTimeValueChange(float unlockTime, int chestIndex)
        {
            UIManager.Instance.UpdateTimeRemaining(unlockTime, chestIndex);
        }

        private void LaunchUnlockOnChestView(int chestIndex)
        {
            ChestController chestController = chestSlotsController.FindChest(chestIndex);
            if(chestController != null)
            {
                if(chestController.ChestView.chestStateManager.CurrentState is ChestUnlockingState)
                {
                    chestController.ChestModel.TimeValueChange -= OnTimeValueChange;
                }
                chestController.ChestView.chestStateManager.SwitchState(chestController.ChestView.chestStateManager.unlockedState);
                UIManager.Instance.SetReadyText?.Invoke(chestIndex);
            }
        }

        public Action OnCoinChange;
        public int Coins
        {
            get
            {
                return coins;
            }
            set
            {
                coins = value;
                OnCoinChange?.Invoke();
            }
        }
        public Action OnGemChange;
        public int Gems
        {
            get
            {
                return gems;
            }
            set
            {
                gems = value;
                OnGemChange?.Invoke();
            }
        }



        public Action NoEmptySlots;
        private void OnAddChestButtonClick()
        {
            chestSlotsController.CreateRandomChest(chestSO);
        }

        // called inside unlocked state when button is clicked
        public Action<float, int> OpenLockedScreenPanel;
        public void OnChestUnlock(ChestUnlockData chestUnlockData)
        {
            int coins = chestUnlockData.Coins;
            int gems = chestUnlockData.Gems;
            int chestIndex = chestUnlockData.ChestIndex;

            // pass coins and gems value to the reward panel in the UI manager.
            // can use public function or action invoke and subscribed in ui manager to pass the coin and gems value
            UIManager.Instance.ShowRewardPanel(coins, gems);
            UIManager.Instance.ClearText?.Invoke(chestIndex);

            // increase coin and gems value in chestSlotService either here or later in the ui manager upon closing the reward panel
            Coins += coins;
            Gems += gems;

            // pass chestIndex in the chestSlotController through the chestSlotsController to replace the chest with an empty chest slot
            // maybe it would be better to call this function separately from chestCollectedState.OnEnterState()
            chestSlotsController.ReplaceWithEmptyChest(chestSO, chestIndex);
        }

    }
}
