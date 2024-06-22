public interface ICharacterContext
{
    void Transition();
    void Transition(ICharacterState state);
}