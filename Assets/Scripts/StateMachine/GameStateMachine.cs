using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateMachine : MonoBehaviour
{
    public static event Action<IState> OnGameStateChanged;

    private static GameStateMachine _instance;
    
    private StateMachine _stateMachine;

    public Type CurrentStateType => _stateMachine.CurrentState.GetType();

    private void Awake()
    {
        if (_instance != null) {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        DontDestroyOnLoad(gameObject);

        _stateMachine = new StateMachine();
        _stateMachine.OnStateChanged += state => OnGameStateChanged?.Invoke(state);

        var menu = new Menu();
        var loading = new LoadLevel();
        var play = new Play();
        var pause = new Pause();

        _stateMachine.SetState(menu);

        _stateMachine.AddTransition(loading, play, loading.Finished);
        _stateMachine.AddTransition(play, pause, () => PlayerInput.Instance.PausedPressed);
        _stateMachine.AddTransition(pause, play, () => PlayerInput.Instance.PausedPressed);
        _stateMachine.AddTransition(pause, menu, () => RestartButton.Pressed);
        _stateMachine.AddTransition(menu, loading, () => PlayButton.LevelToLoad != null);
    }

    private void Update()
    {
        _stateMachine.Tick();
    }
}

public class Menu : IState
{
    public void OnEnter()
    {
        PlayButton.LevelToLoad = null;
        SceneManager.LoadSceneAsync("Menu");
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
    }
}

public class Play : IState
{
    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
    }
}

public class LoadLevel : IState
{
    private List<AsyncOperation> _operations = new List<AsyncOperation>();
    public bool Finished() => _operations.TrueForAll(operation => operation.isDone);

    public void OnEnter()
    {
        _operations.Clear();

        _operations.Add(SceneManager.LoadSceneAsync(PlayButton.LevelToLoad));
        _operations.Add(SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive));
    }

    public void OnExit() { }

    public void Tick() { }
}

public class Pause : IState
{
    public static bool Active { get; private set; }

    public void OnEnter()
    {
        Active = true;
        Time.timeScale = 0f;
    }

    public void OnExit()
    {
        Active = false;
        Time.timeScale = 1f;
    }

    public void Tick()
    {
    }
}
