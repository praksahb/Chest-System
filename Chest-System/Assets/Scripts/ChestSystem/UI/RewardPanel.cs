using UnityEngine;

namespace ChestSystem.UI
{
    public class RewardPanel : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI coinTextObj;
        [SerializeField] private TMPro.TextMeshProUGUI gemTextObj;


        private void OnEnable()
        {
            UIManager.Instance.SendRewardValues += UpdateRewardPanel;
        }

        private void OnDisable()
        {
            UIManager.Instance.SendRewardValues -= UpdateRewardPanel;
        }

        private void UpdateRewardPanel(int coins, int gems)
        {
            coinTextObj.SetText("Coins: {0}", coins);
            gemTextObj.SetText("Gems: {0}", gems);
        }
    }
}
