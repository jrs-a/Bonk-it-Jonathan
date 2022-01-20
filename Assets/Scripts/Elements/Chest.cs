using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectible
{
    public Sprite emptyChest;
    public int energyAmount = 10;

    private void canYouMove(Vector2 dir) {}
    protected override void OnCollect() {
        if (!collected) {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.energy += energyAmount;
            GameManager.instance.ShowText(
                "+ " + energyAmount + " energy!",
                21,
                Color.yellow, 
                transform.position, 
                Vector3.up*80, 0.5f
                );
        }
    }
}
