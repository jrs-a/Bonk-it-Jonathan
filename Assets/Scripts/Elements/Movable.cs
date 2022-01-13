using UnityEngine;
using UnityEngine.Tilemaps;

public class Movable : MonoBehaviour
{
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap collisionTilemap;

    private void moveTo(Vector2 dir) {
        Move(dir);
    }

    private void Move(Vector2 direction)
    {
        if (CanMove(direction))
            transform.position += (Vector3)direction;
    }

    private bool CanMove(Vector2 direction)
    {
        Vector3Int gridPosition = groundTilemap.WorldToCell(transform.position + (Vector3)direction);
        if (!groundTilemap.HasTile(gridPosition) || collisionTilemap.HasTile(gridPosition))
            return false;
        return true;
    }
}