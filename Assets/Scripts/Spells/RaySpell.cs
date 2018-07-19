using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaySpell : Spell {
  public float rayDuration = 0.5f;
  public float rayMaxLength = 20f;
  void Start() {
    // Don't mess with spell library spells as the spell library is a factory
    if (IsLibrarySpell()) { return; }

    RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2((float)spellDirection, 0f));

    if (hit.collider != null) {
      if (hit.collider.gameObject.tag == "Player") {
        hit.collider.gameObject.GetComponent<Player>().ApplySpellEffects(this);
      } 
      StartCoroutine("DestroyWithAnimation", hit.distance);
    } else {
      StartCoroutine("DestroyWithAnimation", rayMaxLength);
    }
  }

  IEnumerator DestroyWithAnimation(float length) {
    transform.localScale = new Vector2(length, transform.localScale.y);
    transform.position = new Vector2(transform.position.x + (length / 2 * (float)spellDirection), transform.position.y);
    GetComponent<SpriteRenderer>().enabled = true;

    float durationElapsed = 0f;
    yield return null;

    while (durationElapsed < rayDuration) {
      durationElapsed += Time.deltaTime;
      yield return null;
    }

    Destroy(gameObject);
  }
}
