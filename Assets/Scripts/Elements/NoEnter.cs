using UnityEngine;

public class NoEnter : Collidable
{
    private void canYouMove(Vector2 dir) {
        GameManager.instance.playerCanGoTo = true;
    }

    protected override void OnCollide(Collider2D coll) {
        if(coll.name == "Player")
            EventsSystem.current.EnterPortal();
    }
}
