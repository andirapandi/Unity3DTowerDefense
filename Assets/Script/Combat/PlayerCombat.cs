using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerCombat : BaseCombat
{
    public GameObject spellButtonPrefab;
    public GameObject spellButtonContainer;

    List<BaseSpell> spellBook = new List<BaseSpell>();

    public override void InitCombat()
    {
        base.InitCombat();

        // Choose which abilities our player will own
        // Player's Abilities
        AddSpell("MeleeAttackSpell");
    }

    void Update()
    {
        foreach (var spell in spellBook)
        {
            if (spell.SpellButton != null)
            {
                spell.SpellButton.interactable = spell.Requirement();
            }
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (spellBook.Count > 0)
        //        spellBook[0].Cast();
        //}
        var temp = spellButtonContainer.transform;
    }

    private void AddSpell(string spellName)
    {
        System.Type t = System.Type.GetType(spellName);
        BaseSpell spell = gameObject.AddComponent(t) as BaseSpell;
        var button = Instantiate(spellButtonPrefab);
        spell.SpellButton = button.GetComponent<Button>();
        spell.SpellButton.onClick.AddListener(() => spell.Cast());
        spell.SpellButton.transform.SetParent(spellButtonContainer.transform);
        // added, this gives buttons nice size, as per parent
        button.transform.localScale = button.transform.parent.localScale;
        //spellButtonContainer.
        //button.transform.SetParent(spellButtonContainer.transform);
        spellBook.Add(spell);
        //spellBook.Add(gameObject.AddComponent<MeleeAttackSpell>() /* as BaseSpell */);
    }
}
