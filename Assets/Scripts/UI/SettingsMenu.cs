using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public GameObject gamevolume;
    public AudioMixer audioMixer;

    void Update()
    {
        int vol = GameManager.instance.gamevolume;
        
        float vol2 = (float)vol*10-80;
        audioMixer.SetFloat("volume", vol2);

        float finalVol = (float)vol/10;
        gamevolume.GetComponent<Image>().fillAmount = finalVol;
    }

    public void addVolume() {
        int vol = GameManager.instance.gamevolume;
        
        if (vol < 10) 
            GameManager.instance.gamevolume += 1;
    }

    public void minusVolume() {
        int vol = GameManager.instance.gamevolume;
        
        if (vol > 0)
            GameManager.instance.gamevolume -= 1;
    }
}
