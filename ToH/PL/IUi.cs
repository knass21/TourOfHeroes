using ToH.PL.Screens;

namespace ToH.PL
{
    public interface IUi
    {
        void SetScreen(Screen? value);

        IScreenFactory ScreenFactory { get; }
    }
}