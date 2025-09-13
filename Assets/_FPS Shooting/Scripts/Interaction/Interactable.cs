using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MyIntEvent : UnityEvent<int>
{
}
public class Interactable : MonoBehaviour
{
    [Range(1f, 10f)]
    public float interactRange = 3f;
    public string description;
    public UnityEvent onInteract;
    public bool destroyAfterInteraction;

    public virtual void Start()
    {

    }

    public virtual void Interact()
    {
        onInteract.Invoke();

    }
}
