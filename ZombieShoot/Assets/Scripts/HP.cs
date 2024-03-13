using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP
{
    public int hp = 100;

    public void GetAttack(int hpM)
    {
        hp -= hpM;
    }
}
