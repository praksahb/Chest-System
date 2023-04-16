using UnityEngine;

namespace ChestSystem.UI
{
    public class RewardPanel : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI coinTextObj;
        [SerializeField] private TMPro.TextMeshProUGUI gemTextObj;


        private void OnEnable()
        {
            UIManager.Instance.GetRewardValues += TempFuncName;
        }

        private void OnDisable()
        {
            UIManager.Instance.GetRewardValues -= TempFuncName;
        }

        private void TempFuncName(int coins, int gems)
        {
            coinTextObj.SetText("Coins: {0}", coins);
            gemTextObj.SetText("Gems: {0}", gems);
        }
    }
}
