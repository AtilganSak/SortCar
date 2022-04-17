using UnityEngine;
using UnityEngine.Events;

public class TriggerButton : MonoBehaviour
{
    public UnityEvent onButtonDown;

    public DOMove DOMove { get; private set; }

    private BoxCollider collider;

    private void OnEnable()
    {
        DOMove = GetComponentInChildren<DOMove>();

        collider = GetComponent<BoxCollider>();

        ActionManager.Instance.AddListener(Actions.GameFinish, () => collider.enabled = false);
        ActionManager.Instance.AddListener(Actions.GameOver, () => collider.enabled = false);
    }
    public void ButtonDown()
    {
        onButtonDown.Invoke();
    }
}
