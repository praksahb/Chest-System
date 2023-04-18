using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ChestSystem.UI
{
    public class LockedChestPanel : MonoBehaviour
    {
        [SerializeField] private Button startTimerButton;
        [SerializeField] private Button unlockWithGemsButton;
        [SerializeField] private TextMeshProUGUI baseTextLockedPanel;
        [SerializeField] private int divisionValueForGems = 10;


        private TextMeshProUGUI unlockButtonText;
        private float unlockTimeInMinutes;
        private int currentGems;
        private int requiredGems;
        private int currentChestIndex;

        private void Awake()
        {
            unlockButtonText = unlockWithGemsButton.GetComponentInChildren<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            currentGems = ChestService.Instance.Gems;
            UIManager.Instance.ChestClickedUIEvent += UpdatePanel;
            unlockWithGemsButton.onClick.AddListener(LaunchUnlockImmediatelyEvent);
        }
        private void OnDisable()
        {
            UIManager.Instance.ChestClickedUIEvent -= UpdatePanel;
            startTimerButton.onClick.RemoveAllListeners();
            unlockWithGemsButton.onClick.RemoveAllListeners();
        }

        private void UpdatePanel(float _unlockTime, int chestIndex, ChestCurrentState currentState)
        {
            if(currentState == ChestCurrentState.LockedState)
            {
                startTimerButton.gameObject.SetActive(true);
                startTimerButton.onClick.AddListener(LaunchStartTimerEvent);
                baseTextLockedPanel.SetText("Start timer to open chest or unlock with gems immediately.");
            }
            if (currentState == ChestCurrentState.UnlockingState)
            {
                startTimerButton.gameObject.SetActive(false);
                baseTextLockedPanel.SetText("Unlock with gems immediately.");
            }

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
                UIManager.Instance.CloseAllPanel?.Invoke();
            } 
            else
            {
                // pop up new window saying gems is not enough.
                UIManager.Instance.InsufficientGems?.Invoke();
            }
        }

        private void LaunchStartTimerEvent()
        {
            UIManager.Instance.StartTimerClickEvent?.Invoke(currentChestIndex);
            UIManager.Instance.CloseAllPanel?.Invoke();
        }

    }
}
