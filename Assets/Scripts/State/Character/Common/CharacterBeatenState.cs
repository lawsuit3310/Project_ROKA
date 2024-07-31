using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBeatenState : MonoBehaviour, ICharacterState
{
    private CharacterController _controller;
    private Coroutine _coroutine = null;
    private BeatenTimer _timer;
    public void Handle(IController controller)
    {
        if (!_controller)
        {
            _controller = (CharacterController)controller;
        }

        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(Damaged());
        }
        else
        {
            StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(Damaged());
        }
    }
    IEnumerator Damaged()
    {
        //데미지
        _timer ??= gameObject.AddComponent<BeatenTimer>();
        _timer.enabled = true;
        _controller.Stop();
        yield return new WaitForSeconds(0.2f);
        
        //데미지로 인한 경직 해제
        _controller.Move();
        _timer.enabled = false;
        Debug.Log("OnDamaged. Remain Hit Point : " + _controller.Statistics.CurrentHitPoint);
        _coroutine = null;
    }
}