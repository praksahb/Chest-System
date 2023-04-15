using UnityEngine;
using UnityEngine.UI;

namespace ChestSystem.BaseChest
{
    public class ChestView : MonoBehaviour
    {
        private ChestController chestController;
        private Button chestButton;
        public ChestController ChestController
        {
            get
            {
                return chestController;
            }
            set
            {
                chestController = value;
            }
        }

        private void Awake()
        {
            chestButton = GetComponent<Button>();
            Debug.Log(chestButton);
            if(chestButton != null)
            {
                chestButton.onClick.AddListener(OnButtonClicked);
            }
        }

        private void OnButtonClicked()
        {
            ChestSlotService.Instance.OpenLockedScreenPanel?.Invoke(chestController.ChestModel.UnlockTimeInMinute);
        }
    }
}