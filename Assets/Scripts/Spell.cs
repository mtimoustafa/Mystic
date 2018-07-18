using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RuneType = Rune.RuneType;

public class Spell : MonoBehaviour {
  public List<RuneType> runeCombo = new List<RuneType>();
  public float deltaHP = 0f;
  public float manaCost = 0f;
  public float travelSpeed = 2f;
  public float duration = 0f;
  public float chargeTime = 0f;
  public bool isSingleton = false;
  public bool instantaneous = false;
  public bool isWard = false;
  public Player owner;

  void Start() {
    // Don't mess spell library spells as the spell library is a factory
    if (transform.parent != null && transform.parent.gameObject.tag == "SpellLibrary") {
      return;
    }

    if (instantaneous) {
      owner.ApplySpellEffects(this);
      Destroy(gameObject);
    }

    if (duration > 0f) {
      StartCoroutine("DestroyAfterDuration");
    }
  }

  void OnTriggerEnter2D(Collider2D collider) {
    if (collider.gameObject.tag == "Spell") {
      if (isWard) {
        // Wards destroy other spells but keep themselves intact
        Destroy(collider.gameObject);
      } else {
        Destroy(gameObject);
      }
    }
  }

  IEnumerator DestroyAfterDuration() {
    float durationElapsed = 0f;
    yield return null;

    while (durationElapsed < duration) {
      durationElapsed += Time.deltaTime;
      yield return null;
    }
    
    Destroy(gameObject);
  }
}
