using UnityEngine;
using System.Collections;
using System;

public class DamageInfo
{
    public int amount;
}

public class BaseCombat : MonoBehaviour
{
    protected int hitpoint = 10;
    protected int maxHitpoint = 10;
    public int Hitpoint { get { return hitpoint; } set { hitpoint = value; } }
    [SerializeField]
    public int MaxHitpoint { get { return maxHitpoint; } set { maxHitpoint = value; } }
    //public int Hitpoint { get; set; } = 10;

    void Start()
    //void Awake()
    //void OnEnable()
    {
        InitCombat();
    }

    public virtual void InitCombat()
    {
       Hitpoint = MaxHitpoint;
    }

    public virtual void OnDamage(DamageInfo dmg)
    {
        Hitpoint -= dmg.amount;

        if (Hitpoint <= 0)
            OnDeath();
    }

    public virtual void OnDeath()
    {
        Debug.Log(name + " has died");
    }
}
