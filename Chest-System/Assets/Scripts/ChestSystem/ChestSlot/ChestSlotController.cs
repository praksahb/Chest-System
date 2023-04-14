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

        public void AddEmptySlot(ChestSO chestSO)
        {
            BaseChestData emptySlotSO = chestSO.allChests[(int)ChestType.EmptySlot];
            ChestModel chestModel = new ChestModel(emptySlotSO);
            ChestController chestController = new ChestController(chestModel, emptySlotSO.chestViewPrefab);

            chestController.ChestView.transform.SetParent(chestSlot.transform);
            chestSlotItems.Add(chestController);
        }

        public void AddEmptySlot2(BaseChestData emptySO)
        {
            ChestModel chestModel = new ChestModel(emptySO);
            ChestController chestController = new ChestController(chestModel, emptySO.chestViewPrefab);
            chestController.ChestView.transform.SetParent(chestSlot.transform);
            chestSlotItems.Add(chestController);
        }

        public void ReplaceBoxObject(int index, ChestController newObject)
        {
            if (index >= 0 && index < chestSlotItems.Count)
            {
                ChestController oldObject = chestSlotItems[index];
                newObject.ChestView.transform.SetParent(chestSlot.transform);
                newObject.ChestView.transform.SetSiblingIndex(oldObject.ChestView.transform.GetSiblingIndex());
                Object.Destroy(oldObject.ChestView);
                chestSlotItems[index] = newObject;
            }
        }
    }
}