using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RuneGrid : MonoBehaviour {
  public GameObject runePrefab;
  public string[] keyBinds = { "q", "w", "e", "a", "s", "d", "z", "x", "c" };
  public string fireKeybind = "f";
  public GameObject owner;

  List<GameObject> runes;
  Vector2 _gridSize;

  public void SetupGrid() {
    runes = new List<GameObject>();

    for (int y = 0; y < 3; y++) {
      for (int x = 0; x < 3; x++) {
        float xPos = (x - 1) * _gridSize.x / 3;
        float yPos = (1 - y) * _gridSize.y / 3;

        GameObject newRuneObject = Instantiate(runePrefab, gameObject.transform) as GameObject;

        newRuneObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);

        Rune newRune = newRuneObject.GetComponent<Rune>();
        newRune.runeType = (Rune.RuneType)(y*3 + x);
        newRune.keyBinding = keyBinds[y*3 + x];

        runes.Add(newRuneObject);
      }
    }
  }

  public void ClearRunes() {
    foreach (GameObject rune in runes) { rune.GetComponent<Rune>().ClearRune(); }
  }

  void Start() {
    _gridSize = gameObject.GetComponent<RectTransform>().sizeDelta;
    SetupGrid();
  }

  void Update() {
    if (Input.GetKeyDown(fireKeybind)) {
      TriggerEnteredCombo();
    }
  }

  void TriggerEnteredCombo() {
    SpellLibrary spellLibrary = GameObject.FindGameObjectsWithTag("SpellLibrary")[0].GetComponent<SpellLibrary>();
    List<GameObject> enabledRunes = runes.Where(r => r.GetComponent<Rune>().isSelected).ToList();
    Spell spell = spellLibrary.FindSpellByCombo(enabledRunes.Select(r => r.GetComponent<Rune>().runeType).ToArray());

    if (spell == null) {
      Debug.Log("Failed combo");
    } else {
      owner.transform.GetChild(0).GetComponent<SpellSpawner>().SpawnSpell(spell);
    }

    ClearRunes();
  }
}
