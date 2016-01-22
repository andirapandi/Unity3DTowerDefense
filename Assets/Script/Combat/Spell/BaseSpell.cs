using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BaseSpell : MonoBehaviour
{
    protected float cooldown;
    protected float lastCast;

    public Button SpellButton { get; set; }

    public virtual void Cast()
    {
        if (!Requirement())
            return;

        Action();

        lastCast = Time.time;
    }

    public virtual bool Requirement()
    {
        if (Time.time - lastCast <= cooldown)
            return false;
        return true;
    }

    public virtual void Action()
    {
        Debug.Log(this.ToString() + " does not implement Action.");
    }
}
