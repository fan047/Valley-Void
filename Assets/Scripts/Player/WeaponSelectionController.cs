using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(WeaponSelectionModel))]
public class WeaponSelectionController : MonoBehaviour
{
    [SerializeField] private GameStateManager gameStateManager;

    private WeaponSelectionModel model;

    private void Awake()
    {
        model = GetComponent<WeaponSelectionModel>();

        // if (gameStateManager == null)
        //     gameStateManager = FindFirstObjectByType<GameStateManager>();

        if (gameStateManager == null)
            Debug.LogError("WeaponSelectionController 未指定 GameStateManager", this);
    }

    private void OnSelectWeapon1(InputValue value)
    {
        TrySelect(value, WeaponType.DualLaser);
    }

    private void OnSelectWeapon2(InputValue value)
    {
        TrySelect(value, WeaponType.BeamLaser);
    }

    private void TrySelect(InputValue value, WeaponType weapon)
    {
        if (!value.isPressed ||
            gameStateManager == null ||
            gameStateManager.CurrentState != GameState.Playing)
            return;

        model.SelectWeapon(weapon);
        Debug.Log($"当前武器：{model.CurrentWeapon}", this);
    }
}
