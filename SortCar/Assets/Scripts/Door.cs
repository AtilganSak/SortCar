using UnityEngine;

public class Door : MonoBehaviour
{
    private DORotate doRotate;

    [SerializeField] bool opened;

    private void OnEnable()
    {
        doRotate = GetComponent<DORotate>();
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
