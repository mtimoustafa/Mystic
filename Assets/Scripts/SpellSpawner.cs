using System;
﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpellSpawner : MonoBehaviour {
  public enum SpellDirection { Right = 1, Left = -1};
  public SpellDirection spellDirectionX = SpellDirection.Right;

  GameObject owner;

  void Start() {
    owner = transform.parent.gameObject;
  }

  public void SpawnSpell(Spell spell) {
    Player player = owner.GetComponent<Player>();
    if (player.mana < spell.manaCost) {
      Debug.Log(owner + " doesn't have enough mana for " + spell.name);
      return;
    } else {
      player.mana -= spell.manaCost;
      player.manaCooldownCurrent = player.manaRegenCooldown;
    }

    List<GameObject> illegalCopies = GameObject.FindGameObjectsWithTag("Spell").Where(s =>
      SpellLibrary.Combofy(s.GetComponent<Spell>().runeCombo) == SpellLibrary.Combofy(spell.runeCombo) &&
      s.GetComponent<Spell>().isSingleton &&
      s.GetComponent<Spell>().owner == player).ToList();

    if (illegalCopies.Count() > 0) {
      foreach (GameObject copy in illegalCopies) { Destroy(copy); }
    }

    if (spell.chargeTime > 0f) {
       StartCoroutine("InstantiateSpellAfterChargeTime", spell);
    } else {
      InstantiateSpell(spell);
    }
  }

  void InstantiateSpell(Spell spell) {
    GameObject spellInstance = Instantiate(spell.gameObject, gameObject.transform.position, gameObject.transform.rotation);
    spellInstance.GetComponent<SpriteRenderer>().enabled = true;
    spellInstance.GetComponent<BoxCollider2D>().enabled = true;
    spellInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(spell.travelSpeed * (int)spellDirectionX, 0f);
    spellInstance.GetComponent<Spell>().owner = owner.GetComponent<Player>();
  }

  IEnumerator InstantiateSpellAfterChargeTime(Spell spell) {
    gameObject.GetComponent<SpriteRenderer>().enabled = true;
    float durationElapsed = 0f;
    yield return null;

    while (durationElapsed < spell.chargeTime) {
      durationElapsed += Time.deltaTime;
      yield return null;
    }

    gameObject.GetComponent<SpriteRenderer>().enabled = false;
    InstantiateSpell(spell);
  }
}
