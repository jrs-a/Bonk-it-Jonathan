using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : Fighter
{
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap collisionTilemap;
    public Animator anim;
    private PlayerMovement controls;
    float timer = 0;
    bool timerReached = false;

    //for moving barrels
    private BoxCollider2D boxCollider;
    public ContactFilter2D filter;
    private Collider2D[] hits = new Collider2D[10];
    private Vector2 dir;
    private bool moveBoxes;

    private void Awake() {
        controls = new PlayerMovement();
    }
    private void OnEnable() {
        controls.Enable();
    }
    private void OnDisable() {
        controls.Disable();
    }

    void Start()
    {
        controls.Main.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
        gameObject.transform.position = GameManager.instance.loadPos;

        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            OnCollide(hits[i]);
            hits[i] = null;
        }
    }
    void FixedUpdate()
    {
        int energy = GameManager.instance.energy;

        if (energy == 0)
        {
            anim.SetTrigger("isDying");

            if (!timerReached)
                timer += Time.deltaTime;
            if (!timerReached && timer > 1.4)
            {
                gameObject.SetActive(false);
                GameManager.instance.levelLoader.showDeadbolsScreen();
                
                timerReached = true;
            }
        }
    }

    private void Move(Vector2 direction)
    {
        int energy = GameManager.instance.energy;
        int x = (int)direction.x;
        dir = direction;

        if (energy > 0)
        {
            if (x > 0)
                transform.localScale = Vector3.one;
            else if (x < 0)
                transform.localScale = new Vector3(-1, 1, 1);

            if (CanMove(direction))
            {
                transform.position += (Vector3)direction;
                if (energy > 1)
                    anim.SetTrigger("isWalking");
                    
                GameManager.instance.energy -= 1;
                moveBoxes = true;
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

    private void OnCollide(Collider2D coll)
    {
        if (moveBoxes) {
            if (coll.name == "Barrel")
                coll.SendMessage("moveTo", dir);

            moveBoxes = false;
        }
    }
}