using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killbox : MonoBehaviour {
  void OnTriggerExit2D(Collider2D collider) {
    // Destroy anything that collides into the killbox
    Destroy(collider.gameObject);
  }
}
