using UnityEngine;
using TMPro;

public class txtEnergy : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    int en_local = 0;

    private void Update()
    {
        int energy = GameManager.instance.energy;
        textMesh.text = energy.ToString();

        //Vector3 player_pos = GameManager.instance.player.transform.position;
        Vector3 player_pos = GameObject.Find("Player").transform.position;
        Vector3 p_norm = Camera.main.WorldToScreenPoint(player_pos);
        float x = p_norm.x;
        float y = p_norm.y;

        LeanTween.move(gameObject, new Vector3(x, y + 45, 1), 0);

        if (en_local != energy)
        {
            en_local = energy;
            txtBounce();
        }
    }

    private void txtBounce()
    {
        LeanTween.cancel(gameObject);
        transform.localScale = Vector3.one;
        LeanTween.scale(gameObject, Vector3.one * 1.5f, 1f).setEasePunch();
    }
}
