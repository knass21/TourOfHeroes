using Moq;
using ToH.BLL;
using ToH.Data;
using ToH.Log;
using ToH.PL;
using ToH.PL.Screens;
using Xunit;

namespace ToH.Tests.Screens;

public class ScreenFactoryTest
{
    private readonly ScreenFactory _uut;
    private readonly Mock<IPrinter> _printer;
    private readonly Mock<IHeroesController> _heroesController;
    private readonly Mock<ILog> _log;
    private readonly Mock<ISessionController> _sessionController;

    public ScreenFactoryTest()
    {
        _heroesController = new Mock<IHeroesController>();
        _sessionController = new Mock<ISessionController>();

        _printer = new Mock<IPrinter>();
        _log = new Mock<ILog>();
        _uut = new ScreenFactory(_heroesController.Object, _sessionController.Object, _printer.Object, _log.Object);
    }
    
    [Fact]
    public void ShouldCreateAHeroListScreen_WhenGivenHeroListScreenType()
    {
        // act
        var screen = _uut.CreateScreen(typeof(HeroesListScreen));
        
        // Assert
        Assert.IsType<HeroesListScreen>(screen);
    }

    [Fact]
    public void ShouldCreateAHeroScreen_WhenGivenHeroScreenType()
    {
        // Arrange
        var hero = new Hero()
        {
            Id = 1,
            Name = "TestHero1"
        };
        
        // Act
        var screen = _uut.CreateScreen(typeof(HeroScreen), hero);

        // Assert
        Assert.IsType<HeroScreen>(screen);
    }
    
    [Fact]
    public void ShouldAttachHeroToHeroScreen_WhenGivenHeroScreenType()
    {
        // Arrange
        var hero = new Hero()
        {
            Id = 1,
            Name = "TestHero1"
        };
        
        // Act
        HeroScreen? screen = _uut.CreateScreen(typeof(HeroScreen), hero) as HeroScreen;

        // Assert
        Assert.Equal(hero, screen?.Hero);
    }
    
    [Fact]
    public void ShouldUpdateHeroScreen_WhenGivenANewHeroScreenType()
    {
        // Arrange
        var hero1 = new Hero()
        {
            Id = 1,
            Name = "TestHero1"
        };
        _uut.CreateScreen(typeof(HeroScreen), hero1);
        var hero2 = new Hero()
        {
            Id = 2,
            Name = "TestHero2"
        };
        
        // Act
        var screen = _uut.CreateScreen(typeof(HeroScreen), hero2) as HeroScreen;


        // Assert
        Assert.Equal(hero2, screen?.Hero);
    }

    [Fact]
    public void ShouldCreateALoginScreen_WhenGivenALoginScreenType()
    {
        // act
        var screen = _uut.CreateScreen(typeof(LoginScreen));
        
        // Assert
        Assert.IsType<LoginScreen>(screen);
    }
}