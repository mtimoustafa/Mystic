using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneController : MonoBehaviour {
  public enum RuneType { Fire, Water, Earth };
  public RuneType runeType = RuneType.Fire;
  public string keyBinding = "a";

  SpriteRenderer spriteRenderer;

  // Use this for initialization
  void Start () {
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  // Update is called once per frame
  void Update () {
    if (Input.GetKeyDown(keyBinding))
      spriteRenderer.color = ToggleColor(true);
    else if (Input.GetKeyUp(keyBinding))
      spriteRenderer.color = ToggleColor(false);
  }

  Color ToggleColor(bool on) {
    if (!on) return Color.white;

    switch (runeType) {
      case RuneType.Fire:
        return Color.red;
      case RuneType.Water:
        return Color.blue;
      case RuneType.Earth:
        return Color.green;
      default:
        return Color.black;
    }
  }
}
