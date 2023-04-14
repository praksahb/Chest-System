using ChestSystem.BaseChest;
using UnityEngine;

namespace ChestSystem.ChestSlot
{
    public class ChestSlotsController
    {
        private ChestSlotController chestSlotController;

        public ChestSlotsController(GameObject chestSlot, int chestSlotItemsCount, ChestSO  chestSO)
        {
            chestSlotController = new ChestSlotController(chestSlot);
            for (int i = 0; i < chestSlotItemsCount; i++)
            {
                chestSlotController.AddEmptySlot(chestSO.allChests[(int)ChestType.EmptySlot]);
            }
        }

        // select a index value from 1-4 and assign chestSO to the createChest function in chestSlotController
        public void CreateChest(ChestSO chestSO)
        {
            int chestSOIndex = Random.Range((int)ChestType.Common, (int)ChestType.Legendary);
            chestSlotController.CreateChest(chestSO.allChests[chestSOIndex]);
        }
    }
}
