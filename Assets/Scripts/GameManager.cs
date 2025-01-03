using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameStates GameState { private set; get; }
    public static event System.Action<GameStates> onGameStateChanges;

    public static GameManager instance;

    void Awake()
    {
        if(instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        GameState = GameStates.Playing;
        onGameStateChanges?.Invoke(GameState);
    }

    public void FinishGame()
    {
        GameState = GameStates.Finished;
        onGameStateChanges?.Invoke(GameState);
    }
}

public enum GameStates
{
    Playing,
    Finished,
}