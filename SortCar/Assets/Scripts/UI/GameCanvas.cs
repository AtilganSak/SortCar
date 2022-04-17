using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour
{
    public DOFade endGamePanel;
    public TMP_Text messageText;
    public Image emojiImage;

    public Sprite sadSprite;
    public Sprite smileSprite;

    private void Start()
    {
        ActionManager.Instance.AddListener(Actions.GameFinish, () => GameEnded(true));
        ActionManager.Instance.AddListener(Actions.GameOver, () => GameEnded(false));
    }
    public void GameEnded(bool success)
    {
        if (success)
        {
            emojiImage.sprite = smileSprite;
            messageText.text = "Congratulations";
        }
        else
        {
            emojiImage.sprite = sadSprite;
            messageText.text = "Game Over!";
        }
        endGamePanel.DO();
    }
    public void PressedRestartButton()
    {
        ReferenceKeeper.Instance.GameManager.RestartGame();
    }
}
