using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
  void Start () {
    GameObject.FindGameObjectsWithTag("RuneGrid")[0].GetComponent<RuneGrid>().SetupGrid();
  }
}
