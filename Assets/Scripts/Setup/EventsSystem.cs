using UnityEngine;
using System;

public class EventsSystem : MonoBehaviour
{
    public static EventsSystem current;
    
    private void Awake() {
        current = this;
    }

    public event Action onTombUnlock;   //killing the tomb
    public void UnlockTomb() {
        if (onTombUnlock != null)
            onTombUnlock();
    }

    public event Action onTombLock;     //revive the tomb
    public void LockTomb() {
        if(onTombLock != null)
            onTombLock();
    }

    public event Action onGateUnlock;   //FOR CINEMACHINE
    public void UnlockGate() {
        if (onGateUnlock != null)
            onGateUnlock();
    }

    public event Action onPortalOpen;   //FOR CINEMACHINE
    public void OpenPortal() {
        if(onPortalOpen != null)
            onPortalOpen();
    }

    public event Action onGateOpen;     //THE GATE IS ACTUALLY OPENED :D
    public void OpenGate() {
        if(onGateOpen != null)
            onGateOpen();
    }

    public event Action onPhase0Activate;
    public void ActivatePhase0() {
        if(onPhase0Activate != null)
            onPhase0Activate();
    }

    public event Action onPhase1Activate;
    public void ActivatePhase1() {
        if(onPhase1Activate != null) {
            onPhase1Activate();
        }
    }

    public event Action onPhase2Activate;
    public void ActivatePhase2() {
        if(onPhase2Activate != null)
            onPhase2Activate();
    }

    public event Action onPhase3Activate;
    public void ActivatePhase3() {
        if(onPhase3Activate != null)
            onPhase3Activate();
    }

    public event Action onBeforePhase0;
    public void BeforePhase0() {
        if(onBeforePhase0 != null)
            onBeforePhase0();
    }

    public event Action onBeforePhase1;
    public void BeforePhase1() {
        if(onBeforePhase1 != null)
            onBeforePhase1();
    }

    public event Action onBeforePhase2;
    public void BeforePhase2() {
        if(onBeforePhase2 != null)
            onBeforePhase2();
    }

    public event Action onBeforePhase3;
    public void BeforePhase3() {
        if(onBeforePhase3 != null)
            onBeforePhase3();
    }

    public event Action onPortalEnter;
    public void EnterPortal() {
        if(onPortalEnter != null)
            onPortalEnter();
    }
}
