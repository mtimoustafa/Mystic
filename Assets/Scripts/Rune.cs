using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : MonoBehaviour {
  public enum RuneType { Fire, Water, Earth, Wind, Light, Dark, Poison, Void, Spirit };
  public RuneType runeType = RuneType.Fire;
  public string keyBinding = "a";
  public bool isSelected = false;

  SpriteRenderer runeRenderer;

  // Use this for initialization
  void Start () {
    runeRenderer = GetComponent<SpriteRenderer>();
  }

  // Update is called once per frame
  void Update () {
    if (Input.GetKeyDown(keyBinding)) {
      isSelected = !isSelected;
      runeRenderer.color = ToggleColor(isSelected);
    }
  }

  Color ToggleColor(bool on) {
    if (!on) return Color.black;

    switch (runeType) {
      case RuneType.Fire:
        return Color.red;
      case RuneType.Water:
        return Color.blue;
      case RuneType.Earth:
        return Color.green;
      case RuneType.Wind:
        return Color.cyan;
      case RuneType.Light:
        return Color.white;
      case RuneType.Dark:
        return Color.gray;
      case RuneType.Poison:
        return Color.yellow;
      case RuneType.Void:
        return Color.magenta;
      case RuneType.Spirit:
        return new Color(0.2f, 0.3f, 0.4f);
      default:
        return Color.black;
    }
  }
}
