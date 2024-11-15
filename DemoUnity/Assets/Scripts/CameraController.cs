using UnityEngine;

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


    public SetPositions setPos = SetPositions.NONE;
    public static CameraController Instance;

    private void Awake()
    {
        Instance = this;
        cam = Camera.main;
        if (cam == null)
        {
            Debug.LogWarning("Main Camera not found. Please set the camera tag to 'MainCamera'.");
        }

        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerControls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        playerControls.Player.Scroll.performed += ctx => scrollInput = ctx.ReadValue<float>();
        playerControls.Player.Scroll.canceled += ctx => scrollInput = 0f;

        playerControls.Player.RightClick.performed += ctx => SetWall(playerControls.Player.Position.ReadValue<Vector2>());
        playerControls.Player.LeflClick.performed += ctx => SetPathPositions(playerControls.Player.Position.ReadValue<Vector2>());

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
        // Ajuster la vitesse de déplacement en fonction du zoom
        float adjustedMoveSpeed = moveSpeed * cam.orthographicSize / 10;

        Vector3 direction = new(moveInput.x, moveInput.y, 0);
        transform.Translate(adjustedMoveSpeed * Time.deltaTime * direction);
    }

    private void SetWall(Vector2 mousePos)
    {
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
        Vector2Int gridPos = Generator.Instance.WorldToTilemapPos(worldPos);
        Generator.Instance.TrySetPos(gridPos, TileState.WALL);
    }

    private void SetPathPositions(Vector2 mousePos)
    {
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
        Vector2Int gridPos = Generator.Instance.WorldToTilemapPos(worldPos);
        switch (setPos)
        {
            case SetPositions.NONE:
                Generator.Instance.TrySetPos(gridPos, TileState.NORMAL);
                return;
            case SetPositions.START:
                Generator.Instance.TrySetPos(gridPos, TileState.START);
                UIManager.Instance.SetStartPos(gridPos);
                break;
            case SetPositions.END:
                Generator.Instance.TrySetPos(gridPos, TileState.END);
                UIManager.Instance.SetEndPos(gridPos);
                break;
        }
        setPos = SetPositions.NONE;
    }
}
