using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ChestSystem.UI
{
    public class UIManager : GenericMonoSingleton<UIManager>
    {
        public GameObject parentPanel;
        public RectTransform slotFullPanel;
        public LockedChestPanel lockedChestPanel;
        public RewardPanel rewardPanel;
        public GameObject closePanel;

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
            EnableChild(closePanel);
            ChestSlotService.Instance.OnCoinChange += ChangeCoinValue;
            ChestSlotService.Instance.OnGemChange += ChangeGemValue;
            ChestSlotService.Instance.NoEmptySlots += ShowSlotFullPanel;
            ChestSlotService.Instance.OpenLockedScreenPanel += ShowLockedChestPanel;
            closePanelButton.onClick.AddListener(CloseParentPanel);
        }
        private void OnDisable()
        {
            ChestSlotService.Instance.OnCoinChange -= ChangeCoinValue;
            ChestSlotService.Instance.OnGemChange -= ChangeGemValue;
            ChestSlotService.Instance.NoEmptySlots -= ShowSlotFullPanel;
            ChestSlotService.Instance.OpenLockedScreenPanel -= ShowLockedChestPanel;
            closePanelButton.onClick.RemoveListener(CloseParentPanel);
        }

        private void ChangeCoinValue()
        {
            int coins = ChestSlotService.Instance.Coins;
            coinCount.SetText("Coins: {0}", coins);
        }

        private void ChangeGemValue()
        {
            int gems = ChestSlotService.Instance.Gems;
            gemCount.SetText("Coins: {0}", gems);
        }

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

        private void EnableChild(GameObject child)
        {
            child.SetActive(true);
        }

        private void ShowSlotFullPanel()
        {
            // first close prev panel if any
            CloseParentPanel();
            parentPanel.SetActive(true);
            slotFullPanel.gameObject.SetActive(true);
        }

        public Action<float, int> UnlockTimeValue;
        private void ShowLockedChestPanel(float unlockTimeInMinutes, int chestIndex)
        {
            // first close prev panel if any
            CloseParentPanel();
            parentPanel.SetActive(true);
            lockedChestPanel.gameObject.SetActive(true);
            UnlockTimeValue?.Invoke(unlockTimeInMinutes, chestIndex);
        }

        public Action<int> SetReadyText;
        public Action<int> ClearText;
        public Action<int, int> GetRewardValues;
        public void ShowRewardPanel(int coins, int gems)
        {
            CloseParentPanel();
            parentPanel.SetActive(true);
            rewardPanel.gameObject.SetActive(true);
            GetRewardValues?.Invoke(coins, gems);
        }

        public Action<int> OnUnlockImmediateClick;
        public Action<int> StartTimerForUnlock;

        public void CloseParentPanel()
        {
            if (parentPanel.activeInHierarchy == true)
            {
                DisableAllChildrenExcept(parentPanel, closePanel);
                parentPanel.SetActive(false);
            }
        }


        public Action<float, int> TimerValueChange;
        public void UpdateTimeRemaining(float timeRemaining, int chestIndex)
        {
            // update the text of the corresponding TextMeshProUGUI element based on the chestIndex
            TimerValueChange?.Invoke(timeRemaining, chestIndex);
        }
    }
}
