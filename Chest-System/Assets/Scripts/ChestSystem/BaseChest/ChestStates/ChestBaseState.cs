
namespace ChestSystem.BaseChest
{
    public abstract class ChestBaseState
    {
        public abstract void OnEnterState(ChestStateManager chest);

        public abstract void OnButtonClick(ChestStateManager chest);


    }
}
