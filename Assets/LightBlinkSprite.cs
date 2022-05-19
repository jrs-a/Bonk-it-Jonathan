using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlinkSprite : MonoBehaviour
{
    private bool active = true;
    void Start() {
        LightBlink();
    }

    private void LightBlink() {
        while(active) {
            // StartCoroutine(LightsOn());
            // StartCoroutine(LightsOff());
            Debug.Log("hello i am blink");
        }
    }

    IEnumerator LightsOn() {
        yield return new WaitForSeconds(2);
        LeanTween.color(gameObject, new Vector4(52,171,200,170), 1);
    }

    IEnumerator LightsOff() {
        yield return new WaitForSeconds(1);
        LeanTween.color(gameObject, new Vector4(52,171,200,0), 1);
    }

    
}
