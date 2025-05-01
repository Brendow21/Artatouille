using UnityEngine;
using UnityEngine.Video;

public class TV : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public MeshRenderer tvScreen;

    private bool powered;

    public void toggleVideo() {
        if(!powered) {
            return;
        }
        if(videoPlayer.isPlaying) {
            videoPlayer.Pause();
            return;
        }
        videoPlayer.Play();
    }

    public void togglePower() {
        if(powered) {
            powered = false;
            tvScreen.enabled = false;
            videoPlayer.Pause();
            return;
        }
        powered = true;
        tvScreen.enabled = true;
        videoPlayer.Play();
    }
}
