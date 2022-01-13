using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //public fields
    public int hitpoint;
    //public int maxHitpoint;                 

    //immunity
    protected float immuneTime = 1.0f;
    protected float lastImmune;

    //all fighter can get damage and die
    protected virtual void ReceiveDamage(Damage dmg)
    {
        if (Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitpoint -= dmg.damageAmount;

            GameManager.instance.ShowText(
                dmg.damageAmount.ToString(),
                20,
                Color.red,
                transform.position,
                Vector3.zero,
                0.5f
                );

            if (hitpoint <= 0)
            {
                hitpoint = 0;
                Death();
            }
        }
    }

    protected virtual void Death()
    {
    }
}
