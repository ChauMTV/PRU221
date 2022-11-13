using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// My event type.
/// </summary>
[System.Serializable]
public class MyEvents : UnityEvent<GameObject, string>
{

}

/// <summary>
/// Message system.
/// </summary>
public class EventManagers : MonoBehaviour
{
    // Singleton
    public static EventManagers instance;

    // Events list
    private Dictionary<string, MyEvents> eventDictionary = new Dictionary<string, MyEvents>();

    /// <summary>
    /// Awake this instance.
    /// </summary>
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    /// <summary>
    /// Raises the destroy event.
    /// </summary>
    void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    /// <summary>
    /// Start listening specified event.
    /// </summary>
    /// <param name="eventName">Event name.</param>
    /// <param name="listener">Listener.</param>
    public static void StartListening(string eventName, UnityAction<GameObject, string> listener)
    {
        if (instance == null)
        {
            instance = FindObjectOfType(typeof(EventManagers)) as EventManagers;
            if (instance == null)
            {
                Debug.Log("Have no event manager on scene");
                return;
            }
        }
        MyEvents thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new MyEvents();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    /// <summary>
    /// Stop listening specified event.
    /// </summary>
    /// <param name="eventName">Event name.</param>
    /// <param name="listener">Listener.</param>
    public static void StopListening(string eventName, UnityAction<GameObject, string> listener)
    {
        if (instance == null)
        {
            return;
        }
        MyEvents thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    /// <summary>
    /// Trigger specified event.
    /// </summary>
    /// <param name="eventName">Event name.</param>
    /// <param name="obj">Object.</param>
    /// <param name="param">Parameter.</param>
    public static void TriggerEvent(string eventName, GameObject obj, string param)
    {
        if (instance == null)
        {
            return;
        }
        MyEvents thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(obj, param);
        }
    }


}
