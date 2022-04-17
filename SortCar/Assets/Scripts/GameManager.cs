using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameStarted;
    public int targetRightParking;

    private int carCounter;

    private void Start()
    {
        int purpleCarCount = ReferenceKeeper.Instance.purpleTrafficController.cars.Length;
        int yellowCarCount = ReferenceKeeper.Instance.yellowTrafficController.cars.Length;
        targetRightParking = purpleCarCount + yellowCarCount;

        StartGame();
    }
    public void StartGame()
    {
        if (!isGameStarted)
        {
            isGameStarted = true;

            ActionManager.Instance.Fire(Actions.GameStart);
        }
    }
    public void FinishGame()
    {
        if (isGameStarted)
        {
            Debug.Log("Finish Game");

            isGameStarted = false;

            ActionManager.Instance.Fire(Actions.GameFinish);
        }
    }
    public void GameOver()
    {
        if (isGameStarted)
        {
            Debug.Log("Game Over");

            isGameStarted = false;

            ActionManager.Instance.Fire(Actions.GameOver);
        }
    }
    public void CarIsDone()
    {
        if (isGameStarted)
        {
            carCounter++;
            if (carCounter >= targetRightParking)
            {
                carCounter = targetRightParking;
                FinishGame();
            }
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
