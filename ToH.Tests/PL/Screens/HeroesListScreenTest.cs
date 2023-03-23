using System;
using System.Collections.Generic;
using Moq;
using ToH.BLL;
using ToH.Data;
using ToH.Log;
using ToH.PL;
using ToH.PL.Screens;
using Xunit;

namespace ToH.Tests.Screens;


public class HeroesListScreenTest
{
    private readonly HeroesListScreen _uut;
    private readonly Mock<IPrinter> _printer;
    private readonly Mock<IUi> _ui;
    private readonly Mock<ILog> _log;
    private readonly Mock<IHeroesController> _heroesController;

    public HeroesListScreenTest()
    {
        _heroesController = new Mock<IHeroesController>();
        _printer = new Mock<IPrinter>(MockBehavior.Strict);
        _ui = new Mock<IUi>();
        _log = new Mock<ILog>();
        _uut = new HeroesListScreen(_heroesController.Object, _printer.Object, _log.Object);
    }

    [Fact]
    public void ShouldPrintTowLines_WithNoActionAndOneHero()
    {
        // Arrange
        _heroesController.Setup(db => db.GetAllHeroes()).Returns(new List<Hero>()
        {
            new () { Id = 1, Name = "TestHero1"},
        });
        _printer.Setup(p => p.Clear());
        _printer.Setup(p => p.PrintLine(It.IsAny<string>()));

        // Act
        _uut.None(_ui.Object);

        // Assert
        _printer.Verify(printer => printer.PrintLine(
                It.IsAny<string>()),
            Times.Exactly(2));
    }

    [Fact]
    public void ShouldAddCursorOnFirstHero_WhenNoActionIsGiven()
    {
        // Arrange
        _heroesController.Setup(db => db.GetAllHeroes()).Returns(new List<Hero>()
        {
            new () { Id = 1, Name = "TestHero1"},
        });
        var seq = new MockSequence();
        _printer.Setup(p => p.Clear());
        _printer.InSequence(seq).Setup(p => p.PrintLine(It.Is<string>(s => !s.Contains('*'))));
        _printer.InSequence(seq).Setup(p => p.PrintLine(It.Is<string>(s => s.Contains('*'))));

        // Act
        _uut.None(_ui.Object);

        // Assert
        _printer.Verify(p => p.PrintLine(It.Is<string>(s => s.Contains('*'))), Times.Once);
        _printer.Verify(p => p.PrintLine(It.Is<string>(s => !s.Contains('*'))), Times.Once);
    }
    
    [Fact]
    public void ShouldKeepCursorOnFirstHero_WhenDownActionIsGivenOnLastHero()
    {
        // Arrange
        _heroesController.Setup(db => db.GetAllHeroes()).Returns(new List<Hero>()
        {
            new () { Id = 1, Name = "TestHero1"},
        });
        var seq = new MockSequence();
        _printer.Setup(p => p.Clear());
        _printer.InSequence(seq).Setup(p => p.PrintLine(It.Is<string>(s => !s.Contains('*'))));
        _printer.InSequence(seq).Setup(p => p.PrintLine(It.Is<string>(s => s.Contains('*'))));

        // Act
        _uut.Down(_ui.Object);

        // Assert
        _printer.Verify(p => p.PrintLine(It.Is<string>(s => s.Contains('*'))), Times.Once);
        _printer.Verify(p => p.PrintLine(It.Is<string>(s => !s.Contains('*'))), Times.Once);
    }
    
    [Fact]
    public void ShouldKeepCursorOnLastHero_WhenDownActionIsGivenOnFirstHero()
    {
        // Arrange
        _heroesController.Setup(db => db.GetAllHeroes()).Returns(new List<Hero>()
        {
            new () { Id = 1, Name = "TestHero1"},
        });
        var seq = new MockSequence();
        _printer.Setup(p => p.Clear());
        _printer.InSequence(seq).Setup(p => p.PrintLine(It.Is<string>(s => !s.Contains('*'))));
        _printer.InSequence(seq).Setup(p => p.PrintLine(It.Is<string>(s => s.Contains('*'))));

        // Act
        _uut.Up(_ui.Object);

        // Assert
        _printer.Verify(p => p.PrintLine(It.Is<string>(s => s.Contains('*'))), Times.Once);
        _printer.Verify(p => p.PrintLine(It.Is<string>(s => !s.Contains('*'))), Times.Once);
    }
    
    [Fact]
    public void ShouldAddCursorOnFirstHero_WhenNoActionIsGivenAndTwoHeroes()
    {
        // Arrange
        _heroesController.Setup(db => db.GetAllHeroes()).Returns(new List<Hero>()
        {
            new () { Id = 1, Name = "TestHero1"},
            new () { Id = 2, Name = "TestHero2"},
        });
        var seq = new MockSequence();
        _printer.Setup(p => p.Clear());
        _printer.InSequence(seq).Setup(p => p.PrintLine(It.Is<string>(s => !s.Contains('*'))));
        _printer.InSequence(seq).Setup(p => p.PrintLine(It.Is<string>(s => s.Contains('*'))));
        _printer.InSequence(seq).Setup(p => p.PrintLine(It.Is<string>(s => !s.Contains('*'))));

        // Act
        _uut.None(_ui.Object);

        // Assert
        _printer.Verify(p => p.PrintLine(It.Is<string>(s => !s.Contains('*'))), Times.Exactly(2));
        _printer.Verify(p => p.PrintLine(It.Is<string>(s => s.Contains('*'))), Times.Once);
    }

