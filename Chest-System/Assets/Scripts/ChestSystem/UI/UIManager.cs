using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.UI
{
    public class UIManager : GenericMonoSingleton<UIManager>
    {
        [SerializeField] private GameObject parentPanel;
        [SerializeField] private RectTransform slotFullPanel;
        [SerializeField] private LockedChestPanel lockedChestPanel;
        [SerializeField] private RewardPanel rewardPanel;
        [SerializeField] private GameObject closePanel;
        [SerializeField] private RectTransform gemNotEnoughPanel;
        [SerializeField] private RectTransform queueFullPanel;

        [SerializeField] private TextMeshProUGUI coinCount;
        [SerializeField] private TextMeshProUGUI gemCount;

        private Button closePanelButton;

        protected override void Awake()
        {
            base.Awake();
            CloseParentPanel();
            closePanelButton = closePanel.GetComponentInChildren<Button>();
            ChangeCoinValue();
            ChangeGemValue();
        }

        private void OnEnable()
        {
            // keep the close button enabled at the start. parent is still inactive so it wont appear
            closePanel.SetActive(true);
            closePanelButton.onClick.AddListener(CloseParentPanel);
            CloseAllPanel += CloseParentPanel;
            ChestService.Instance.OpenLockedScreenPanel += ShowLockedChestPanel;
            ChestService.Instance.NoEmptySlots += ShowSlotFullPanel;
            InsufficientGems += ShowGemNotEnoughPanel;
            OnQueueFull += ShowQueueFullPanel;
            ChestService.Instance.OnCoinChange += ChangeCoinValue;
            ChestService.Instance.OnGemChange += ChangeGemValue;
            GetRewardValues += ShowRewardPanel;
        }
        private void OnDisable()
        {
            closePanelButton.onClick.RemoveListener(CloseParentPanel);
            CloseAllPanel -= CloseParentPanel;
            ChestService.Instance.OpenLockedScreenPanel -= ShowLockedChestPanel;
            ChestService.Instance.NoEmptySlots -= ShowSlotFullPanel;
            InsufficientGems -= ShowGemNotEnoughPanel;
            OnQueueFull -= ShowQueueFullPanel;
            ChestService.Instance.OnCoinChange -= ChangeCoinValue;
            ChestService.Instance.OnGemChange -= ChangeGemValue;
            GetRewardValues -= ShowRewardPanel;
        }

        // Actions handler for changing coin value of current user 
        private void ChangeCoinValue()
        {
            int coins = ChestService.Instance.Coins;
            coinCount.SetText("Coins: {0}", coins);
        }
        // Actions handler for changing gem value of current user
        private void ChangeGemValue()
        {
            int gems = ChestService.Instance.Gems;
            gemCount.SetText("Coins: {0}", gems);
        }

        // can be called whenever closing any panel except closing the close button which will be needed in all panels
        private void DisableAllChildrenExcept(GameObject parentObj, GameObject childToKeepActive)
        {
            foreach (Transform child in parentObj.transform)
            {
                if (child.gameObject != childToKeepActive)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }

        private void ShowSlotFullPanel()
        {
            // first close prev panel if any
            CloseParentPanel();
            parentPanel.SetActive(true);
            slotFullPanel.gameObject.SetActive(true);
        }


        // chestService invokes a event which is handled by below function which will again invoke below action which will be handled in lockedChestPanel upon enabling
        public Action<float, int, ChestCurrentState> ChestClickedUIEvent;
        private void ShowLockedChestPanel(float unlockTimeInMinutes, int chestIndex, ChestCurrentState currentState)
        {
            // first close prev panel if any
            CloseParentPanel();
            parentPanel.SetActive(true);
            lockedChestPanel.gameObject.SetActive(true);
            ChestClickedUIEvent?.Invoke(unlockTimeInMinutes, chestIndex, currentState);
        }

        public Action<int> SetReadyText;
        public Action<int> ClearText;

        // invoked in chestService, subscribed to here, eventHandler - ShowRewardPanel
        public Action<int, int> GetRewardValues;
        // which invokes SendRewardvalues, handled in rewardPanel.cs
        public Action<int, int> SendRewardValues;

        private void ShowRewardPanel(int coins, int gems)
        {
            CloseParentPanel();
            parentPanel.SetActive(true);
            rewardPanel.gameObject.SetActive(true);
            SendRewardValues?.Invoke(coins, gems);
        }

        // invoked in LockedChestPanel,
        public Action InsufficientGems;
        // handler function
        private void ShowGemNotEnoughPanel()
        {
            CloseParentPanel();
            parentPanel.SetActive(true);
            gemNotEnoughPanel.gameObject.SetActive(true);
        }

        // invoked in chestService if queue is full
        public Action OnQueueFull;
        //  handler function
        private async void ShowQueueFullPanel()
        {
            await Task.Delay(1);
            parentPanel.SetActive(true);
            queueFullPanel.gameObject.SetActive(true);
        }

        // subscribed in chestService, Invoked in LockedChestPanel
        public Action<int> OnUnlockImmediateClick;
        public Action<int> StartTimerClickEvent;

        // subscribed in ChestSlotsState.cs - displays timer and other text displaying chest current state
        // invoked in chestService, inside chestService's event handler.
        public Action<float, int> CountdownTimerEvent;


        // subscribed here, invoked in lockedChestPanel, to close the panel after a button click
        public Action CloseAllPanel;
        private void CloseParentPanel()
        {
            if (parentPanel.activeInHierarchy == true)
            {
                DisableAllChildrenExcept(parentPanel, closePanel);
                parentPanel.SetActive(false);
            }
        }
    }
}
