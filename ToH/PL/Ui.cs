using ToH.BLL;
using ToH.Log;
using ToH.PL.Screens;
using Action = ToH.BLL.Action;

namespace ToH.PL;

public class Ui : IUi, IObserver
{
    private readonly Controller _controller;
    private readonly ILog _log;
    private Screen? _screen;

    private Screen? GetScreen()
    {
        return _screen;
    }

    public void SetScreen(Screen? value)
    {
        _screen = value;
        _screen?.Init();
    }
    public IScreenFactory ScreenFactory { get; }

    public Ui(Controller controller, Screen? screen, ILog log, IScreenFactory screenFactory)
    {
        SetScreen(screen);
        ScreenFactory = screenFactory;
        _controller = controller;
        _log = log;
        _controller.Add(this);
    }

    public void Update()
    {
        switch (_controller.Action)
        {
            case Action.None:
                GetScreen()?.None(this);
                break;
            case Action.Down:
                GetScreen()?.Down(this);
                break;
            case Action.Up:
                GetScreen()?.Up(this);
                break;
            case Action.Enter:
                GetScreen()?.Enter(this);
                break;
            case Action.Escape:
                GetScreen()?.Escape(this);
                break;
            case Action.Text:
                GetScreen()?.Text(this, _controller.Value!);
                break;
            default:
                _log.LogError($"Unhandled action: {_controller.Action}");
                break;
        }
    }
}