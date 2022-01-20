using UnityEngine;

public class GlobalLight : MonoBehaviour
{
    public UnityEngine.Experimental.Rendering.Universal.Light2D globalLight;
    public float phase0Light,phase1Light, phase2Light, phase3Light;
    
    private void Start() {
        EventsSystem.current.onBeforePhase0 += Phase0;
        EventsSystem.current.onBeforePhase1 += Phase1;
        EventsSystem.current.onBeforePhase2 += Phase2;
        EventsSystem.current.onBeforePhase3 += Phase3;

        globalLight = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
    }

    private void Phase0() {
        globalLight.intensity = phase0Light;
    }

   private void Phase1() {
        globalLight.intensity = phase1Light;
    }

    private void Phase2() {
        globalLight.intensity = phase2Light;
    }

    private void Phase3() {
        globalLight.intensity = phase3Light;
    }

    private void OnDestroy() {
        EventsSystem.current.onBeforePhase0 -= Phase0;
        EventsSystem.current.onBeforePhase1 -= Phase1;
        EventsSystem.current.onBeforePhase2 -= Phase2;
        EventsSystem.current.onBeforePhase3 -= Phase3;
    }
}