using ChestSystem.ChestSlot;
using ChestSystem.BaseChest;
using System;
using UnityEngine;
using UnityEngine.UI;
using ChestSystem.UI;

namespace ChestSystem
{
    public enum ChestCurrentState
    {
        None,
        LockedState,
        UnlockingState,
        UnlockedState,
        EnqueuedState,
    }

    public class ChestService : GenericMonoSingleton<ChestService>
    {
        [SerializeField] private GameObject chestSlot;
        [SerializeField] private Button createChestButton;
        [SerializeField] private int chestSlotMaxCount;
        [SerializeField] private int chestQueueSize;
        [SerializeField] private ChestSO chestSO;
        [SerializeField] private int coins;
        [SerializeField] private int gems;

        protected override void Awake()
        {
            base.Awake();
            createChestButton.onClick.AddListener(OnAddChestButtonClick);
        }

        private ChestSlotsController chestSlotsController;

        private void OnEnable()
        {
            OnChestCollect += CollectChestRewards;
        }
        private void Start()
        {
            chestSlotsController = new ChestSlotsController(chestSlot, chestSlotMaxCount, chestSO, chestQueueSize);
            UIManager.Instance.OnUnlockImmediateClick += UnlockChest;
            UnlockOnTimerEnd += UnlockChest;
            UIManager.Instance.StartTimerClickEvent += QueueChestForUnlocking;
        }

        private void OnDisable()
        {
            OnChestCollect -= CollectChestRewards;
        }

        private void OnDestroy()
        {
            UIManager.Instance.OnUnlockImmediateClick -= UnlockChest;
            UnlockOnTimerEnd -= UnlockChest;
            UIManager.Instance.StartTimerClickEvent -= QueueChestForUnlocking;
        }

        //event handler for ChestModel.UnlockTimeInSecond

        public void CountdownTimer(float unlockTime, int chestIndex)
        {
            UIManager.Instance.CountdownTimerEvent?.Invoke(unlockTime, chestIndex);
        }

        //event handler for button click to start timer
        private void QueueChestForUnlocking(int chestIndex)
        {
            if (chestQueueSize > chestSlotsController.GetQueueCount())
            {
                chestSlotsController.QueueChest(chestIndex);
            }
            else
            {
                UIManager.Instance.OnQueueFull?.Invoke();
            }
        }

        //event handler for button click to unlock immediately and
        //public method to handle chest finishing countdown timer

        public Action<int> UnlockOnTimerEnd;
        private void UnlockChest(int chestIndex)
        {
            chestSlotsController.UnlockChest(chestIndex);
            UIManager.Instance.SetReadyText?.Invoke(chestIndex);
        }


        // coins and gems that are there on user
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

        // invoked when chest is clicked in locked and unlocking state
        public Action<float, int, ChestCurrentState> OpenLockedScreenPanel;


        public Action<ChestUnlockData> OnChestCollect;
        private void CollectChestRewards(ChestUnlockData chestUnlockData)
        {
            int coins = chestUnlockData.Coins;
            int gems = chestUnlockData.Gems;
            int chestIndex = chestUnlockData.ChestIndex;

            // pass coins and gems value to the reward panel in the UI manager.
            // can use public function or action invoke and subscribed in ui manager to pass the coin and gems value
            UIManager.Instance.GetRewardValues?.Invoke(coins, gems);
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
