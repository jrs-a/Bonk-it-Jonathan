using UnityEngine;

public class player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta; //for movement :D
    private RaycastHit2D hit;

    private void Start() //at the start of the game, this runs
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate() //every frame of the game, this runs
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moveDelta = new Vector3(x, y, 0);

        //swap sprite direction
        if (moveDelta.x > 0)
            transform.localScale = Vector3.one;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 1); //idk but this corresponds to the scale property of the sprite


        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            //making jonathan move y
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);

            //Vector2 dir = new Vector2(4,3);

            //transform.position = transform.position + new Vector3(0,y*40,0);

        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            //making jonathan move x
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }

    }
}
