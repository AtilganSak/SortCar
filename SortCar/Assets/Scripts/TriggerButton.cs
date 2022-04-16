using UnityEngine;
using UnityEngine.Events;

public class TriggerButton : MonoBehaviour
{
    public UnityEvent onButtonDown;

    public DOMove DOMove { get; private set; }

    private void OnEnable()
    {
        DOMove = GetComponentInChildren<DOMove>();
    }
    public void ButtonDown()
    {
        onButtonDown.Invoke();
    }
}
