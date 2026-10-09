using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameBalanceConfig balanceConfig;

    private Vector2 movement;

    private void Awake()
    {
        if (balanceConfig == null)
        {
            Debug.LogError("PlayerMovement 尚未指定 GameBalanceConfig", this);
            enabled = false;
        }
    }

    private void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    public void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    private void ProcessTranslation()
    {
        PlayerBalanceData player = balanceConfig.player;

        float xOffset = movement.x * player.controlSpeed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -player.xClampRange, player.xClampRange);

        float yOffset = movement.y * player.controlSpeed * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -player.lowerYClampFactor * player.yClampRange, player.yClampRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, 0f);
    }

    private void ProcessRotation()
    {
        PlayerBalanceData player = balanceConfig.player;

        float pitch = -player.controlPitchFactor * movement.y;
        float roll = -player.controlRollFactor * movement.x;
        Quaternion targetRotation = Quaternion.Euler(pitch, 0f, roll);

        transform.localRotation = Quaternion.Lerp(
            transform.localRotation,
            targetRotation,
            player.rotationSpeed * Time.deltaTime
        );
    }
}