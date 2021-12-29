using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Fighter
{
    protected override void Death()
    {
        Destroy(gameObject);
    }
}
