using ChestSystem.BaseChest;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.ChestSlot
{
    public class ChestSlotsController
    {
        private ChestSlotController chestSlotController;
        private Queue<ChestController> chestQueue;


        public ChestSlotsController(GameObject chestSlot, int chestSlotItemsCount, ChestSO chestSO, int chestQueueSize)
        {
            chestSlotController = new ChestSlotController(chestSlot);
            for (int i = 0; i < chestSlotItemsCount; i++)
            {
                chestSlotController.InitializeEmptySlot(chestSO.allChests[(int)ChestType.EmptySlot], i);
            }

            //intialize queue
            chestQueue = new Queue<ChestController>(chestQueueSize);
        }

        public void ReplaceWithEmptyChest(ChestSO chestSO, int chestIndex)
        {
            chestSlotController.ReplaceChestByEmpty(chestSO.allChests[(int)ChestType.EmptySlot], chestIndex);
        }

        public void CreateRandomChest(ChestSO chestSO)
        {
            int maxRangeModifier = 1;
            int chestSOIndex = Random.Range((int)ChestType.Common, (int)ChestType.Legendary + maxRangeModifier);
            chestSlotController.CreateChest(chestSO.allChests[chestSOIndex]);
        }

        private ChestController FindChest(int chestIndex)
        {
            return chestSlotController.GetChestByIndex(chestIndex);
        }

        //queue chest related
        public int GetQueueCount()
        {
            return chestQueue.Count;
        }

        public void QueueChest(int chestIndex)
        {
            ChestController chestController = FindChest(chestIndex);
            if (chestController != null)
            {
                AddToQueue(chestController);
            }
        }

        public void UnlockChest(int chestIndex)
        {
            ChestController chestController = FindChest(chestIndex);

            if (chestController != null)
            {
                if (chestQueue.Contains(chestController))
                {
                    DequeueAction(chestController);
                }
                chestController.UnlockChest();
            }
        }
        private void AddToQueue(ChestController chestController)
        {
            if (chestQueue.Count == 0)
            {
                chestController.StartTimer();
            }

            if (chestQueue.Contains(chestController))
            {
                Debug.Log("Cannot add same element again.");
                return;
            }

            chestQueue.Enqueue(chestController);
            chestController.SwitchToEnqueuedState();
        }

        private void DequeueAction(ChestController _chestController)
        {
            //if first elem then easy peasy
            if (chestQueue.Peek() == _chestController)
            {
                chestQueue.Dequeue();
                ChestController chestController = chestQueue.Peek();
                if (chestController != null)
                {
                    chestController.StartTimer();
                }
            }
            else // filter out
            {
                Queue<ChestController> tempQ = new Queue<ChestController>();
                while (chestQueue.Count > 0)
                {
                    ChestController chestController = chestQueue.Dequeue();
                    if(chestController != _chestController)
                    {
                        tempQ.Enqueue(chestController);
                    }
                }
                chestQueue = tempQ;
            }
        }
    }
}
