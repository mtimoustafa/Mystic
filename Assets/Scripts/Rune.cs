using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : MonoBehaviour {
  public enum RuneType { Fire, Water, Earth, Wind, Light, Dark, Poison, Void, Spirit };
  public RuneType runeType = RuneType.Fire;
  public string keyBinding = "a";
  public bool isSelected = false;

  SpriteRenderer runeRenderer;

  public void ActivateRune() {
    isSelected = true;
    runeRenderer.color = ToggleColor(isSelected);
  }

  public void ClearRune() {
    isSelected = false;
    runeRenderer.color = ToggleColor(isSelected);
  }

  public void ToggleRune() {
    isSelected = !isSelected;
    runeRenderer.color = ToggleColor(isSelected);
  }

  void Start() {
    runeRenderer = GetComponent<SpriteRenderer>();
  }

  void Update() {
    if (Input.GetKeyDown(keyBinding)) {
      ToggleRune();
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
