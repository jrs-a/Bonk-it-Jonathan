using UnityEngine;

public class Portal : Collidable
{
    public int tombsUnlocked, tombsLocked;
    public Animator anim;

    protected override void Start() {
        base.Start();
        EventsSystem.current.onTombUnlock += addTombUnlocked;
    }

    protected override void Update() {
        base.Update();
    }

    //TODO public last level -> last scene where saved is deleted and display "yay!"

    private void addTombUnlocked() {
        tombsUnlocked ++;
        
        if(tombsLocked == tombsUnlocked) {
            EventsSystem.current.OpenPortal();
            anim.SetBool("youShallPass", true);
        }
    }
    protected override void OnCollide(Collider2D coll) {
        if (tombsLocked == tombsUnlocked) {
            if (GameManager.instance.activated == false) { //instance refers to whether this portal has been activated to collide doesn't carry on to other scene
                if (coll.name == "Player") {
                    Debug.Log("look im colliding with the portal");
                    Object.Destroy(this);
                    GameManager.instance.levelLoader.showLevelCompleteScreen();
                    GameManager.instance.activated = true;
                }
            }
        }
    }

    private void canYouMove(Vector2 dir) {
        GameManager.instance.playerCanGoTo = true;
    }
    
    private void OnDestroy() {
        EventsSystem.current.onTombUnlock -= addTombUnlocked;
    }
}