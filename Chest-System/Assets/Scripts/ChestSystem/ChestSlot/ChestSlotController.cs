using ChestSystem.BaseChest;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem.ChestSlot
{
    public class ChestSlotController
    {
        private GameObject chestSlot;
        private List<ChestController> chestSlotItems = new List<ChestController>();

        private int chestSlotIndex;

        public ChestSlotController(GameObject chestSlot)
        {
            if (chestSlot == null)
            {
                Debug.LogError("Chest slot game object is null!");
                return;
            }
            this.chestSlot = chestSlot;
        }

        public void AddEmptySlot(BaseChestData emptySO)
        {
            chestSlotItems.Add(CreateChestSetParent(emptySO));
        }

        public void CreateChest(BaseChestData chestSO)
        {
            for(int i = 0; i < chestSlotItems.Count; i++)
            {
                if (chestSlotItems[i].ChestModel.ChestType == ChestType.EmptySlot)
                {
                    ChestController oldChest = chestSlotItems[i];
                    ChestController chestController = CreateChestSetParent(chestSO);
                    chestController.ChestView.transform.SetSiblingIndex(oldChest.ChestView.transform.GetSiblingIndex());
                    //oldChest.ChestView.DestroySelf();
                    UnityEngine.Object.Destroy(oldChest.ChestView.gameObject);
                    chestSlotItems[i] = chestController;
                    return;
                }
            }
            //no empty slot in chestSlots - if line can reach here.
            Debug.LogError("It reached");
        }
        private ChestController CreateChestSetParent(BaseChestData chestSO)
        {
            ChestModel chestModel = new ChestModel(chestSO);
            ChestController chestController = new ChestController(chestModel, chestSO.chestViewPrefab);
            chestController.ChestView.transform.SetParent(chestSlot.transform);
            return chestController;
        }
    }
}