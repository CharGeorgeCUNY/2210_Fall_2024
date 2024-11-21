using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    GAME_BEGIN,
    GAME_PLAY,
    GAME_LOSE,
    GAME_WIN
}

public class GameManager : MonoBehaviour
{
    private static GameManager _Instance;
    public GameObject WinStuff;
    public GameObject LoseStuff;
    public GameObject BeginStuff;

    private GameState CurrentState;

    public GameObject ShipPrefab;
    public GameObject AsteroidPrefab;

    public GameObject ShipInGame;

    public GameState GetGameState()
    {
        return CurrentState;
    }

    public static GameManager GetGameManager()
    {
        if(_Instance == null)
        {
            _Instance = FindObjectOfType<GameManager>();
        }
        return _Instance;
    }
    int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        //List<Bullet> bullets = new List<Bullet>( GameObject.FindObjectsOfType<Bullet>());
        ChangeState(GameState.GAME_BEGIN);
    }

    // Update is called once per frame
    void Update()
    {
        switch(CurrentState)
        {
            case GameState.GAME_BEGIN:
                Debug.Log("In Begin");
                break;
            case GameState.GAME_LOSE:
                Debug.Log("In GAME_LOSE");
                break;
            case GameState.GAME_PLAY:
                if(ShipInGame == null)
                {
                    ChangeState(GameState.GAME_LOSE);
                }
                break;
            case GameState.GAME_WIN:
                Debug.Log("In GAME_WIN");
                break;
        }
    }

    public void StartGame()
    {
        ChangeState(GameState.GAME_PLAY);
    }

    public void ChangeState(GameState NewState)
    {
        //DO something for ending state
        switch (CurrentState)
        {
            case GameState.GAME_BEGIN:
                Debug.Log("In end Begin");
                break;
            case GameState.GAME_LOSE:
                Debug.Log("In end GAME_LOSE");
                break;
            case GameState.GAME_PLAY:
                Debug.Log("In end GAME_PLAY");
                if(ShipInGame != null)
                {
                    Destroy(ShipInGame);
                }    
                break;
            case GameState.GAME_WIN:
                Debug.Log("In end GAME_WIN");
                break;
        }
        CurrentState = NewState;
        //Do Something for Beginning state
        switch (CurrentState)
        {
            case GameState.GAME_BEGIN:
                BeginStuff.SetActive(true);
                WinStuff.SetActive(false);
                LoseStuff.SetActive(false);
                break;
            case GameState.GAME_LOSE:
                LoseStuff.SetActive(true) ;
                break;
            case GameState.GAME_PLAY:
                BeginStuff.SetActive(false);
                WinStuff.SetActive(false);
                LoseStuff.SetActive(false);

                ShipInGame = GameObject.Instantiate(ShipPrefab);
                GameObject.Instantiate(AsteroidPrefab, new Vector3(-4f,-4f, 0f), Quaternion.identity);
                break;
            case GameState.GAME_WIN:
                WinStuff.SetActive(true);
                break;
        }

    }

    public void IncrementScore()
    {
        score++;
        Debug.Log(score);
    }
}
