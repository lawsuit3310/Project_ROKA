using UnityEngine;
public class CharacterAttackState : MonoBehaviour, ICharacterState
{
    public void Handle(IController controller)
    {
        if (controller is CharacterController)
        {
            var ctrl = (CharacterController)controller;
            
            ctrl.Equip.Fire();
        }
    }
}