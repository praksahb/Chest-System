using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.UI
{
    public class UIManager : MonoBehaviour
    {
        public GameObject parentPanel;
        public GameObject slotFullPanel;
        public GameObject lockedChestPanel;
        public GameObject rewardPanel;
        public GameObject closePanel;

        [SerializeField] private TMPro.TextMeshProUGUI coinCount;
        [SerializeField] private TMPro.TextMeshProUGUI gemCount;

        private Button closePanelButton;

        private void Awake()
        {

            closePanelButton = closePanel.GetComponentInChildren<Button>();
            ChangeCoinValue();
            ChangeGemValue();
        }

        private void OnEnable()
        {
            EnableChild(closePanel);
            ChestSlotService.Instance.OnCoinChange += ChangeCoinValue;
            ChestSlotService.Instance.OnGemChange += ChangeGemValue;
            ChestSlotService.Instance.OpenSlotsFullPanel += SetActiveSlotFull;
            ChestSlotService.Instance.OpenLockedScreenPanel += SetActiveLockedChestScreen;
            closePanelButton.onClick.AddListener(CloseParentPanel);
        }
        private void OnDisable()
        {
            ChestSlotService.Instance.OnCoinChange -= ChangeCoinValue;
            ChestSlotService.Instance.OnGemChange -= ChangeGemValue;
            ChestSlotService.Instance.OpenSlotsFullPanel -= SetActiveSlotFull;
            ChestSlotService.Instance.OpenLockedScreenPanel -= SetActiveLockedChestScreen;
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

        private void SetActiveSlotFull()
        {
            // first close prev panel if any
            CloseParentPanel();
            parentPanel.SetActive(true);
            slotFullPanel.SetActive(true);
        }

        private void SetActiveLockedChestScreen()
        {
            // first close prev panel if any
            CloseParentPanel();
            parentPanel.SetActive(true);
            lockedChestPanel.SetActive(true);
        }

        private void CloseParentPanel()
        {
            if(parentPanel.activeInHierarchy == true)
            {
                DisableAllChildrenExcept(parentPanel, closePanel);
                parentPanel.SetActive(false);
            }
        }
    }
}
