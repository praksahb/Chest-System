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
        [SerializeField] private int chestSlotItemsCount;
        [SerializeField] private ChestSO chestSO;
        [SerializeField] private int coins;
        [SerializeField] private int gems;

        protected override void Awake()
        {
            base.Awake();
            createChestButton.onClick.AddListener(OnAddChestButtonClick);
        }
        private void Start()
        {
            chestSlotsController = new ChestSlotsController(chestSlot, chestSlotItemsCount, chestSO);
            UIManager.Instance.OnUnlockImmediateClick += LaunchUnlockOnChestView;

        }

        private void LaunchUnlockOnChestView(int chestIndex)
        {
            ChestController chestController = chestSlotsController.FindChest(chestIndex);
            if(chestController != null)
            {
                chestController.ChestView.chestStateManager.SwitchState(chestController.ChestView.chestStateManager.unlockedState);
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
                // can invoke event here
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
                //can invoke event here or
                //can invoke single event for both coins and gems - not preferred
                OnGemChange?.Invoke();
            }
        }

        private ChestSlotsController chestSlotsController;


        public Action NoEmptySlots;
        private void OnAddChestButtonClick()
        {
            chestSlotsController.CreateChest(chestSO);
        }

        // passing unlock time in minutes directly
        public Action<float, int> OpenLockedScreenPanel;
        public void OnChestUnlock(ChestUnlockData chestUnlockData)
        {
            int coins = chestUnlockData.Coins;
            int gems = chestUnlockData.Gems;
            int chestIndex = chestUnlockData.ChestIndex;

            // pass coins and gems value to the reward panel in the UI manager.
            // can use public function or action invoke and subscribed in ui manager to pass the coin and gems value
            UIManager.Instance.ShowRewardPanel(coins, gems);

            // increase coin and gems value in chestSlotService either here or later in the ui manager upon closing the reward panel
            Coins += coins;
            Gems += gems;

            // pass chestIndex in the chestSlotController through the chestSlotsController to replace the chest with an empty chest slot
            // maybe it would be better to call this function separately from chestCollectedState.OnEnterState()
            chestSlotsController.ReplaceWithEmptyChest(chestSO, chestIndex);
        }

    }
}
