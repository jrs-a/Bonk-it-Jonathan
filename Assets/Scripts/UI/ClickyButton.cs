using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _img;
    [SerializeField] private Sprite _default, _pressed;
    [SerializeField] private AudioClip _compressClip, _uncompressClip;
    [SerializeField] private AudioSource _source;
    public GameObject go;
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

    public void IWasClicked()
    {
        Debug.Log("clicked");
    }

    public void iGoToMenu()
    {
        GameManager.instance.SaveState();
        go.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    public void iSaveProgress()
    {
        GameManager.instance.SaveState();
    }

    public void iClosePopup()
    {
        go.SetActive(false);
    }

    public void iOpenPopup()
    {
        go.SetActive(true);
    }
}
