using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.UI
{
    public class LockedChestPanel : MonoBehaviour
    {
        [SerializeField] private Button startTimerButton;
        [SerializeField] private Button unlockWithGemsButton;
        [SerializeField] private int divisionValueForGems = 10;


        private TMPro.TextMeshProUGUI unlockButtonText;
        private float unlockTimeInMinutes;
        private int currentGems;
        private int requiredGems;
        private int currentChestIndex;

        private void Awake()
        {
            unlockButtonText = unlockWithGemsButton.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            currentGems = ChestService.Instance.Gems;
            UIManager.Instance.UnlockTimeValue += UpdateLockedChestInfo;
            UIManager.Instance.UnlockTimeValueInUnlockingState += UpdateLockedChestInfoInUnlockingState;
            startTimerButton.onClick.AddListener(LaunchStartTimerEvent);
            unlockWithGemsButton.onClick.AddListener(LaunchUnlockImmediatelyEvent);
        }
        private void OnDisable()
        {
            UIManager.Instance.UnlockTimeValue -= UpdateLockedChestInfo;
            UIManager.Instance.UnlockTimeValueInUnlockingState -= UpdateLockedChestInfoInUnlockingState;
            startTimerButton.onClick.RemoveAllListeners();
            unlockWithGemsButton.onClick.RemoveAllListeners();
        }

        private void UpdateLockedChestInfo(float _unlockTime, int chestIndex)
        {
            startTimerButton.gameObject.SetActive(true);
            unlockTimeInMinutes = _unlockTime;
            currentChestIndex = chestIndex;
            CalculateRequiredGems();
        }

        private void UpdateLockedChestInfoInUnlockingState(float _unlockTime, int chestIndex)
        {
            startTimerButton.gameObject.SetActive(false);
            unlockTimeInMinutes = _unlockTime;
            currentChestIndex = chestIndex;
            CalculateRequiredGems();
        }

        private void CalculateRequiredGems()
        {
            requiredGems = Mathf.CeilToInt(unlockTimeInMinutes / divisionValueForGems);
            // change text value in button
            unlockButtonText.SetText("Unlock with {0} gems", requiredGems);
        }

        private void LaunchUnlockImmediatelyEvent()
        {
            if(requiredGems <= currentGems)
            {
                // change state of chest to unlocked
                UIManager.Instance.OnUnlockImmediateClick?.Invoke(currentChestIndex);
                ChestService.Instance.Gems -= requiredGems;
                UIManager.Instance.CloseParentPanel();
            } 
            else
            {
                // pop up new window saying gems is not enough.
                UIManager.Instance.ShowGemNotEnoughPanel();
            }
        }

        private void LaunchStartTimerEvent()
        {
            UIManager.Instance.StartTimerClickEvent?.Invoke(currentChestIndex);
            UIManager.Instance.CloseParentPanel();
        }

    }
}
