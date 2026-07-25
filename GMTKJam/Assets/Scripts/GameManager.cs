using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    #region public resources
    [Header("Cameras")]
    public Camera _camera;
    public Camera _displayCamera;
    public CameraMovement _cameraMovement;
    public GameObject _fpCamera;
    public CinemachineInputAxisController _axisController;
    public DeathPPE _deathPPE;
    [Header("Player Input")]
    public PlayerInput _playerInput;
    public PlayerMovement _playerMovement;
    public GameObject _rightHand;
    #endregion
    public static GameManager Instance { get; private set; }

    // Difficulty management variables

    public bool gameStarted = false;

    public float timeTillEnd = 120f; // Determines the time in seconds until the game reaches its maximum difficulty level... (character dies)

    private float timeSinceStart = 0f; // Tracks the amount of time in seconds since the game has started
    private float difficultyValue = 0f; // Value between 0 and 1 representing the current difficulty level

    private DifficultyLevel currentDifficultyLevel;

    // Thresholds required for difficulty changes
    private float easyThreshold = 0f;
    private float mediumThreshold = 0.5f;
    private float hardThreshold = 0.8f;

    public Action<DifficultyLevel> onDifficulyAltered;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
            UpdateDifficulty();
    }

    public void StartGame()
    {
        timeSinceStart = 0f;
        gameStarted = true;

        difficultyValue = 0f;
    }

    public void EndGame(bool hasLost)
    {
        gameStarted = false;

        if (hasLost)
            Debug.Log("You lose!");
        else
            Debug.Log("You win!");

        // Implement game over logic here... hasLost determines whether or not the player lost the game
    }

    private void UpdateDifficulty()
    {
        DifficultyLevel nextDifficultyLevel = DifficultyLevel.Undefined;

        timeSinceStart += Time.deltaTime;

        if(timeSinceStart >= timeTillEnd)
        {
            EndGame(true);
            return;
        }

        difficultyValue = timeSinceStart / timeTillEnd;

        if(difficultyValue > hardThreshold)
            nextDifficultyLevel = DifficultyLevel.Hard;
        else if (difficultyValue > mediumThreshold)
            nextDifficultyLevel = DifficultyLevel.Medium;
        else if (difficultyValue > easyThreshold)
            nextDifficultyLevel = DifficultyLevel.Easy;

        if(nextDifficultyLevel != currentDifficultyLevel)
        {
            SetDifficultyLevel(nextDifficultyLevel);
        }
    }

    private void SetDifficultyLevel(DifficultyLevel newLevel)
    {
        onDifficulyAltered?.Invoke(newLevel);
        currentDifficultyLevel = newLevel;
    }
    
    public bool IsGameStarted() => gameStarted;

    public DifficultyLevel CurrentDifficultyLevel() => currentDifficultyLevel;

    public float GetDifficultyValue() => difficultyValue;
}

public enum DifficultyLevel
{
    Undefined,
    Easy,
    Medium,
    Hard
}