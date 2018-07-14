using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour {
  Dictionary<string, UnityEvent> eventDictionary;

  public void Start() {
    eventDictionary = new Dictionary<string, UnityEvent>();
  }

  public void StartListening(string eventName, UnityAction listener) {
    UnityEvent thisEvent = null;
    if (eventDictionary.TryGetValue(eventName, out thisEvent)) {
      thisEvent.AddListener(listener);
    } else {
      thisEvent = new UnityEvent();
      thisEvent.AddListener(listener);
      eventDictionary.Add(eventName, thisEvent);
    }
  }

  public void StopListening(string eventName, UnityAction listener) {
    UnityEvent thisEvent = null;
    if (eventDictionary.TryGetValue(eventName, out thisEvent)) {
      thisEvent.RemoveListener(listener);
    } else {
      Debug.LogWarning("Could not find event to remove: " + eventName);
    }
  }

  public void TriggerEvent(string eventName) {
    UnityEvent thisEvent = null;
    if (eventDictionary.TryGetValue(eventName, out thisEvent)) {
      thisEvent.Invoke();
    } else {
      Debug.LogError("Could not find event to trigger: " + eventName);
    }
  }
}
