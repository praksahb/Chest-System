using System;
using System.Collections;
using System.Collections.Generic;
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
            currentGems = ChestSlotService.Instance.Gems;
            UIManager.Instance.UnlockTimeValue += UpdateLockedChestInfo;
            startTimerButton.onClick.AddListener(LaunchStartTimerEvent);
            unlockWithGemsButton.onClick.AddListener(LaunchUnlockImmediatelyEvent);
        }
        private void OnDisable()
        {
            UIManager.Instance.UnlockTimeValue -= UpdateLockedChestInfo;
            startTimerButton.onClick.RemoveAllListeners();
            unlockWithGemsButton.onClick.RemoveAllListeners();
        }

        private void UpdateLockedChestInfo(float _unlockTime, int chestIndex)
        {
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
                ChestSlotService.Instance.Gems -= requiredGems;
                UIManager.Instance.CloseParentPanel();
            } 
            else
            {
                // pop up new window saying gems is not enough.
                Debug.Log("Gems not enough");
            }
        }

        private void LaunchStartTimerEvent()
        {
            UIManager.Instance.StartTimerForUnlock?.Invoke(currentChestIndex);
            UIManager.Instance.CloseParentPanel();

        }

    }
}
