using ChestSystem.BaseChest;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.ChestSlot
{
    public class ChestSlotController
    {
        private GameObject chestSlot;
        private List<ChestController> chestSlotItems = new List<ChestController>();
        public ChestSlotController(GameObject chestSlot)
        {
            if (chestSlot == null)
            {
                Debug.LogError("Chest slot game object is null!");
                return;
            }
            this.chestSlot = chestSlot;
        }
        //intialization
        public void InitializeEmptySlot(BaseChestData emptySO, int index)
        {
            chestSlotItems.Add(CreateChestSetParent(emptySO, index));
        }

        public ChestController GetChestByIndex(int chestIndex)
        {
            if (chestIndex < 0 || chestIndex >= chestSlotItems.Count)
            {
                return null;
            }
            return chestSlotItems[chestIndex];
        }

        public void CreateChest(BaseChestData chestSO)
        {
            for (int i = 0; i < chestSlotItems.Count; i++)
            {
                if (chestSlotItems[i].ChestModel.ChestType == ChestType.EmptySlot)
                {
                    ReplaceChestPrivate(chestSO, i);
                    return;
                }
            }
            ChestService.Instance.NoEmptySlots?.Invoke();
        }

        public void ReplaceChestByEmpty(BaseChestData emptySO, int chestIndex)
        {
            ReplaceChestPrivate(emptySO, chestIndex);
        }

        private void ReplaceChestPrivate(BaseChestData chestSO, int chestIndex)
        {
            ChestController oldChest = chestSlotItems[chestIndex];
            ChestController chestController = CreateChestSetParent(chestSO, chestIndex);
            chestController.ChestView.transform.SetSiblingIndex(oldChest.ChestView.transform.GetSiblingIndex());
            UnityEngine.Object.Destroy(oldChest.ChestView.gameObject);
            chestSlotItems[chestIndex] = chestController;
        }

        private ChestController CreateChestSetParent(BaseChestData chestSO, int index)
        {
            ChestModel chestModel = new ChestModel(chestSO, index);
            ChestController chestController = new ChestController(chestModel, chestSO.chestViewPrefab);
            chestController.ChestView.transform.SetParent(chestSlot.transform);
            return chestController;
        }
    }
}