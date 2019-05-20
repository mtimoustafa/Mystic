using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneGridController : MonoBehaviour {
  public GameObject runePrefab;
  public string[] keyBinds = {
    "q", "w", "e",
    "a", "s", "d",
    "z", "x", "c"
  };

  Vector2 _gridSize;

  void generateRunes() {
    List<GameObject> runes = new List<GameObject>();
    for (int y = 0; y < 3; y++) {
      for (int x = 0; x < 3; x++) {
        float xPos = (x - 1) * _gridSize.x / 3;
        float yPos = (1 - y) * _gridSize.y / 3;

        GameObject newRune = Instantiate(runePrefab, gameObject.transform) as GameObject;

        newRune.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);

        RuneController newRuneController = newRune.GetComponent<RuneController>();
        newRuneController.runeType = (RuneController.RuneType)(y*3 + x);
        newRuneController.keyBinding = keyBinds[y*3 + x];

        runes.Add(newRune);
      }
    }
  }

  void Start () {
    _gridSize = gameObject.GetComponent<RectTransform>().sizeDelta;
    generateRunes();
  }
}
