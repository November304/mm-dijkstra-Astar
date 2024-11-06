using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 10f;
    [SerializeField] private float minZoom = 5f;
    [SerializeField] private float maxZoom = 20f;
    [SerializeField] private float moveSpeed = 10f;

    private Camera cam;

    private PlayerControls playerControls;

    private Vector2 moveInput;
    private float scrollInput;

    private void Awake()
    {
        cam = Camera.main;
        if (cam == null)
        {
            Debug.LogWarning("Main Camera not found. Please set the camera tag to 'MainCamera'.");
        }

        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        // Assurez-vous de créer un Input Action Asset et de l'attacher ici


        playerControls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerControls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        playerControls.Player.Scroll.performed += ctx => scrollInput = ctx.ReadValue<float>();
        playerControls.Player.Scroll.canceled += ctx => scrollInput = 0f;

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        HandleZoom();
        HandleMovement();
    }

    private void HandleZoom()
    {
        if (scrollInput != 0)
        {
            float targetZoom = cam.orthographicSize - scrollInput * zoomSpeed * Time.deltaTime;
            cam.orthographicSize = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        }
    }

    private void HandleMovement()
    {
        Vector3 direction = new Vector3(moveInput.x, moveInput.y, 0);
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}
