using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : Fighter
{
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap collisionTilemap;
    public Animator anim;
    private PlayerMovement controls;
    private bool hasDied = false;

    private void Awake() {
        controls = new PlayerMovement();
    }
    private void OnEnable() {
        controls.Enable();
    }
    private void OnDisable() {
        controls.Disable();
    }

    void Start() {
        controls.Main.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
        gameObject.transform.position = GameManager.instance.loadPos;
    }

    void Update() {
        int energy = GameManager.instance.energy;
        if (energy == 0) {
            anim.SetTrigger("isDying");
            if(!hasDied) 
                StartCoroutine(deads());
        }

        if(DialogueManager.GetInstance().dialogueIsPlaying)
            controls.Disable();
        else
            controls.Enable();
    }

    IEnumerator deads() {
        yield return new WaitForSeconds(1.4f);
        gameObject.SetActive(false);
        GameManager.instance.levelLoader.showDeadbolsScreen();
        hasDied = true;
    }

    private void Move(Vector2 direction) {
        int energy = GameManager.instance.energy;
        int x = (int)direction.x;

        if (energy > 0) {
            if (x > 0)
                transform.localScale = Vector3.one;
            else if (x < 0)
                transform.localScale = new Vector3(-1, 1, 1);

            if (CanMove(direction)) {
                transform.position += (Vector3)direction;
                if (energy > 1)     //to prevent walk and die animation happening at the same tile
                    anim.SetTrigger("isWalking");
                GameManager.instance.energy -= 1;   //has to be here to not disrupt walk animation
                EventsSystem.current.PlayerMove();
            }
        }
    }

    private bool CanMove(Vector2 direction) {
        GameManager.instance.playerCanGoTo = true;
        Vector3Int gridPosition = groundTilemap.WorldToCell(transform.position + (Vector3)direction);
        Collider2D coll =  Physics2D.OverlapPoint((Vector2)transform.position+direction);
        
        if(coll) {                                          //is there a collider2d in that direction
            coll.SendMessage("canYouMove", direction);      //set value of playerCanGoTo
            if(GameManager.instance.playerCanGoTo) {
                if (coll.tag == "Movable") 
                    coll.SendMessage("moveTo", direction);
            }
            else if(!GameManager.instance.playerCanGoTo) 
                return false;
        } 
        else if (!groundTilemap.HasTile(gridPosition) || collisionTilemap.HasTile(gridPosition))
            return false;
        return true;
    }
}