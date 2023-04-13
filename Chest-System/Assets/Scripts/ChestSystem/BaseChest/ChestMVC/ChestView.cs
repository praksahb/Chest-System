using UnityEngine;

namespace ChestSystem.BaseChest
{
    public class ChestView : MonoBehaviour
    {
        private ChestController chestController;

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

    }
}