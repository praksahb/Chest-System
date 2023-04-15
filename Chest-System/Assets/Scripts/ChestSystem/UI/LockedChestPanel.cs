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

        private float unlockTimeInMinutes;
        private int currentGems;

        private void OnEnable()
        {
            currentGems = ChestSlotService.Instance.Gems;
            UIManager.Instance.UnlockTimeValue += SetUnlockTimeValue;
            startTimerButton.onClick.AddListener(LaunchStartTimerEvent);
            unlockWithGemsButton.onClick.AddListener(LaunchUnlockImmediatelyEvent);
        }
        private void OnDisable()
        {
            startTimerButton.onClick.RemoveAllListeners();
            unlockWithGemsButton.onClick.RemoveAllListeners();
        }

        private void SetUnlockTimeValue(float _unlockTime)
        {
            unlockTimeInMinutes = _unlockTime;
        }

        private void LaunchUnlockImmediatelyEvent()
        {
            Debug.Log($"unlockTime: {unlockTimeInMinutes}");
            Debug.Log($"unlockGem Amounts: {Mathf.Ceil(unlockTimeInMinutes / 10)}");
        }

        private void LaunchStartTimerEvent()
        {
            // start timer event should change state from locked to unlocking
        }

    }
}
