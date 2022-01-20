using UnityEngine;

public class GateActivator : Collidable
{
    private bool activated = false;

    protected override void OnCollide(Collider2D coll) {
        if (coll.name == "Player") {
            if (!activated)
                openGate();
        }
    }

    private void openGate() {
        EventsSystem.current.OpenGate();
        activated = true;
        LeanTween.color(gameObject, new Vector4(255,255,255,0), 1.5f);
    }
    

    private void canYouMove(Vector2 dir) {
        GameManager.instance.playerCanGoTo = true;
    }
}
