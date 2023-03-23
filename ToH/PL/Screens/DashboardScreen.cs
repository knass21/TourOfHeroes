using System.Diagnostics;
using ToH.BLL;
using ToH.Data;
using ToH.Log;

namespace ToH.PL.Screens;

public class DashboardScreen : Screen
{
    private readonly IHeroesController _heroesController;
    private readonly ISessionController _sessionController;
    private readonly IPrinter _printer;
    private readonly ILog _log;
    private int _cursorPosition;

    public DashboardScreen(IHeroesController heroesController, ISessionController sessionController, IPrinter printer, ILog log)
    {
        _heroesController = heroesController;
        _sessionController = sessionController;
        _printer = printer;
        _log = log;
    }
    public override void Init()
    {
        DrawDashboard();
    }
    
    public override void Up(IUi ui)
    {
        if (_cursorPosition > 0)
        {
            _cursorPosition -= 1;
        }
        _log.LogDebug($"DashboardScreen.Up: cursorPosition={_cursorPosition}");
        DrawDashboard();
    }

    public override void Down(IUi ui)
    {
        if (_heroesController.GetAllHeroes().Count > _cursorPosition)
        {
            _cursorPosition += 1;
        }
        _log.LogDebug($"DashboardScreen.Down: cursorPosition={_cursorPosition}");
        DrawDashboard();
    }

    public override void Enter(IUi ui)
    {
        if (_cursorPosition == 0)
        {
            _log.LogInfo($"DashboardScreen.Enter: Switching to HeroesListScreen");
            Debug.Assert(ui != null, nameof(ui) + " != null");
            ui.SetScreen(ui.ScreenFactory.CreateScreen(typeof(HeroesListScreen)));
        }
        else
        {
            var heroIndex = _cursorPosition - 1;
            _log.LogInfo($"DashboardScreen.Enter: Switching to HeroScreen for hero with index {heroIndex}");
            // TODO how to go back to right place
            Debug.Assert(ui != null, nameof(ui) + " != null");
            ui.SetScreen(ui.ScreenFactory.CreateScreen(typeof(HeroScreen), _heroesController.GetDashboardHeroes()[heroIndex]));
        }
    }
    
    private void DrawDashboard()
    {
        var heroes = _heroesController.GetDashboardHeroes();
        _log.LogDebug($"DashboardScreen.DrawDashboard: printing {heroes.Count} heroes");
        _printer.Clear();
        _printer.PrintLine($"Welcome: " + _sessionController.Username.ToUpperInvariant());        
        _printer.PrintLine("+++++++++++++++++++++++++");
        _printer.PrintLine("   | GOTO Action / Hero ");
        _printer.PrintLine($" {(0 == _cursorPosition ? "*" : " ")} | Heroes list");
        _printer.PrintLine("+++++++++++++++++++++++++");
        foreach (var (index, hero) in heroes.Select((value, i) => (i, value)))
        {
            _printer.PrintLine($" {(index + 1 == _cursorPosition ? "*" : " ")} | {hero.Name.ToUpperInvariant()}");
        }
    }
}