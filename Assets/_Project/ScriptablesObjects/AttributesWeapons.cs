using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attributes", menuName = "WeaponSo")]
public class AttributesWeapons : ScriptableObject
{
    public int damage;
    public int maxAmmo;

    public float range;
    public float fireRate;
    public float reloadTime;
}
