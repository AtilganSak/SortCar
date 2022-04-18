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

    public GameManager GameManager;
    public TrafficController purpleTrafficController;
    public TrafficController yellowTrafficController;
    public Door purpleDoor;
    public Door yellowDoor;

    [SerializeField] LevelSettings levelSettings;
    public LevelSettings LevelSettings { get
        {
            if (levelSettings == null)
                levelSettings = Resources.Load<LevelSettings>("LevelSettings");
            return levelSettings;
        } }
}
