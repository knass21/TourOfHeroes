// See https://aka.ms/new-console-template for more information

using ToH.BLL;
using ToH.Data;
using ToH.Log;
using ToH.PL;
using ToH.PL.Screens;

namespace ToH.Application
{
    internal sealed class Program
    {
        public static void Main()
        {
            var db = new HeroesContainer();
            var heroesController = new HeroesController(db);
            var sessionController = new SessionController();
            var printer = new ConsolePrinter();
            using var log = new FileLog("log.txt", append: false);
            var screenFactory = new ScreenFactory(heroesController, sessionController, printer, log);
            var initScreen = screenFactory.CreateScreen(typeof(LoginScreen));

            var controller = new Controller();
            var _ = new Ui(controller, initScreen, log, screenFactory);

            controller.ListenForInput();
        }
    }
}