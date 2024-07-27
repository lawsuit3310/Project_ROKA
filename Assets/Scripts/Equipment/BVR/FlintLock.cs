using UnityEngine;

public class FlintLock : Equipment
{
    private GameObject _bulletPrefabs;
    public FlintLock(EquipmentConfig config , IController controller) : base(config, controller)
    {
        _bulletPrefabs = Resources.Load<GameObject>("Prefabs/Bullet");
        
        EquipObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        EquipObject.transform.localScale = new Vector3(0.25f, 0.25f, 1f);
        
        EquipObject.transform.SetParent(controller.transform);
        EquipObject.transform.localPosition = new Vector3(0.8f, 1.3f, 0.4f);
        //플린트락 객체에서만 이뤄져야하는 거 (ex 무기 위치 등을 여기서 구현, 무기 공통은 base 생성자에서 
    }

    public override void Fire()
    {
        var bullet = Object.Instantiate(_bulletPrefabs);
        bullet.transform.position = EquipObject.transform.position;
        bullet.AddComponent<Bullet>().Launch(this);
        Debug.Log("플린트락 발사");
    }
}