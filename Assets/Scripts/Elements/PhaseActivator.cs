using UnityEngine;

public class PhaseActivator : Collidable
{
    public int phaseToActivate;
    public GameObject nextActivator;

    protected override void Start() {
        base.Start();
        EventsSystem.current.onPhase0Activate += resetActivators;
    }

    private void canYouMove(Vector2 dir) {
        GameManager.instance.playerCanGoTo = true;
    }

    protected override void OnCollide(Collider2D coll) {
        if(coll.name == "Player")
            ActivateRequest(phaseToActivate);
    }

    private void resetActivators() {
        switch (phaseToActivate) {
            case 1:
                gameObject.SetActive(true);
                break;
            case 2:
                gameObject.SetActive(false);
                break;
            case 3:
                gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    private void ActivateRequest(int phaseNum) {
        switch (phaseNum) {
            case 1:
                EventsSystem.current.ActivatePhase1();
                nextActivator.SetActive(true);
                gameObject.SetActive(false);
                break;
            case 2:
                EventsSystem.current.ActivatePhase2();
                nextActivator.SetActive(true);
                gameObject.SetActive(false);
                break;
            case 3:
                EventsSystem.current.ActivatePhase3();
                gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    private void OnDestroy() {
        EventsSystem.current.onPhase0Activate -= resetActivators;
    }
}
