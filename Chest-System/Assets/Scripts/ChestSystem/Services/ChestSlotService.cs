using ChestSystem.ChestSlot;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem
{
    public class ChestSlotService : GenericMonoSingleton<ChestSlotService>
    {
        [SerializeField] private GameObject chestSlot;
        [SerializeField] private Button createChestButton;
        [SerializeField] private int chestSlotItemsCount;
        [SerializeField] private BaseChest.ChestSO chestSO;
        [SerializeField] private int coins;
        [SerializeField] private int gems;

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

        private int chestSlotFilledCount;

        protected override void Awake()
        {
            base.Awake();
            createChestButton.onClick.AddListener(OnAddChestButtonClick);
        }

        private void Start()
        {
            chestSlotsController = new ChestSlotsController(chestSlot, chestSlotItemsCount, chestSO);
        }

        public Action OpenSlotsFullPanel;
        private void OnAddChestButtonClick()
        {
            if (chestSlotFilledCount == chestSlotItemsCount)
            {
                // make the SlotFullPanel active
                Debug.Log("Slot is full");
                OpenSlotsFullPanel?.Invoke();
                return;
            }
            chestSlotsController.CreateChest(chestSO);
            chestSlotFilledCount++;
        }


    }
}
