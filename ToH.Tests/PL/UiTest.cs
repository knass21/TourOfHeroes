using Moq;
using ToH.BLL;
using ToH.Log;
using ToH.PL;
using ToH.PL.Screens;
using Xunit;

namespace ToH.Tests.Screens;

public class UiTest
{
    private readonly Ui _uut;
    private readonly Mock<Controller> _controller;
    private readonly Mock<Screen?> _screen;
    private readonly Mock<ILog> _log;
    private readonly Mock<IScreenFactory> _screenFactory;

    public UiTest()
    {
        _controller = new Mock<Controller>();
        _screen = new Mock<Screen?>();
        _log = new Mock<ILog>();
        _screenFactory = new Mock<IScreenFactory>();
        _uut = new Ui(_controller.Object, _screen.Object, _log.Object, _screenFactory.Object);
    }

    [Fact]
    public void ShouldExecuteInitOne_WhenConstructed()
    {
        var screen = new Mock<Screen?>();
        // Act
        var uut = new Ui(_controller.Object, screen.Object, _log.Object, _screenFactory.Object);

        // Assert
        screen.Verify(screen1 => screen1!.Init(), Times.Once);
    }
    
    [Fact]
    public void ShouldExecuteNoneActionOnScreen_WhenUpdateIsCalledAndControllerActionIsNone()
    {
        // Arrange
        _controller.Setup(controller => controller.Action).Returns(Action.None);
        
        // Act
        _uut.Update();
        
        // Assert
        _screen.Verify(screen => screen!.None(It.Is<Ui>(ui => ui.Equals(_uut))), Times.Once);
    }
    
    [Fact]
    public void ShouldExecuteDownActionOnScreen_WhenUpdateIsCalledAndControllerActionIsDown()
    {
        // Arrange
        _controller.Setup(controller => controller.Action).Returns(Action.Down);
        
        // Act
        _uut.Update();
        
        // Assert
        _screen.Verify(screen => screen!.Down(It.Is<Ui>(ui => ui.Equals(_uut))), Times.Once);
    }
    
    [Fact]
    public void ShouldExecuteUpActionOnScreen_WhenUpdateIsCalledAndControllerActionIsUp()
    {
        // Arrange
        _controller.Setup(controller => controller.Action).Returns(Action.Up);
        
        // Act
        _uut.Update();
        
        // Assert
        _screen.Verify(screen => screen!.Up(It.Is<Ui>(ui => ui.Equals(_uut))), Times.Once);
    }
    
    [Fact]
    public void ShouldExecuteEnterActionOnScreen_WhenUpdateIsCalledAndControllerActionIsEnter()
    {
        // Arrange
        _controller.Setup(controller => controller.Action).Returns(Action.Enter);
        
        // Act
        _uut.Update();
        
        // Assert
        _screen.Verify(screen => screen!.Enter(It.Is<Ui>(ui => ui.Equals(_uut))), Times.Once);
    }

}