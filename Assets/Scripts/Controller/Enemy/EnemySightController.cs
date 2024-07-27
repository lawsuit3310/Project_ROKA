using System.Collections;
using UnityEngine;

public class EnemySightController : CharacterSightController
{
    public override IEnumerator Sighting()
    {
        while(true)
        {
            point =
                EnemyController.Player.transform.position
                - Vector3.up * EnemyController.Player.transform.position.y;
            yield return null;
        }
    }
}