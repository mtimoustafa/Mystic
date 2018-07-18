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
  public bool isSingleton = false;
  public bool isWard = false;

  [HideInInspector]
  public string belongsTo = "";

  float timeAlive = 0f;

  void Start() {
    if (duration > 0f && (transform.parent == null || transform.parent.gameObject.tag != "SpellLibrary")) {
      StartCoroutine("DestroyAfterDuration");
    }
  }

  void Update() {
    timeAlive += Time.deltaTime;
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
    while (timeAlive < duration) {
      yield return null;
    }
    Destroy(gameObject);
  }
}
