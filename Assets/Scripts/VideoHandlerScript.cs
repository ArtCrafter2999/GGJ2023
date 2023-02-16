using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoHandlerScript : MonoBehaviour
{
    public VideoPlayer player;
    public PlayerControlls inputs;
    public int NumberOfNextScene;
    void Start()
    {
        inputs = new PlayerControlls();
        player.loopPointReached += a => ChangeScene();
        inputs.UI.SkipMovie.performed += a => ChangeScene();
        inputs.UI.Enable();
    }

    private void ChangeScene()
    {
        inputs.Disable();
        SceneManager.LoadScene(NumberOfNextScene);
    }
}
