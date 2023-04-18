
namespace ChestSystem.BaseChest
{
    public interface IChestBaseState
    {
        ChestCurrentState State { get; }

        void OnEnterState(ChestStateManager chest);

        void OnButtonClick(ChestStateManager chest);

    }
}