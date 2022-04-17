using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    public void PressedRestartButton()
    {
        ReferenceKeeper.Instance.GameManager.RestartGame();
    }
}
