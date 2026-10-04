using System;
using UnityEngine;

public enum WeaponType
{
    DualLaser,
    BeamLaser
}

public class WeaponSelectionModel : MonoBehaviour
{
    public WeaponType CurrentWeapon { get; private set; } = WeaponType.DualLaser;

    public event Action<WeaponType> WeaponChanged;

    public void SelectWeapon(WeaponType weapon)
    {
        if (CurrentWeapon == weapon) return;

        CurrentWeapon = weapon;
        WeaponChanged?.Invoke(CurrentWeapon);
    }
}