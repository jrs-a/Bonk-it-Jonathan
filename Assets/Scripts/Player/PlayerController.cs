using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : Fighter
{
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap collisionTilemap;
    private PlayerMovement controls;
    private int x;
    public Animator animator;

    private void Awake()
    {
        controls = new PlayerMovement();
    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }

    void Start()
    {
        controls.Main.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
    }

    public Vector3 playerPos()
    {
        return transform.position;
    }

    private void Move(Vector2 direction)
    {
        int energy = GameManager.instance.energy;
        x = (int)direction.x;

        if (x > 0)
            transform.localScale = Vector3.one;
        else if (x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        if (energy > 0)
        {
            if (CanMove(direction))
            {
                transform.position += (Vector3)direction;
                GameManager.instance.energy -= 1;
            }
        }
    }

    private bool CanMove(Vector2 direction)
    {
        Vector3Int gridPosition = groundTilemap.WorldToCell(transform.position + (Vector3)direction);
        if (!groundTilemap.HasTile(gridPosition) || collisionTilemap.HasTile(gridPosition))
            return false;
        return true;
    }
}
