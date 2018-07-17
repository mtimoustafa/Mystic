using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RuneGrid : MonoBehaviour {
  public GameObject runePrefab;
  public string[] keyBinds = { "q", "w", "e", "a", "s", "d", "z", "x", "c" };

  List<GameObject> runes;

  public void SetupGrid() {
    runes = new List<GameObject>();

    for (int y = 0; y < 3; y++) {
      for (int x = 0; x < 3; x++) {
        runes.Add(Instantiate(runePrefab, new Vector3(transform.position.x + x-1, transform.position.y + 1-y, 0f), Quaternion.identity) as GameObject);
        runes[runes.Count-1].transform.parent = gameObject.transform;
        Rune rune = runes[runes.Count-1].GetComponent<Rune>();

        rune.runeType = (Rune.RuneType)(y*3 + x);
        rune.keyBinding = keyBinds[y*3 + x];
      }
    }
  }

  public void ClearRunes() {
    foreach (GameObject rune in runes) { rune.GetComponent<Rune>().ClearRune(); }
  }

  void Start() {
    // Sprite is only to help us see grid position in the editor, so disable it in runtime
    gameObject.GetComponent<SpriteRenderer>().enabled = false;
    SetupGrid();
  }

  void Update() {
    if (Input.GetKeyDown("space")) {
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
      GameObject.Find("Player Spell Spawner").GetComponent<SpellSpawner>().SpawnSpell(spell, "player");
    }

    ClearRunes();
  }
}
