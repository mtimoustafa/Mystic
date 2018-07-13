using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
  public RuneGridController runeGridController;

  // Use this for initialization
  void Start () {
    runeGridController.SetupGrid();
  }

  // Update is called once per frame
  void Update () {
  }
}
