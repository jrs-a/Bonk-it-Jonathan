using UnityEngine;
using UnityEngine.Tilemaps;

public class Movable : MonoBehaviour
{
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap collisionTilemap;

    private void moveTo(Vector2 dir) {
        Move(dir);
    }
    
    private void canYouMove(Vector2 dir) {
        GameManager.instance.playerCanGoTo = CanMove(dir);
    }

    private void Move(Vector2 direction) {
        if (CanMove(direction))
            transform.position += (Vector3)direction;
    }

    private bool CanMove(Vector2 direction) {
        Vector3Int gridPosition = groundTilemap.WorldToCell(transform.position + (Vector3)direction);
        Collider2D coll =  Physics2D.OverlapPoint((Vector2)transform.position+direction);

        if (coll) { //there's a stuff on the position im going
            if(coll.tag == "Step_activator") {
                coll = null;
                return true;
            }
            coll = null;
            return false;
        }
        else if (!groundTilemap.HasTile(gridPosition) || collisionTilemap.HasTile(gridPosition))
            return false; 
        return true;
    }
}