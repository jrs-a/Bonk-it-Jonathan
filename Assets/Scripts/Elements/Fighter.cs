using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //public fields
    public int hitpoint;
    //public int maxHitpoint;                 

    //immunity
    protected float immuneTime = 0.8f;
    protected float lastImmune;

    //all fighter can get damage and die
    protected virtual void ReceiveDamage(Damage dmg) {
        if (hitpoint == 0) 
            return;
        else if (Time.time - lastImmune > immuneTime) {
            lastImmune = Time.time;
            hitpoint -= dmg.damageAmount;

            Vector3 pos = new Vector3(transform.position.x, transform.position.y+0.35f, transform.position.z);

            GameManager.instance.ShowText(
                "-" + dmg.damageAmount.ToString(),
                24,
                Color.red,
                pos,
                Vector3.up*45,
                0.25f
                );

            if (hitpoint <= 0) {
                hitpoint = 0;
                Death();
            }
        }
    }

    protected virtual void Death() {}
}
