using UnityEngine;

public class Door : MonoBehaviour
{
    public Team team;

    private DORotate doRotate;

    [SerializeField] bool opened;

    private void OnEnable()
    {
        doRotate = GetComponent<DORotate>();

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.materials[1].color = ReferenceKeeper.Instance.LevelSettings.GetColorByTeam(team);
        }
    }
    [EasyButtons.Button]
    public void Open()
    {
        if (!opened)
        {
            opened = true;
            doRotate.DO();
        }
    }
    [EasyButtons.Button]
    public void Close()
    {
        if (opened)
        {
            opened = false;
            doRotate.DORevert();
        }
    }
}
