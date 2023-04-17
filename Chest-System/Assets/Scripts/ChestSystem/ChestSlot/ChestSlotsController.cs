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
                chestSlotController.InitializeEmptySlot(chestSO.allChests[(int)ChestType.EmptySlot], i);
            }
        }

        public void CreateRandomChest(ChestSO chestSO)
        {
            int maxRangeModifier = 1;
            int chestSOIndex = Random.Range((int)ChestType.Common, (int)ChestType.Legendary + maxRangeModifier);
            chestSlotController.CreateChest(chestSO.allChests[chestSOIndex]);
        }

        public ChestController FindChest(int chestIndex)
        {
            return chestSlotController.GetChestByIndex(chestIndex);
        }

        public void ReplaceWithEmptyChest(ChestSO chestSO, int chestIndex)
        {
            chestSlotController.ReplaceChestByEmpty(chestSO.allChests[(int)ChestType.EmptySlot], chestIndex);
        }
    }
}
