using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public static InputReader Instance { get; private set; }

    private InputSystem_Actions controls;

    public Vector2 MoveInput { get; private set; }

    public bool JumpPressed { get; private set; }
    public bool MaterializePressed { get; private set; }
    public bool TemporalPressed { get; private set; }
    public bool PausePressed { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        controls = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        controls.Enable();

        controls.Gameplay.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => MoveInput = Vector2.zero;

        controls.Gameplay.Jump.performed += ctx => JumpPressed = true;

        controls.Gameplay.Materialize.performed += ctx => MaterializePressed = true;

        controls.Gameplay.Temporal.performed += ctx => TemporalPressed = true;

        controls.Gameplay.Pause.performed += ctx => PausePressed = true;
    }

    private void LateUpdate()
    {
        JumpPressed = false;
        MaterializePressed = false;
        TemporalPressed = false;
        PausePressed = false;
    }
}