using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneGridController : MonoBehaviour {
  public GameObject runePrefab;

  string[] keyBinds = { "q", "w", "e", "a", "s", "d", "z", "x", "c" };

  public void SetupGrid() {
    List<GameObject> runes = new List<GameObject>();
    for (int y = 0; y < 3; y++) {
      for (int x = 0; x < 3; x++) {
        runes.Add(Instantiate(runePrefab, new Vector3(x-1, 1-y, 0f), Quaternion.identity) as GameObject);
        Rune rune = runes[runes.Count-1].GetComponent<Rune>();

        rune.runeType = (Rune.RuneType)(y*3 + x);
        rune.keyBinding = keyBinds[y*3 + x];
      }
    }
  }
}
