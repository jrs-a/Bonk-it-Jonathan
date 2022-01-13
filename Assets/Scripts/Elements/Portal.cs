using UnityEngine;

public class Portal : Collidable
{
    protected override void OnCollide(Collider2D coll)
    {
        if (GameManager.instance.activated == false) { //instance refers to whether this portal has been activated to collide doesn't carry on to other scene
            if (coll.name == "Player")
            {
                Debug.Log("look im colliding with the portal");
                Object.Destroy(this);
                GameManager.instance.levelLoader.showLevelCompleteScreen();
                GameManager.instance.activated = true;
            }
        }
    }
}