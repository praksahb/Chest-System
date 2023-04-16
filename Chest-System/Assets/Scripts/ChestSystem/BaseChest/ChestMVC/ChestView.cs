using UnityEngine;

namespace ChestSystem.BaseChest
{
    public class ChestView : MonoBehaviour
    {
        private ChestController chestController;

        public ChestStateManager chestStateManager { get; private set; }

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
            chestStateManager = GetComponent<ChestStateManager>();
        }
    }
}