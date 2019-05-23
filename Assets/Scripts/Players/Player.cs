using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
  public float maxHP = 10f;
  public float maxMana = 10f;
  public float manaRegenCooldown = 1f;
  public float manaRegenPerSecond = 2f;
  public float HP;
  public float mana;
  public Slider HPDisplay;
  public Slider manaDisplay;

  [HideInInspector]
  public float manaCooldownCurrent;

  void Start () {
    HP = maxHP;
    mana = maxMana;

    manaCooldownCurrent = 0f;

    HPDisplay.maxValue = maxHP;
    HPDisplay.value = HP;
    manaDisplay.maxValue = maxMana;
    manaDisplay.value = mana;
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

    UpdateDisplays();
  }

  void OnTriggerEnter2D(Collider2D collider) {
    if (collider.gameObject.tag == "Spell") {
      ApplySpellEffects(collider.gameObject.GetComponent<Spell>());
      Destroy(collider.gameObject);
    }
  }

  public void ApplySpellEffects(Spell spell) {
    HP += spell.deltaHP;

    Action<SpellCondition> addCondition = new Action<SpellCondition>(AddCondition);
    Array.ForEach(spell.gameObject.GetComponents<SpellCondition>(), addCondition);
  }

  void AddCondition(SpellCondition condition) {
    gameObject.GetComponent<PlayerCondition>().AddCondition(condition);
  }

  void UpdateDisplays() {
    HPDisplay.value = HP;
    manaDisplay.value = mana;
  }
}
