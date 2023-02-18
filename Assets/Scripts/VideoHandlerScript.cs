using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoHandlerScript : MonoBehaviour
{
    public VideoPlayer player;
    public PlayerControlls inputs;
    public int NumberOfNextScene;
    public string fileName;

    void Start()
    {
        player.source = VideoSource.Url;
        player.url = Path.Combine(Application.streamingAssetsPath, fileName);

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
