using System.Collections;
using UnityEngine;

public class Teleport : Collidable
{
    public GameObject iGoHere;
    public GameObject player, weapon;
    private Vector3 endPosition;
    private Color tmp;

    protected override void OnCollide(Collider2D coll)
    {
        endPosition = iGoHere.GetComponent<Transform>().position;

        tmp = player.GetComponent<SpriteRenderer>().color;
        tmp.a = 0f;
        
        if(coll.name == "Player")
            StartCoroutine(teleport());
        
    }

    IEnumerator teleport() {
        LeanTween.color(weapon, tmp, 0.2f);
        LeanTween.color(player, tmp, 0.2f);
        yield return new WaitForSecondsRealtime(1f);
        LeanTween.moveLocal(player, endPosition, 0.3f);
        yield return new WaitForSecondsRealtime(1f);
        tmp.a = 255f;
        LeanTween.color(weapon, tmp, 0.3f);
        LeanTween.color(player, tmp, 0.3f);
    }
}
