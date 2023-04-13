using UnityEngine;

namespace ChestSystem.BaseChest
{
    public class ChestController
    {
        public ChestController(ChestModel chestModel, ChestView chestView)
        {
            ChestModel = chestModel;
            ChestView = Object.Instantiate(chestView);
            ChestView.ChestController = this;
        }

        public ChestModel ChestModel { get; }
        public ChestView ChestView { get; }
    }
}