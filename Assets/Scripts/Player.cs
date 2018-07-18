using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
  public float maxHP = 10f;
  public float maxMana = 10f;
  public float manaRegenCooldown = 1f;
  public float manaRegenPerSecond = 2f;

  // TODO: make these readonly in inspector
  public float HP;
  public float mana;
  
  float manaCooldownCurrent;

  void Start () {
    HP = maxHP;
    mana = maxMana;

    manaCooldownCurrent = 0f;
  }

  void Update () {
    if (HP <= 0f) {
      Destroy(gameObject);
    }

    if (manaCooldownCurrent > 0f) {
      manaCooldownCurrent -= Time.deltaTime;
    } else {
      mana += manaRegenPerSecond / 60f;
    }

    if (HP > maxHP) { HP = maxHP; }
    if (mana > maxMana) { mana = maxMana; }
  }

  void OnTriggerEnter2D(Collider2D collider) {
    if (collider.gameObject.tag == "Spell") {
      HP += collider.gameObject.GetComponent<Spell>().deltaHP;
      Destroy(collider.gameObject);
    }
  }
}
