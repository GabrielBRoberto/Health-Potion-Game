using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class EndVideo : MonoBehaviour
{
    private VideoPlayer player;

    [Header("Levels to Load")]
    public string _proximaCena;


    private void Start()
    {
        player = GetComponent<VideoPlayer>();

        player.loopPointReached += ChangeScene;
    }
    
    private void ChangeScene(VideoPlayer soruce)
    {
        SceneManager.LoadScene(_proximaCena);
    }

}
