using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rune : MonoBehaviour {
  public enum RuneType { Fire, Water, Earth, Air, Light, Dark, Poison, Void, Spirit };
  public RuneType runeType = RuneType.Fire;
  public string keyBinding = "a";
  public bool isSelected = false;

  Image image;

  public void ActivateRune() {
    isSelected = true;
    image.color = ToggleColor(isSelected);
  }

  public void ClearRune() {
    isSelected = false;
    image.color = ToggleColor(isSelected);
  }

  public void ToggleRune() {
    isSelected = !isSelected;
    image.color = ToggleColor(isSelected);
  }

  void Start() {
    image = GetComponent<Image>();
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
      case RuneType.Air:
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
