using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int currentLevelIndex, scoreToCompleteCurrentLevel, currentScore, currentLives;

    public GameObject player;

    public LevelScriptableObject[] levels;

    void Awake()
    {
        if (!instance)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        levels = Resources.LoadAll<LevelScriptableObject>("ScriptableObjects/Levels");

        Debug.Log("found " + levels.Length + " levels");

        PopupHandler.instance.TogglePopup(true, "Press Start!", "Start", () => 
        {
            BrickCreator.instance.Configure(levels[currentLevelIndex]);
            BrickCreator.instance.Generate();
            Configure(levels[currentLevelIndex]);
            BallPooler.instance.SpawnNewBallFromPool();
            PopupHandler.instance.TogglePopup(false);
        });
    }

    public void AddScore(GameObject currentBall)
    {
        currentScore++;

        if(currentScore >= scoreToCompleteCurrentLevel)
        {
            BallPooler.instance.PutBallBackIntoPool(currentBall, false);

            if (currentLevelIndex + 1 <= levels.Length - 1)
                PromptNextLevel();
            else
                PromptNoMoreLevels();
        }
    }

    public void Configure(LevelScriptableObject level)
    {
        scoreToCompleteCurrentLevel = level.rows * level.columns;
        currentScore = 0;
        currentLives = level.lives;
        UIHandler.instance.SetLevelText(currentLevelIndex + 1);
        UIHandler.instance.SetLivesText(currentLives);
    }

    public void PromptRestartLevel()
    {
        PopupHandler.instance.TogglePopup(true, "You failed :(", "Restart", () =>
        {
            BrickCreator.instance.Configure(levels[currentLevelIndex]);
            BrickCreator.instance.Generate();
            Configure(levels[currentLevelIndex]);
            BallPooler.instance.SpawnNewBallFromPool();
            PopupHandler.instance.TogglePopup(false);
        });
    }

    public void PromptNextLevel()
    {
        PopupHandler.instance.TogglePopup(true, "Good Job!", "Next", () =>
        {
            BrickCreator.instance.Configure(levels[++currentLevelIndex]);
            BrickCreator.instance.Generate();
            Configure(levels[currentLevelIndex]);
            BallPooler.instance.SpawnNewBallFromPool();
            PopupHandler.instance.TogglePopup(false);
        });
    }

    public void PromptNoMoreLevels()
    {
        PopupHandler.instance.TogglePopup(true, "You finished the game!", "Quit", () =>
        {
            Application.Quit();
        });
    }
}
