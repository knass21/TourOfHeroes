using Moq;
using ToH.BLL;
using ToH.Data;
using ToH.Log;
using ToH.PL.Screens;
using Xunit;

namespace ToH.Tests.Integration;

public class DashboardScreenTest
{
    private readonly ListPrinter _printer;
    private readonly DashboardScreen _uui;
    private readonly HeroesContainer _db;
    private readonly HeroesController _heroesController;
    private readonly SessionController _sessionController;


    public DashboardScreenTest()
    {
        _db = new HeroesContainer();
        _heroesController = new HeroesController(_db);
        _sessionController = new SessionController
        {
            Username = "Test"
        };
        _printer = new ListPrinter();
        var log = new Mock<ILog>();
        _uui = new DashboardScreen(_heroesController, _sessionController, _printer, log.Object);
    }

    [Fact]
    public void ShouldShowDashboard()
    {
        _uui.Init();

        // Banner consists of 5 lines.
        var printed = _printer.Lines();
        var heroListBeginning = printed.FindLastIndex(s => s.StartsWith("+++++++"));
        Assert.Equal(4, heroListBeginning);
        
        // Top hero list consists of 3 heroes.   +1 is to exclude string "+++++...."
        Assert.Equal(2, printed.Count - (heroListBeginning + 1));
    }
}

