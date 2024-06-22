using System.Collections;
using UnityEngine;

public class PlayerController : CharacterController
{
    protected override void Start()
    {
        base.Start();
        Destroy(_moveState as CharacterMoveState);
        _moveState = gameObject.AddComponent<PlayerMoveState>();
        StartCoroutine(PlayerControl());
    }

    IEnumerator PlayerControl()
    {
        while (true)
        {
            yield return null;
        }
    }
}