    [Fact]
    public void ShouldShowHeroNameInUppercase()
    {
        // Arrange
        _heroesController.Setup(db => db.GetAllHeroes()).Returns(new List<Hero>()
        {
            new () { Id = 1, Name = "TestHero1"},
        });
        _printer.Setup(p => p.Clear());
        _printer.Setup(p => p.PrintLine(It.IsAny<string>()));

        // Act
        _uut.None(_ui.Object);
        
        // Assert
        _printer.Verify(printer => printer.PrintLine(
                It.Is<string>(s => s.Contains("TESTHERO1"))), 
            Times.Exactly(1));   
    }

    [Fact]
    public void ShouldAddCursorSecondFirstHero_WhenDowActionIsGivenWithTwoHeroes()
    {
        // Arrange
        _heroesController.Setup(db => db.GetAllHeroes()).Returns(new List<Hero>()
        {
            new () { Id = 1, Name = "TestHero1"},
            new () { Id = 2, Name = "TestHero2"},
        });
        var seq = new MockSequence();
        _printer.Setup(p => p.Clear());
        _printer.InSequence(seq).Setup(p => p.PrintLine(It.Is<string>(s => !s.Contains('*'))));
        _printer.InSequence(seq).Setup(p => p.PrintLine(It.Is<string>(s => !s.Contains('*'))));
        _printer.InSequence(seq).Setup(p => p.PrintLine(It.Is<string>(s => s.Contains('*'))));

        // Act
        _uut.Down(_ui.Object);

        // Assert
        _printer.Verify(p => p.PrintLine(It.Is<string>(s => !s.Contains('*'))), Times.Exactly(2));
        _printer.Verify(p => p.PrintLine(It.Is<string>(s => s.Contains('*'))), Times.Once);
    }
    
    [Fact]
    public void ShouldKeepCursorSecondFirstHero_WhenDowActionIsGivenTwiceWithTwoHeroes()
    {
        // Arrange
        _heroesController.Setup(db => db.GetAllHeroes()).Returns(new List<Hero>()
        {
            new () { Id = 1, Name = "TestHero1"},
            new () { Id = 2, Name = "TestHero2"},
        });
        var seq = new MockSequence();
        _printer.Setup(p => p.Clear());
        _printer.InSequence(seq).Setup(p => p.PrintLine(It.Is<string>(s => !s.Contains('*'))));
        _printer.InSequence(seq).Setup(p => p.PrintLine(It.Is<string>(s => !s.Contains('*'))));
        _printer.InSequence(seq).Setup(p => p.PrintLine(It.Is<string>(s => s.Contains('*'))));
        _printer.InSequence(seq).Setup(p => p.PrintLine(It.Is<string>(s => !s.Contains('*'))));
        _printer.InSequence(seq).Setup(p => p.PrintLine(It.Is<string>(s => !s.Contains('*'))));
        _printer.InSequence(seq).Setup(p => p.PrintLine(It.Is<string>(s => s.Contains('*'))));

        // Act
        _uut.Down(_ui.Object);
        _uut.Down(_ui.Object);

        // Assert
        _printer.Verify(p => p.PrintLine(It.Is<string>(s => !s.Contains('*'))), Times.Exactly(4));
        _printer.Verify(p => p.PrintLine(It.Is<string>(s => s.Contains('*'))), Times.Exactly(2));
    }
    
    [Fact]
    public void ShouldMoveCursorToSecondAndBackToFirstHero_WhenDowAndUpActionsIsGivenWithTwoHeroes()
    {
        // Arrange
        _heroesController.Setup(db => db.GetAllHeroes()).Returns(new List<Hero>()
        {
            new () { Id = 1, Name = "TestHero1"},
            new () { Id = 2, Name = "TestHero2"},
        });
        var seq = new MockSequence();
        _printer.Setup(p => p.Clear());
        _printer.InSequence(seq).Setup(p => p.PrintLine(It.Is<string>(s => !s.Contains('*'))));
        _printer.InSequence(seq).Setup(p => p.PrintLine(It.Is<string>(s => !s.Contains('*'))));
        _printer.InSequence(seq).Setup(p => p.PrintLine(It.Is<string>(s => s.Contains('*'))));
        _printer.InSequence(seq).Setup(p => p.PrintLine(It.Is<string>(s => !s.Contains('*'))));
        _printer.InSequence(seq).Setup(p => p.PrintLine(It.Is<string>(s => s.Contains('*'))));
        _printer.InSequence(seq).Setup(p => p.PrintLine(It.Is<string>(s => !s.Contains('*'))));

        // Act
        _uut.Down(_ui.Object);
        _uut.Up(_ui.Object);

        // Assert
        _printer.Verify(p => p.PrintLine(It.Is<string>(s => !s.Contains('*'))), Times.Exactly(4));
        _printer.Verify(p => p.PrintLine(It.Is<string>(s => s.Contains('*'))), Times.Exactly(2));
    }

    [Fact]
    public void ShouldSetScreenOnUiToHeroScreen_WhenEnterActionIsGiven()
    {
        // Arrange
        var hero = new Hero() { Id = 1, Name = "TestHero1" };
        var heroScreen = new HeroScreen(hero, new Mock<IPrinter>().Object, _log.Object);
        _heroesController.Setup(db => db.GetAllHeroes()).Returns(new List<Hero>()
        {
            hero,
        });
        var screenFactory = new Mock<IScreenFactory>();
        screenFactory
            .Setup(sf => sf.CreateScreen(
                    It.Is<Type>(t => t == typeof(HeroScreen)), 
                    It.IsAny<Hero>()))
            .Returns(heroScreen);
        _ui.Setup(ui => ui.ScreenFactory).Returns(screenFactory.Object);
        
        // Act
        _uut.Enter(_ui.Object);
        
        // Assert
        _ui.VerifySet(ui => ui.SetScreen(It.Is<HeroScreen>(hs => hs == heroScreen)));
    }

}