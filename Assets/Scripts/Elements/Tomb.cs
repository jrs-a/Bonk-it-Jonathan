using UnityEngine;

public class Tomb : Enemy
{
    private void Start() {
        EventsSystem.current.onBeforePhase0 += reviveTomb;
    }

    private void reviveTomb() {
        LeanTween.color(gameObject, new Vector4(255,255,255,255), 1);
        hitpoint = 2;
        EventsSystem.current.LockTomb();
    }

    protected override void Death() {
        base.Death();
    }

    private void OnDestroy() {
        EventsSystem.current.onBeforePhase0 -= reviveTomb;
    }
}
