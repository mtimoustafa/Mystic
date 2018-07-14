using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RuneType = Rune.RuneType;

public class ComboLibrary : MonoBehaviour {
  public List<string> validCombos;

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
    return validCombos.Contains(Combofy(runes));
  }

  void InitializeComboList() {
    validCombos = new List<string> {
      Combofy(RuneType.Fire, RuneType.Wind)
    };
  }
}
