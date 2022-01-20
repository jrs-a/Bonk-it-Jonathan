using UnityEngine;
using System.Collections;

public class CinemachineSwitcher : MonoBehaviour
{
    public Animator anim;
    
    private void Start() {
        EventsSystem.current.onGateUnlock += ShowGateUnlock;
        EventsSystem.current.onPortalOpen += ShowPortalOpen;
    }

    private void ShowGateUnlock () {
        anim.SetBool("gate_unlock", true);
        StartCoroutine(gatewaiter());
    }

    private void ShowPortalOpen () {
        anim.SetBool("portal_activate", true);
        StartCoroutine(portalwaiter());
    }
        
    IEnumerator portalwaiter () {
        yield return new WaitForSeconds(2f);
        anim.SetBool("portal_activate", false);
    }

    IEnumerator gatewaiter () {
        yield return new WaitForSeconds(2f);
        anim.SetBool("gate_unlock", false);
    }

    private void OnDestroy() {
        EventsSystem.current.onGateUnlock -= ShowGateUnlock;
        EventsSystem.current.onPortalOpen -= ShowPortalOpen;
    }
}