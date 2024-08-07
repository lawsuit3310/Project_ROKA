public class PlayerStopState : CharcterStopState
{
    private PlayerController _controller;
    
    public override void Handle(IController controller)
    {
        if (!_controller)
        {
            _controller = (PlayerController)controller;
        }

        _controller.moveStatistics.Stop();
    }
}