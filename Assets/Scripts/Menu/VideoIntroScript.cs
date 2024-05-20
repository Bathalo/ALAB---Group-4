using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoIntroScript : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName = "MainGame";

    private void Start()
    {
        videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextSceneName);
    }

}
