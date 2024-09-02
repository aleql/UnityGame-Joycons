using System.Collections;
using TMPro;
using UnityEngine;

public class FlappyBirdController : MonoBehaviour
{
    // Singleton
    public static FlappyBirdController Instance;

    // Objects Default Positions
    //private Vector3 _parallax1InitPosition;
    //private Vector3 _parallax2InitPosition;

    //private Vector3 _obstacleGeneratorInitPosition;
    //private Vector3 _appleGeneratorInitPosition;

    //private Vector3 _playerInitPosition;

    //private Vector3 _gameplayManagerInitPosition;


    //Game States
    public enum FlappyBirdGameStates
    {
        StandBy,
        Countdown,
        Playing,
        GameOver
    }
    private int _currentGameStateIndex;
    public int CurrentGameStateIndex
    {
        get { return _currentGameStateIndex; }
        set { 
            _currentGameStateIndex = value;
            currentGameState = _gameStateSequence[value];
        }
    }
    public FlappyBirdGameStates currentGameState;
    private FlappyBirdGameStates[] _gameStateSequence;

    // Controller Scripts
    [SerializeField] private GameplayManager _gameplayManager;
    [SerializeField] private Terrain _parallaxTerrain1;
    [SerializeField] private Terrain _parallaxTerrain2;
    [SerializeField] private BirdPlayerController _birdPlayerController;
    [SerializeField] private Generador _obstacleGeneratorController;
    [SerializeField] private GeneradorManzana _appleGeneratorController;

    // UI
    [SerializeField] private CanvasGroup _standByCanvasGroup;
    [SerializeField] private CanvasGroup _countdownCanvasGroup;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private CanvasGroup _playCanvasGroup;
    [SerializeField] private CanvasGroup _gameOverCanvasGroup;
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField] private CanvasGroup _gameOverMenuCanvasGroup;
    [SerializeField] private TextMeshProUGUI _scoreScreenText;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        _gameStateSequence = new FlappyBirdGameStates[4] { FlappyBirdGameStates.StandBy, FlappyBirdGameStates.Countdown, FlappyBirdGameStates.Playing, FlappyBirdGameStates.GameOver };
        CurrentGameStateIndex = 0;
        GameStateAction();

        // TESTING
        StartCoroutine(TestingSequence());
    }

    public void NextGameState()
    {
        CurrentGameStateIndex = (CurrentGameStateIndex + 1) % 4;
        GameStateAction();
    }

    private void GameStateAction()
    {
        FlappyBirdGameStates currentState = _gameStateSequence[CurrentGameStateIndex];
        switch (currentState)
        {
            case FlappyBirdGameStates.StandBy:
                // Change UI
                _standByCanvasGroup.alpha = 1;
                _gameOverCanvasGroup.alpha = 0;

                // Reset Positions
                // _gameplayManager.transform.position = _gameplayManagerInitPosition;
                // _parallaxTerrain1.transform.position = _parallax1InitPosition;
                // _parallaxTerrain2.transform.position = _parallax2InitPosition;
                // _birdPlayerController.transform.position = _playerInitPosition;
                // _obstacleGeneratorController.transform.position = _obstacleGeneratorInitPosition;
                // _appleGeneratorController.transform.position = _appleGeneratorInitPosition;

                break;

            case FlappyBirdGameStates.Countdown:
                // Change UI
                _standByCanvasGroup.alpha = 0;
                _countdownCanvasGroup.alpha = 1;

                // Start Countdouwn
                StartCoroutine(CountdownAction());
                break;

            case FlappyBirdGameStates.Playing:
                // Change UI
                _countdownCanvasGroup.alpha = 0;
                _playCanvasGroup.alpha = 1;

                // set up each game


                break;

            case FlappyBirdGameStates.GameOver:
                // Bird fly
                BirdPlayerController.Instance.animator.SetTrigger("Leave");

                // Set score in game over screen
                _scoreScreenText.text = PuntajeCanvas.puntaje.ToString();

                // Enable UI
                _playCanvasGroup.alpha = 0;
                _gameOverCanvasGroup.alpha = 1;

                // Stop Everything
                StartCoroutine(GameOverAction());

                // Save game
                break;



        }

    }
    IEnumerator CountdownAction()
    {
        int timerCount = 3;
        while (timerCount > 0)
        {
            countdownText.text = timerCount.ToString();
            yield return new WaitForSeconds(1);
            timerCount--;
        }
        countdownText.text = "GO!";
        yield return new WaitForSeconds(1);
        NextGameState();
    }

    IEnumerator GameOverAction()
    {
        yield return new WaitForSeconds(2);
        _birdPlayerController.gameObject.SetActive(false);

        yield return new WaitForSeconds(5);
        _gameOverText.gameObject.SetActive(false);
        _gameOverMenuCanvasGroup.alpha = 1;
    }

    IEnumerator TestingSequence()
    {
        for (int i = 0; i < 5; i++)
        {
            
            yield return new WaitForSeconds(5);
            if (_gameStateSequence[CurrentGameStateIndex] == FlappyBirdGameStates.Playing)
            {
                yield return null;
            }
            else
            {
                NextGameState();

            }


        }
        
    }

}
