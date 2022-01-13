using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class anim_Opener : MonoBehaviour
{
    public GameObject go;
    public GameObject bg;
    public GameObject bg2;
    public GameObject go2;
    [SerializeField] private Image _img;
    [SerializeField] private Sprite _default, _pressed;
    [SerializeField] private AudioClip _compressClip;
    [SerializeField] private AudioSource _source;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        _img.sprite = _pressed;
        _source.PlayOneShot(_compressClip);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _img.sprite = _default;
        //_source.PlayOneShot(_uncompressClip);
    }

    public void iOpenNoBg(){
        LeanTween.scale(go, new Vector3(1, 1, 1), 0.1f);
        _source.PlayOneShot(_compressClip);
    }
    public void iOpen()
    {
        LeanTween.scale(go, new Vector3(1, 1, 1), 0.1f);
        LeanTween.alphaCanvas(bg.GetComponent<CanvasGroup>(), 1, 0.1f).setEase(LeanTweenType.linear).setOnComplete(iOpenTheDoor);

        _source.PlayOneShot(_compressClip);
    }
    public void iOpen2()
    {
        LeanTween.scale(go2, new Vector3(1, 1, 1), 0.1f);
        LeanTween.alphaCanvas(bg2.GetComponent<CanvasGroup>(), 1, 0.1f).setEase(LeanTweenType.linear).setOnComplete(iOpenTheDoor2);
    }
    public void iClose()
    {
        LeanTween.scale(go, new Vector3(0, 0, 0), 0.1f);
        LeanTween.alphaCanvas(bg.GetComponent<CanvasGroup>(), 0, 0.1f).setEase(LeanTweenType.linear).setOnComplete(iCloseTheDoor);
    }

    public void iClose2() {
        LeanTween.scale(go2, new Vector3(0, 0, 0), 0.1f);
        LeanTween.alphaCanvas(bg2.GetComponent<CanvasGroup>(), 0, 0.1f).setEase(LeanTweenType.linear).setOnComplete(iCloseTheDoor2);
    }

    public void iSendPanelMessage(string PanelMessage) {
        go.GetComponent<TextMeshProUGUI>().text = PanelMessage;
        LeanTween.moveLocal(bg, new Vector3(0, 200, 0), 0.3f);
        StartCoroutine(iClosePanelMessage());
        }

    IEnumerator iClosePanelMessage(){
        yield return new WaitForSeconds (2);
        LeanTween.moveLocal(bg, new Vector3(0, 365, 0), 0.3f);
    }

    private void iCloseTheDoor()
    {
        bg.SetActive(false);
    }
    private void iCloseTheDoor2()
    {
        bg2.SetActive(false);
    }
    private void iOpenTheDoor()
    {
        bg.SetActive(true);
    }
    private void iOpenTheDoor2()
    {
        bg2.SetActive(true);
    }

    public void iPlay(){
        string path = GameManager.instance.path;
        
        if(System.IO.File.Exists(path))
            iOpenNoBg();
        else if (!System.IO.File.Exists(path)) {
            GameManager.instance.levelLoader.LoadLevel(1);
            GameManager.instance.energy = GameManager.instance.initialEnergyPerLevel(1);
            GameManager.instance.activated = false;
        }
    }
}
