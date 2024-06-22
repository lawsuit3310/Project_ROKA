using UnityEngine;

public class CharacterContext : ICharacterContext
{
    
    private readonly CharacterController _controller;
    
    public ICharacterState CurrentState;

    public CharacterContext(CharacterController controller)
    {
        _controller = controller;
    }
    
    public void Transition()
    {
        CurrentState.Handle(_controller);
    }

    public void Transition(ICharacterState state)
    {
        CurrentState = state;
        Transition();
    }
}