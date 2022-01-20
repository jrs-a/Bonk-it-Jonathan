using UnityEngine;

public class Gate : MonoBehaviour
{
    bool isOpen = false;

    void Start() {
        EventsSystem.current.onGateOpen += openingGate;
    }

    private void canYouMove(Vector2 dir) {
        GameManager.instance.playerCanGoTo = isOpen;
    }

    private void openingGate() {
        EventsSystem.current.UnlockGate();  //CINEMACHINE
        isOpen = true;
        LeanTween.color(gameObject, new Vector4(255,255,255,0), 0.65f);
    }

    void OnDestroy() {
        EventsSystem.current.onGateOpen -= openingGate;
    }
}