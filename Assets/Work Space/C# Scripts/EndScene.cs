using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    public AudioSource backgroundAudio;
    public AudioSource buttonAudio;

    private void Start()
    {
        backgroundAudio.volume = PlayerPrefs.GetFloat("music");
        buttonAudio.volume = PlayerPrefs.GetFloat("effect");
    }

    public void loadMenuScene()
    {
        Invoke("MenuScene", 0.3f);
    }

    private void MenuScene() 
    {
        SceneManager.LoadScene(0);
    }
}
