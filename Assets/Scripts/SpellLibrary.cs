using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RuneType = Rune.RuneType;

public class SpellLibrary : MonoBehaviour {
  List<Spell> spells;

  public void Start () {
    InitializeComboList();
  }

  public string Combofy(params RuneType[] runes) {
    string combo = "";
    foreach (RuneType rune in runes) {
      combo += rune.ToString() + '.';
    }
    return combo;
  }

  public bool IsValidCombo(params RuneType[] runes) {
    return spells.Where(s => Combofy(s.runeCombo.ToArray()) == Combofy(runes)).Count() > 0;
  }

  void InitializeComboList() {
    spells = GameObject.FindGameObjectsWithTag("Spell").Select(go => go.GetComponent<Spell>()).ToList();
  }
}
