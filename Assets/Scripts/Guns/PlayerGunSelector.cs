using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerGunSelector : MonoBehaviour
{
    [SerializeField] private GunType gun;
    [SerializeField] private Transform gunParent;
    [SerializeField] private List<GunScriptableObject> guns;

    [Space]
    [Header("Runtime Filled")]
    public GunScriptableObject activeGun;

    private void Start()
    {
        GunScriptableObject _gun = guns.Find(_gun => _gun.type == gun);

        if (_gun == null)
        {
            Debug.LogError($"No GunScriptableObject found for GunType: {gun}");
            return;
        }

        activeGun = _gun;
        _gun.Spawn(gunParent, this);
    }
}
