using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [SerializeField] int armor;
    [SerializeField] int Health;

    public void GetAttacked(int damage)
    {
        if(damage>armor)
        {
            Health -= (damage - armor);
            if (Health <= 0)
            {
                Debug.Log(name + " is dead");
            }
        }
    }
}
