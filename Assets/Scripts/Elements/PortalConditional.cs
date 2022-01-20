using UnityEngine;

public class PortalConditional : Collidable
{
    public int tombsUnlocked, tombsLocked;
    public Animator anim;
    public int portalLocation; //1- east, 2-west, 3-south
    public GameObject curtain;
    private bool truePath;
    private bool winCondition = false;  //if can pass tp to go to next level(win)
    private int phase = 0;
    private bool activated = false;
    

    protected override void Start() {
        base.Start();
        EventsSystem.current.onPortalEnter += allowPortalEntry;
        EventsSystem.current.onTombUnlock += addTombUnlocked;
        EventsSystem.current.onTombLock += resetTombs;
        EventsSystem.current.onPhase0Activate += Phase0;
        EventsSystem.current.onPhase1Activate += Phase1;
        EventsSystem.current.onPhase2Activate += Phase2;
        EventsSystem.current.onPhase3Activate += Phase3;
    }

    private void Phase0() {
        phase = 0;
        Debug.Log("phase 0");
        truePath = false;
    }

    private void Phase1() {
        phase = 1;
        Debug.Log("phase 1");
        switch (portalLocation) {
            case 1:
                truePath = true;
                break;
            case 2:
                truePath = false;
                break;
            case 3:
                truePath = false;
                break;
            default:
                break;
        }
    }

    private void Phase2() {
        Debug.Log("phase2");
        phase = 2;
        switch (portalLocation) {
            case 1:
                truePath = false;
                break;
            case 2:
                truePath = false;
                break;
            case 3:
                truePath = true;
                break;
            default:
                break;
        }
    }

    private void Phase3() {
        Debug.Log("phase 3");
        phase = 3;
        winCondition = true;
        switch (portalLocation) {
            case 1:
                truePath = false;
                break;
            case 2:
                truePath = true;
                break;
            case 3:
                truePath = false;
                break;
            default:
                break;
        }
    }

    private void allowPortalEntry() {
        activated = false;
    }

    private void addTombUnlocked() {
        tombsUnlocked ++;
        if(tombsLocked == tombsUnlocked) {
            EventsSystem.current.OpenPortal();
            anim.SetBool("youShallPass", true);
        }
    }

    private void resetTombs() {
        tombsUnlocked = 0;
        anim.SetBool("youShallPass", false);
    }

    protected override void OnCollide(Collider2D coll) {
        if(!activated) {
            activated = true;
            if (tombsLocked == tombsUnlocked) {
                if (coll.name == "Player") {
                    if (truePath) {
                        if(winCondition)
                            goToNextLevel(coll);
                        else {
                            CloseCurtain();
                            Invoke("goToNextPhase", 1);
                        }
                    } 
                    else{
                        Debug.Log("not true path, resetting phase..");
                        CloseCurtain();
                        Invoke("resetPhase", 1);
                    }
                }
            }
        }
    }

    private void goToNextPhase() {
        GameObject.Find("Player").transform.position = new Vector3(3, -20, 0);
        switch (phase) { //lights only
            case 1:
                EventsSystem.current.BeforePhase1();
                break;
            case 2:
                EventsSystem.current.BeforePhase2();
                break;
            case 3:
                EventsSystem.current.BeforePhase3();
                break;
            default:
                break;
        }
        GameManager.instance.energy = 29;
        Invoke("OpenCurtain", 0.8f);
        truePath = false;
        Debug.Log("gotonextphase method");
    }

    private void resetPhase() {
        GameObject.Find("Player").transform.position = new Vector3(3, -20, 0);
        EventsSystem.current.BeforePhase0();
        GameManager.instance.energy = 29;
        Invoke("OpenCurtain", 0.8f);
        truePath = false;
        EventsSystem.current.ActivatePhase0();
    }

    private void goToNextLevel (Collider2D coll) {
        if (GameManager.instance.activated == false) { //whether this portal has been activated to collide doesn't carry on to other scene
            Object.Destroy(this);
            GameManager.instance.levelLoader.showLevelCompleteScreen();
            GameManager.instance.activated = true;
        }
    }

    private void CloseCurtain() {
        curtain.SetActive(true);
        LeanTween.alphaCanvas(curtain.GetComponent<CanvasGroup>(), 1, 0.2f);
    }

    private void OpenCurtain() {
        LeanTween.alphaCanvas(curtain.GetComponent<CanvasGroup>(), 0, 0.1f);
        curtain.SetActive(false);
    }

    private void canYouMove(Vector2 dir) {
        GameManager.instance.playerCanGoTo = true;
    }

    private void OnDestroy() {
        EventsSystem.current.onTombUnlock -= addTombUnlocked;
        EventsSystem.current.onPortalEnter -= allowPortalEntry;
        EventsSystem.current.onTombLock -= resetTombs;
        EventsSystem.current.onPhase0Activate -= Phase0;
        EventsSystem.current.onPhase1Activate -= Phase1;
        EventsSystem.current.onPhase2Activate -= Phase2;
        EventsSystem.current.onPhase3Activate -= Phase3;
    }
}