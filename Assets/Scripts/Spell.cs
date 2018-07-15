using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RuneType = Rune.RuneType;

public class Spell : MonoBehaviour {
  public List<RuneType> runeCombo = new List<RuneType>();
  public float deltaHP = 0f;
  public float manaCost = 0f;
  public float travelSpeed = 2f;
}
