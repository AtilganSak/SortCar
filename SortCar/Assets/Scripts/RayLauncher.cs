using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayLauncher : MonoBehaviour
{
    public LayerMask buttonLayer;

    private Camera camera;
    private RaycastHit raycastHit;

    private void OnEnable()
    {
        camera = Camera.main;
    }
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out raycastHit, buttonLayer))
                {
                    TriggerButton triggerButton = raycastHit.collider.GetComponent<TriggerButton>();
                    if (triggerButton != null)
                    {
                        triggerButton.ButtonDown();
                    }
                }
            }
        }
    }
}
