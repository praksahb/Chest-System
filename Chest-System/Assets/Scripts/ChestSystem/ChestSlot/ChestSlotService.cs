using ChestSystem.BaseChest;
using UnityEngine;

namespace ChestSystem.ChestSlot
{
    public class ChestSlotService : MonoBehaviour
    {
        private GameObject chestSlot;
        public int chestSlotItemsCount;
        public ChestSO chestSO;

        private ChestSlotController chestSlotController;

        private void Awake()
        {
            chestSlot = this.gameObject;
        }

        private void Start()
        {
            chestSlotController = new ChestSlotController(chestSlot);
            for (int i = 0; i < chestSlotItemsCount; i++)
            {
                chestSlotController.AddEmptySlot2(chestSO.allChests[(int)ChestType.EmptySlot]);
            }
        }
    }
}
