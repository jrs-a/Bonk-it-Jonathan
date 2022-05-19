using System.Collections;
using UnityEngine;
using TMPro;

public class GateActivator : Collidable
{
    private bool activated = false;
    public int id;
    //[SerializeField] GameObject tooltip;    //TODO this tooltip please

    protected override void OnCollide(Collider2D coll) {
        if (coll.name == "Player") {
            if (!activated)
                openGate();
        }
    }

    private void openGate() {
        EventsSystem.current.OpenGate(id);
        activated = true;
        // LeanTween.color(gameObject, new Vector4(255,255,255,0), 1.5f);
        // tooltip.text = "gate opened";
        //StartCoroutine(ShowText());
    }

    private void canYouMove(Vector2 dir) {
        GameManager.instance.playerCanGoTo = true;
    }

    IEnumerator ShowText() {
        //LeanTween.alphaCanvas(tooltip.GetComponent<CanvasGroup>(), 1, 0.2f);
        return null;
    }
}
