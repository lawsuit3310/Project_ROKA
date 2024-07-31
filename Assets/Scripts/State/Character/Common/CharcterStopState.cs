using UnityEngine;

public class CharcterStopState : MonoBehaviour, ICharacterState
{
    private CharacterController _controller;
    public void Handle(IController controller)
    {
        if (!_controller)
        {
            _controller = (CharacterController)controller;
        }

        _controller.MoveStatistics.Reset();
        _controller.MoveStatistics.Stop();
    }
}