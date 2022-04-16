using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderDetector : MonoBehaviour
{
    public bool isActive = true;
    public string targetTag = "Player";

    public bool isActiveTriggerEnter = true;
    public UnityEvent OnTriggerEnterEvent;
    public bool isActiveTriggerStay = true;
    public UnityEvent OnTriggerStayEvent;
    public bool isActiveTriggerExit = true;
    public UnityEvent OnTriggerExitEvent;

    GameObject otherObject;

    public void SetActive(bool _state)
    {
        isActive = _state;
    }
    public void SetActiveTriggerEnter(bool _state)
    {
        isActiveTriggerEnter = _state;
    }
    public void SetActiveTriggerStay(bool _state)
    {
        isActiveTriggerStay = _state;
    }
    public void SetActiveTriggerExit(bool _state)
    {
        isActiveTriggerExit = _state;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!isActive) return;
        if(!isActiveTriggerEnter) return;
        if (targetTag != "" && other.tag != targetTag) return;
        otherObject = other.gameObject;
        OnTriggerEnterEvent.Invoke();
    }
    private void OnTriggerStay(Collider other)
    {
        if(!isActive) return;
        if(!isActiveTriggerStay) return;
        if(targetTag != "" && other.tag != targetTag) return;

        OnTriggerStayEvent.Invoke();
    }
    private void OnTriggerExit(Collider other)
    {
        if(!isActive) return;
        if(!isActiveTriggerExit) return;
        if(targetTag != "" && other.tag != targetTag) return;
        otherObject = null;
        OnTriggerExitEvent.Invoke();
    }
    public void DestroyOther()
    {
        if (otherObject)
        {
            Destroy(otherObject);
        }
    }
}
