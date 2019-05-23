using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCastComponents : MonoBehaviour
{
  public Player owner;
  public bool verbal = true;
  public bool somatic = true;
  public bool material = true;

  PlayerCondition _playerCondition;
  Text _verbalIcon;
  Text _somaticIcon;
  Text _materialIcon;

  void Start() {
    _playerCondition = owner.GetComponent<PlayerCondition>();
    _verbalIcon = gameObject.transform.Find("Verbal").GetComponent<Text>();
    _somaticIcon = gameObject.transform.Find("Somatic").GetComponent<Text>();
    _materialIcon = gameObject.transform.Find("Material").GetComponent<Text>();
  }

  void Update() {
    verbal = _playerCondition.verbalDisableCount == 0;
    somatic = _playerCondition.somaticDisableCount == 0;
    material = _playerCondition.materialDisableCount == 0;

    UpdateIconColors();
  }

  void UpdateIconColors() {
    _verbalIcon.color = ColorIcon(verbal);
    _somaticIcon.color = ColorIcon(somatic);
    _materialIcon.color = ColorIcon(material);
  }

  Color ColorIcon(bool active) {
    return active ? Color.green : Color.red;
  }
}
