using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public FloatingTextManager floatingTextManager;
    public LevelLoader levelLoader;
    public int energy = 10;
    public int gamevolume = 5;
    [HideInInspector] public bool activated = false;
    [HideInInspector] public Vector3 loadPos = Vector3.zero;
    [HideInInspector] public bool playerCanGoTo = true;

    //saving data to json
    private PlayerData playerData;
    [HideInInspector] public string path = "";
    private string persistentPath = "";

    private void Awake() {
        if (GameManager.instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        SetPaths();
        LoadSettings();
    }

    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration) {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    public int initialEnergyPerLevel (int sceneIndex) {
        switch (sceneIndex) {
            case 1:
                return 50;
            case 2: 
                return 50;
            case 3:
                return 40;
            case 4:
                return 50;
            case 5: 
                return 160;
            default:
                break;
        }
        return 17;
    }

    public void SaveState() {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        int sceneInitialEnergy = initialEnergyPerLevel(sceneIndex);
        Vector3 player_pos = GameObject.Find("Player").transform.position;
            float x = player_pos.x;
            float y = player_pos.y;
            float z = player_pos.z;
        playerData = new PlayerData(sceneIndex, energy, sceneInitialEnergy, gamevolume, x, y, z);
        SaveData();
    }

    public void SaveData() {
        string savePath = path;
        Debug.Log("Saving data at " + savePath);
        
        string json  = JsonUtility.ToJson(playerData);
        Debug.Log(json);

        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
    }

    private void SetPaths() {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
    }

    int vol;
    public void LoadSettings() {
        
        if(PlayerPrefs.HasKey("volume"))
            vol = PlayerPrefs.GetInt("volume");
        
        gamevolume = vol;
    }
}
