using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehavior
{
    public static GameManager Instance;

    public GameState State;

    void Awake()
    {
        Instance = this;
    }

    public void ChangeState(GameState newState)
    {
        State = newState;
        
        switch (newState)
        {
        }
    }
}

public enum GameState
{
    TitleScreen,
    Tutorial,
    MainGame,
    LoseScreen,
    WinScreen
}
