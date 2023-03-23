using System;
using Moq;
using ToH.BLL;
using ToH.Data;
using ToH.Log;
using ToH.PL;
using ToH.PL.Screens;
using Xunit;

namespace ToH.Tests.Screens;

public class HeroScreenTest
{
    private readonly Mock<IPrinter> _printer;
    private readonly Mock<ILog> _log;
    private readonly HeroScreen _uut;

    public HeroScreenTest()
    {
        _printer = new Mock<IPrinter>(MockBehavior.Strict);
        _log = new Mock<ILog>();
        var hero = new Hero()
        {
            Id = 1,
            Name = "TestHero1"
        };
        _uut = new HeroScreen(hero, _printer.Object, _log.Object);

    }

    [Fact]
    public void ShouldPrintHeroName_WhenInitialized()
    {
        // Arrange
        var seq = new MockSequence();
        _printer.InSequence(seq).Setup(p => p.Clear());
        _printer.InSequence(seq)
            .Setup(p => p.PrintLine(
                It.Is<string>(s => s.Contains("Hero details"))));
        _printer.InSequence(seq)
            .Setup(p => p.PrintLine(
                It.Is<string>(s => s == "")));
        _printer.InSequence(seq)
            .Setup(p => p.PrintLine(
                It.Is<string>(s => s.Contains("Id: 1"))));
        _printer.InSequence(seq)
            .Setup(p => p.PrintLine(
                It.Is<string>(s => s.Contains("Name: TESTHERO1"))));

        // Act
        _uut.Init();
        
        // Assert
        _printer.Verify(p => p.PrintLine(It.IsAny<string>()), Times.Exactly(4));
    }

    [Fact]
    public void ShouldSetScreenToHeroListScreen_WhenEscapeIsCalled()
    {
        // Arrange
        var heroesListScreen = new HeroesListScreen(new Mock<IHeroesController>().Object, _printer.Object, _log.Object);
        var ui = new Mock<IUi>();
        var screenFactory = new Mock<IScreenFactory>();
        screenFactory
            .Setup(sf => sf.CreateScreen(
                It.Is<Type>(t => t == typeof(HeroesListScreen)), 
                It.IsAny<Hero>()))
            .Returns(heroesListScreen);
        ui.Setup(ui => ui.ScreenFactory).Returns(screenFactory.Object);

        // Act
        _uut.Escape(ui.Object);
        
        // Arrange
        ui.VerifySet(ui => ui.SetScreen(It.Is<HeroesListScreen>(hs => hs == heroesListScreen)));
    }
}