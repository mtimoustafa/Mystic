using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RuneGridController : MonoBehaviour {
  public GameObject runePrefab;
  public List<GameObject> runes;

  string[] keyBinds = { "q", "w", "e", "a", "s", "d", "z", "x", "c" };

  public void SetupGrid() {
    runes = new List<GameObject>();

    for (int y = 0; y < 3; y++) {
      for (int x = 0; x < 3; x++) {
        runes.Add(Instantiate(runePrefab, new Vector3(x-1, 1-y, 0f), Quaternion.identity) as GameObject);
        Rune rune = runes[runes.Count-1].GetComponent<Rune>();

        rune.runeType = (Rune.RuneType)(y*3 + x);
        rune.keyBinding = keyBinds[y*3 + x];
      }
    }
  }

  public void ClearRunes() {
    foreach (GameObject rune in runes) { rune.GetComponent<Rune>().ClearRune(); }
  }

  void Update() {
    if (Input.GetKeyDown("space")) {
      TriggerEnteredCombo();
    }
  }

  void TriggerEnteredCombo() {
    GameObject gameController = GameObject.FindGameObjectsWithTag("GameController")[0];
    ComboLibrary comboLibrary = gameController.GetComponent(typeof(ComboLibrary)) as ComboLibrary;

    List<GameObject> enabledRunes = runes.Where(r => r.GetComponent<Rune>().isSelected).ToList();
    if (comboLibrary.IsValidCombo(enabledRunes.Select(r => r.GetComponent<Rune>().runeType).ToArray())) {
      Debug.Log("Successful combo!");
    } else {
      Debug.Log("Failed combo.");
    }

    ClearRunes();
  }
}
