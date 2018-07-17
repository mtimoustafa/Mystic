using System;
﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpellSpawner : MonoBehaviour {
  public void SpawnSpell(Spell spell, string owner, int spellDirectionX = 1) {
    List<GameObject> illegalCopies = GameObject.FindGameObjectsWithTag("Spell").Where(s =>
      SpellLibrary.Combofy(s.GetComponent<Spell>().runeCombo) == SpellLibrary.Combofy(spell.runeCombo) &&
      s.GetComponent<Spell>().isSingleton &&
      s.GetComponent<Spell>().belongsTo == owner).ToList();

    if (illegalCopies.Count() > 0) {
      foreach (GameObject copy in illegalCopies) { Destroy(copy); }
    }

    GameObject spellInstance = Instantiate(spell.gameObject, gameObject.transform.position, gameObject.transform.rotation);
    spellInstance.GetComponent<SpriteRenderer>().enabled = true;
    spellInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(spell.travelSpeed * (float)Math.Pow(spellDirectionX, 0f), 0f);
    spellInstance.GetComponent<Spell>().belongsTo = owner;
  }
}
