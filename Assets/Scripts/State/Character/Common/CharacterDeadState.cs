using UnityEngine;

public class CharacterDeadState : MonoBehaviour, ICharacterState
{
    private CharacterController _controller;
    public void Handle(IController controller)
    {
        if (!_controller)
        {
            _controller = (CharacterController)controller;
            
        } 
        Debug.Log("Dead");
    }
}