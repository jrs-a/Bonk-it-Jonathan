using UnityEngine;
using UnityEngine.Events;

public class anim_Opener : MonoBehaviour
{
    public GameObject go;
    public GameObject bg;
    public void iOpen()
    {
        LeanTween.scale(go, new Vector3(1, 1, 1), 0.1f); 
        LeanTween.alphaCanvas(bg.GetComponent<CanvasGroup>(), 1, 0.1f).setEase(LeanTweenType.linear).setOnComplete(iOpenTheDoor);
    }

    public void iClose()
    {
        LeanTween.scale(go, new Vector3(0, 0, 0), 0.1f);
        LeanTween.alphaCanvas(bg.GetComponent<CanvasGroup>(), 0, 0.1f).setEase(LeanTweenType.linear).setOnComplete(iCloseTheDoor);
    }

    private void iCloseTheDoor()
    {
        bg.SetActive(false);
    }
    private void iOpenTheDoor()
    {
        bg.SetActive(true);
    }
}
