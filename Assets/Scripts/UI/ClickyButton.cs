using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

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
    }

    public void IWasClicked()
    {
        Debug.Log("clicked");
    }

    public void iClosePopup()
    {
        go.SetActive(false);
    }

    public void iOpenPopup()
    {
        go.SetActive(true);
        _source.PlayOneShot(_uncompressClip);
    }

    public void iGoToMenu()
    {
        GameManager.instance.SaveState();
        go.SetActive(false);

        GameManager.instance.levelLoader.LoadLevel(0);
    }

    public void iGoToMenuNoPanel()
    {
        GameManager.instance.SaveState();
        GameManager.instance.levelLoader.LoadLevel(0);
    }

    public void iGoToMenuNoSave()
    {
        go.SetActive(false);
        GameManager.instance.levelLoader.LoadLevel(0);
    }

    public void iGoToMenuFromEndGame() {    //only one button will use this (end game scene)
        string path = GameManager.instance.path;
        
        if(System.IO.File.Exists(path))
            System.IO.File.Delete(path);

        GameManager.instance.levelLoader.LoadLevel(0);
    }

    public void iResetGame() {
        string path = GameManager.instance.path;
        System.IO.File.Delete(path);
        GameManager.instance.levelLoader.LoadLevel(1);
        GameManager.instance.energy = GameManager.instance.initialEnergyPerLevel(1);
        GameManager.instance.activated = false;
    }

    public void iLoadProgress() {
        string path = GameManager.instance.path;
        using StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();
        PlayerData data = JsonUtility.FromJson<PlayerData>(json);
        Debug.Log(data.ToString());

        int sI = data.sceneIndex;
        int en = data.energy;
        float x = data.x;
        float y = data.y;
        float z = data.z;
        Vector3 pos = new Vector3(x,y,z);
        
        GameManager.instance.levelLoader.LoadLevel(sI);
        GameManager.instance.energy = en;
        GameManager.instance.activated = false;
        GameManager.instance.loadPos = pos;
    }

    public void iSaveVolume() {
        int saveVol = GameManager.instance.gamevolume;
        PlayerPrefs.SetInt("volume", saveVol);
    }

    public void iSaveGameProgress() {
        GameManager.instance.SaveState();
    }

    public void iGotToNextLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        
        GameManager.instance.levelLoader.LoadLevel(sceneIndex);
        GameManager.instance.energy = GameManager.instance.initialEnergyPerLevel(sceneIndex);

        GameManager.instance.activated = false;
    }

    public void iRestartGame() {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        GameManager.instance.levelLoader.LoadLevel(sceneIndex);
        GameManager.instance.energy = GameManager.instance.initialEnergyPerLevel(sceneIndex);
        GameManager.instance.activated = false;
    }

    public void iExitGame()
    {
        Application.Quit();
        Debug.Log("application exited");
    }
}