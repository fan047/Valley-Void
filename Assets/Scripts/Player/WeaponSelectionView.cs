using UnityEngine;

public class WeaponSelectionView : MonoBehaviour
{
    [SerializeField] private WeaponSelectionModel model;
    [SerializeField] private CanvasGroup dualLaserSlot;
    [SerializeField] private CanvasGroup beamLaserSlot;
    [SerializeField, Range(0f, 1f)] private float inactiveAlpha = 0.35f;

    private void OnEnable()
    {
        if (model == null || dualLaserSlot == null || beamLaserSlot == null)
        {
            Debug.LogError("WeaponSelectionView 的引用未设置完整", this);
            return;
        }

        model.WeaponChanged += Refresh;
        Refresh(model.CurrentWeapon);
    }

    private void OnDisable()
    {
        if (model != null)
            model.WeaponChanged -= Refresh;
    }

    private void Refresh(WeaponType selected)
    {
        dualLaserSlot.alpha =
            selected == WeaponType.DualLaser ? 1f : inactiveAlpha;

        beamLaserSlot.alpha =
            selected == WeaponType.BeamLaser ? 1f : inactiveAlpha;
    }
}