using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RuneType = Rune.RuneType;
using SpellDirection = SpellSpawner.SpellDirection;

public class Spell : MonoBehaviour {
  public List<RuneType> runeCombo = new List<RuneType>();
  public float deltaHP = 0f;
  public float manaCost = 0f;
  public float travelSpeed = 2f;
  public float duration = 0f;
  public float chargeTime = 0f;
  public bool isSingleton = false;
  public bool instantlyApplied = false;
  public bool isWard = false;
  public Player owner;

  [HideInInspector]
  public SpellDirection spellDirection;

  void Start() {
    // Don't mess with spell library spells as the spell library is a factory
    if (IsLibrarySpell()) { return; }

    if (instantlyApplied) {
      owner.ApplySpellEffects(this);
      Destroy(gameObject);
    }

    if (duration > 0f) {
      StartCoroutine("DestroyAfterDuration");
    }

    if (GetComponent<SpriteRenderer>() != null) { GetComponent<SpriteRenderer>().enabled = true; }
    if (GetComponent<BoxCollider2D>() != null) { GetComponent<BoxCollider2D>().enabled = true; }
    if (GetComponent<Rigidbody2D>() != null) {
      GetComponent<Rigidbody2D>().velocity = new Vector2(travelSpeed * (float)spellDirection, 0f);
    }
  }

  protected bool IsLibrarySpell() {
    return (transform.parent != null && transform.parent.gameObject.tag == "SpellLibrary");
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
