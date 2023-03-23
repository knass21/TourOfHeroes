using System.Diagnostics;
using ToH.BLL;
using ToH.Log;

namespace ToH.PL.Screens;

public class LoginScreen : Screen
{
    private readonly ISessionController _controller;
    private readonly IPrinter _printer;
    private readonly ILog _log;

    public LoginScreen(ISessionController controller, IPrinter printer, ILog log)
    {
        _controller = controller;
        _printer = printer;
        _log = log;
    }
    
    public override void Init()
    {
        _printer.Clear();
        _printer.PrintLine("Write username:");
    }

    public override void Text(IUi ui, string controllerValue)
    {
        if (string.IsNullOrEmpty(controllerValue))
        {
            Init();
        }
        else
        {
            _controller.Username = controllerValue;
            _log.LogInfo($"LoginScreen.Text: Switching to DashboardScreen with username {controllerValue}");
            Debug.Assert(ui != null, nameof(ui) + " != null");
            ui.SetScreen(ui.ScreenFactory.CreateScreen(typeof(DashboardScreen)));
        }
    }
}