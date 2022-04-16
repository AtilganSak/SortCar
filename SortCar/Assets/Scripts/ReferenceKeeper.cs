using UnityEngine;

public class ReferenceKeeper : MonoBehaviour
{
    private static ReferenceKeeper instance;
    public static ReferenceKeeper Instance { get
        {
            if (instance == null)
                instance = FindObjectOfType<ReferenceKeeper>();
            return instance;
        } }

    public Door purpleDoor;
    public Door yellowDoor;
}
