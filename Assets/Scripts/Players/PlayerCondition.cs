using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
  public uint verbalDisableCount = 0;
  public uint somaticDisableCount = 0;
  public uint materialDisableCount = 0;

  public Dictionary<string, SpellCondition> activeConditions = new Dictionary<string, SpellCondition>();

  public void AddCondition(SpellCondition condition) {
    if (activeConditions.ContainsKey(condition.name)) {
      activeConditions[condition.name].duration += condition.duration;
      return;
    }

    activeConditions.Add(condition.name, condition);

    if (condition.disableVerbal) { verbalDisableCount++; }
    if (condition.disableSomatic) { somaticDisableCount++; }
    if (condition.disableMaterial) { materialDisableCount++; }

    StartCoroutine(CountConditionDuration(condition.name));
  }

  void RemoveCondition(string conditionName) {
    if (!activeConditions.ContainsKey(conditionName)) {
      return;
    }

    SpellCondition condition = activeConditions[conditionName];

    if (condition.disableVerbal) { verbalDisableCount--; }
    if (condition.disableSomatic) { somaticDisableCount--; }
    if (condition.disableMaterial) { materialDisableCount--; }

    activeConditions.Remove(conditionName);
    Destroy(condition.gameObject);
  }

  IEnumerator CountConditionDuration(string conditionName) {
    float durationElapsed = 0f;
    yield return null;

    while (durationElapsed < activeConditions[conditionName].duration) {
      durationElapsed += Time.deltaTime;
      yield return null;
    }

    RemoveCondition(conditionName);
  }
}
