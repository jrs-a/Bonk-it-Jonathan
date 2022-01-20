using UnityEngine;

public class Enemy : Fighter //refers to tombs and ONLY TOMBS because.
{
    protected virtual void canYouMove(Vector2 dir) {
        GameManager.instance.playerCanGoTo = false;
    }

    protected override void Death() {
        LeanTween.color(gameObject, new Vector4(255,255,255,0), 1);
        EventsSystem.current.UnlockTomb();
    }
}