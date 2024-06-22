using System;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class EnemyObjectPool : MonoBehaviour
{
       private IObjectPool<EnemyController> _pool;
 
       public int maxPoolSize = 10;
       public int stackDefaultCapacity = 10;

       public IObjectPool<EnemyController> Pool
       {
              get
              {
                     return _pool ??= new ObjectPool<EnemyController>
                     (CreatePooledItem,
                            OnTakeFromPool,
                            OnReturnedPool,
                            OnDestroyPoolObject,
                            true,
                            stackDefaultCapacity,
                            maxPoolSize);
              }
       }

       private EnemyController CreatePooledItem()
       {
              var go = GameObject.CreatePrimitive(PrimitiveType.Cube);

              var v = go.AddComponent<EnemyController>();

              go.name = "Enemy";
              v.Pool = _pool;
              
              return v;
       }

       private void OnReturnedPool(EnemyController enemy)
       {
              enemy.gameObject.SetActive(false);
       }
       private void OnTakeFromPool(EnemyController enemy)
       {
              enemy.gameObject.SetActive(true);
       }
       private void OnDestroyPoolObject(EnemyController enemy)
       {
              Destroy(enemy.gameObject);
       }

       public void Spawn()
       {
              var amount = Random.Range(1, 10);

              for (int i = 0; i < amount; i++)
              {
                     var enemy = Pool.Get();
                     var pos = Random.insideUnitSphere * 50;
                     pos.y = 0;
                     enemy.transform.position = pos;
              }
       }

       private void OnGUI()
       {
              if (GUILayout.Button("소환"))
              {
                     Spawn();
              }
              if (GUILayout.Button("복귀"))
              {
                     foreach (var enemy in FindObjectsByType<EnemyController>(FindObjectsInactive.Exclude, FindObjectsSortMode.None))
                     {
                            enemy.ReturnToPool();
                     }
              }
       }
}