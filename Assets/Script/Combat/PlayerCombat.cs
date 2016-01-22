using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCombat : BaseCombat
{
    List<BaseSpell> spellBook = new List<BaseSpell>();

    public override void InitCombat()
    {
        base.InitCombat();

        // Choose which abilities our player will own
        spellBook.Add(gameObject.AddComponent<MeleeAttackSpell>() /* as BaseSpell */);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            spellBook[0].Cast();
        }
    }
}
