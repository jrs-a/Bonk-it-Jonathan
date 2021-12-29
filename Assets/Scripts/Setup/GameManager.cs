using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerController player;
    public FloatingTextManager floatingTextManager;
    public static txtEnergy txtEnergy;
    
    public List<Sprite> playerSprites;
    public int energy = 10;
    
    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);

        //PlayerPrefs.DeleteAll(); - restart game(clear all prefs in data)
    }

    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    public void SaveState()
    {
        string s = "";
        s += "0" + "|"; // INT preferred Skin
        s += energy.ToString() + "|"; // INT pesos

        PlayerPrefs.SetString("SaveState", s);
        Debug.Log("SaveState");
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState"))
            return;
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        //0|10|0|0 from the values in the string above
        //Change player skin
        energy = int.Parse(data[1]); 

        Debug.Log("LoadState");
    }
}
