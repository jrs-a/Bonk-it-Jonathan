using UnityEngine;
using System.Collections;

public class CinemachineSwitcher : MonoBehaviour
{
    public Animator anim;
    
    private void Start() {
        EventsSystem.current.onGateUnlock += ShowGateUnlock;
        EventsSystem.current.onPortalOpen += ShowPortalOpen;
    }

    private void ShowGateUnlock (int id) {
        switch (id) {
            case 0:
                anim.SetBool("gate_unlock0", true);
                StartCoroutine(gatewaiter(id));
                break;   
            case 1: 
                anim.SetBool("gate_unlock1", true);
                StartCoroutine(gatewaiter(id));
                break;
            case 2:
                anim.SetBool("gate_unlock2", true);
                StartCoroutine(gatewaiter(id));
                break;
            default:
                break;
        }
    }

    private void ShowPortalOpen () {
        anim.SetBool("portal_activate", true);
        StartCoroutine(portalwaiter());
    }
        
    IEnumerator portalwaiter () {
        yield return new WaitForSeconds(2f);
        anim.SetBool("portal_activate", false);
    }

    IEnumerator gatewaiter (int id) {
        yield return new WaitForSeconds(2f);
        switch (id) {
            case 0:
                anim.SetBool("gate_unlock0", false);
                break;   
            case 1: 
                anim.SetBool("gate_unlock1", false);
                break;
            case 2: 
                anim.SetBool("gate_unlock2", false);
                break;
            default:
                break;
        }
    }

    private void OnDestroy() {
        EventsSystem.current.onGateUnlock -= ShowGateUnlock;
        EventsSystem.current.onPortalOpen -= ShowPortalOpen;
    }
}