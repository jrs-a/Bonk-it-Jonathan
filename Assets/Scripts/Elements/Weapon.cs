using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //damage struct
    public int damagePoint = 1;

    //swing
    private Animator anim;
    private float cooldown = 0f;
    private float lastSwing;

    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.tag == "Fighter")
        {
            if(coll.name == "Player")
                return;

            Damage dmg = new Damage //to be sent to enemy
            {
                damageAmount = damagePoint
            };

            coll.SendMessage("ReceiveDamage", dmg);
        }
        
    }

    private void Swing()
    {
        anim.SetTrigger("Swing");
    }
}
