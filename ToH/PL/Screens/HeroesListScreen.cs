using System.Diagnostics;
using ToH.BLL;
using ToH.Log;

namespace ToH.PL.Screens;

public class HeroesListScreen : Screen
{
    private readonly IHeroesController _heroesController;
    private readonly IPrinter _printer;
    private readonly ILog _log;

    private int _cursorPosition;

    public HeroesListScreen(IHeroesController heroesController, IPrinter printer, ILog log)
    {
        _heroesController = heroesController;
        _printer = printer;
        _log = log;
    }

    public override void None(IUi ui)
    {
        ShowHeroes();
    }

    public override void Up(IUi ui)
    {
        if (_cursorPosition > 0)
        {
            _cursorPosition -= 1;
        }
        _log.LogDebug($"HeroesListScreen.Up: cursorPosition={_cursorPosition}");
        ShowHeroes();
    }

    public override void Down(IUi ui)
    {
        if (_heroesController.GetAllHeroes().Count - 1 > _cursorPosition)
        {
            _cursorPosition += 1;
        }
        _log.LogDebug($"HeroesListScreen.Down: cursorPosition={_cursorPosition}");
        ShowHeroes();
    }

    public override void Enter(IUi ui)
    {
        _log.LogInfo($"HeroesListScreen.Enter: Switching to HeroScreen for hero in cursorPosition {_cursorPosition}");
        Debug.Assert(ui != null, nameof(ui) + " != null");
        var newScreen = ui.ScreenFactory.CreateScreen(typeof(HeroScreen), _heroesController.GetAllHeroes()[_cursorPosition]);
        if (newScreen != null)
        {
            ui.SetScreen(newScreen);
        }
    }

    public override void Init()
    {
        ShowHeroes();
    }

    private void ShowHeroes()
    {
        var heroes = _heroesController.GetAllHeroes();
        _log.LogDebug($"HeroesListScreen.ShowHeroes: Showing {heroes.Count} heroes.");
        _printer.Clear();

        _printer.PrintLine("   | Id | Name ");
        foreach (var (index, hero) in heroes.Select((value, i) => (i, value)))
        {
            _printer.PrintLine($" {(index == _cursorPosition ? "*" : " ")} | {hero.Id} | {hero.Name.ToUpperInvariant()}");
        }
    } 

}