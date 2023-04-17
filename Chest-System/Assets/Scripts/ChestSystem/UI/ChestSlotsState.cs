using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ChestSystem.UI
{
    public class ChestSlotsState : MonoBehaviour
    {
        private List<TextMeshProUGUI> chestSlotsElements;

        void Awake()
        {
            // Get all child elements of the layout group
            GetChildElements();
        }

        private void OnEnable()
        {
            UIManager.Instance.TimerValueChange += UpdateTimerValueInChild;
            UIManager.Instance.ClearText += ClearTextHere;
            UIManager.Instance.SetReadyText += ChangeTextToReady;
        }

        private void OnDisable()
        {
            UIManager.Instance.TimerValueChange -= UpdateTimerValueInChild;
            UIManager.Instance.ClearText -= ClearTextHere;
            UIManager.Instance.SetReadyText -= ChangeTextToReady;
        }

        private void ClearTextHere(int chestIndex)
        {
            if (chestIndex < 0 || chestIndex >= chestSlotsElements.Count) return;

            chestSlotsElements[chestIndex].SetText("");
        }

        private void ChangeTextToReady(int chestIndex)
        {
            if (chestIndex < 0 || chestIndex >= chestSlotsElements.Count) return;

            chestSlotsElements[chestIndex].SetText("Ready");
        }

        private void UpdateTimerValueInChild(float timeValue, int chestIndex)
        {
            if (chestIndex < 0 || chestIndex >= chestSlotsElements.Count) return;

            TextMeshProUGUI childText = chestSlotsElements[chestIndex];
            childText.SetText("{0}:{1}", (int)(timeValue / 60), timeValue % 60);
        }

        private void GetChildElements()
        {
            chestSlotsElements = new List<TextMeshProUGUI>();
            foreach (Transform child in transform)
            {
                TextMeshProUGUI textElement = child.GetComponent<TextMeshProUGUI>();
                if (textElement != null)
                {
                    chestSlotsElements.Add(textElement);
                }
            }
        }
    }
}
