using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject Player;
    public Player PlayerComponent => Player.GetComponent<Player>();
    public PlayerController PlayerController => Player.GetComponent<PlayerController>();
    public PlayerControlls Controlls { get; private set; }
    public bool IsPause = false;
    private void Awake()
    {
        Instance = this;

        Controlls = new PlayerControlls();
        Controlls.UI.Pause.performed += (ctx) => Pause();
        Controlls.UI.Enable();

        //DontDestroyOnLoad(gameObject);
    }

    public event Action OnGameTick;
    private void Update()
    {
        OnGameTick?.Invoke();
    }
    private void Pause()
    {
        Time.timeScale = IsPause ? 1 : 0;
        IsPause = !IsPause;
    }
    public void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
    //private void SceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    Debug.Log("Scene loaded");
    //    SceneManager.UnloadSceneAsync(_currentScene);
    //    Debug.Log("Scene unload start");
    //    SceneManager.sceneUnloaded += sc => Debug.Log("Scene unloaded");
    //}
}
