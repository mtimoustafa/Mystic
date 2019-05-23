using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RuneType = Rune.RuneType;

public class SpellLibrary : MonoBehaviour {
  [HideInInspector]
  List<Spell> spells;

  public void Start () {
    spells = GameObject.FindGameObjectsWithTag("Spell").Select(go => go.GetComponent<Spell>()).ToList();
  }

  public static string Combofy(params RuneType[] runes) {
    string combo = "";
    foreach (RuneType rune in runes) {
      combo += rune.ToString() + '.';
    }
    return combo;
  }

  public static string Combofy(List<RuneType> runes) {
    return Combofy(runes.ToArray());
  }

  public Spell FindSpellByCombo(params RuneType[] runes) {
    List<Spell> results = spells.Where(s => Combofy(s.runeCombo.ToArray()) == Combofy(runes)).ToList();
    if (results.Count() > 0) { return results.First(); }
    else { return null; }
  }
}
