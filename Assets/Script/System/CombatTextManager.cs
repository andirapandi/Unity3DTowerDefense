using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CombatText
{
    public bool active;
    public GameObject go;
    public Text txt;
    public Vector3 motion;
    public float duration;
    public float lastShow;

    public void Show()
    {
        active = true;
        lastShow = Time.time;
        go.SetActive(true);
    }

    public void Hide()
    {
        go.SetActive(false);
        active = false;
    }

    public void UpdateCombatText()
    {
        if (!active)
            return;

        if (Time.time - lastShow > duration)
            Hide();

        go.transform.position += motion * Time.deltaTime;
    }
}

public class CombatTextManager : MonoSingleton<CombatTextManager>
{
    public GameObject combatTextContainer;
    public GameObject combatTextPrefab;

    List<CombatText> combatTexts = new List<CombatText>();

    void Update()
    {
        foreach (var cmb in combatTexts)
        {
            cmb.UpdateCombatText();
        }
    }

    public void Show(string text, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        CombatText cmb = GetCombatText();
        cmb.txt.text = text;
        cmb.txt.fontSize = fontSize;
        cmb.txt.color = color;
        cmb.go.transform.position = position;
        cmb.motion = motion;
        cmb.duration = duration;
        cmb.go.transform.rotation = Camera.main.transform.rotation;
        cmb.Show();
    }

    CombatText GetCombatText()
    {
        var cmb = combatTexts.Find(c => !c.active);
        if (cmb == null)
        {
            // no (inactive/free) combat text available, let's create a new one!
            cmb = new CombatText();
            cmb.go = Instantiate(combatTextPrefab);
            cmb.go.transform.SetParent(combatTextContainer.transform);
            cmb.txt = cmb.go.GetComponent<Text>();
            combatTexts.Add(cmb);
        }
        return cmb;
    }
}