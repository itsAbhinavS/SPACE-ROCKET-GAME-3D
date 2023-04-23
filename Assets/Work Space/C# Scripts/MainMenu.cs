using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //images
    public GameObject[] lockedLevelImage;
    public GameObject[] greenticksImage;


    [Space(20)]
    public Slider Music_slider;
    public Slider Effect_slider;

    [Space(20)]
    public AudioSource backgroundAudio;
    public AudioSource buttonAudio;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("save"))
        {
            PlayerPrefs.SetFloat("music", 1f);
            PlayerPrefs.SetFloat("effect", 1f);

            //__________________LOCKED LEVEL___________________________
            // create an array
            int[] lockedLevel = new int[] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            // convert the array to a string
            string lockedLevelString = string.Join(",", lockedLevel);
            // save the string to PlayerPrefs
            PlayerPrefs.SetString("lockedLevel", lockedLevelString);


            //__________________COMPLETED LEVEL________________________
            // create an array
            int[] completedLevel = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            // convert the array to a string
            string completedLevelString = string.Join(",", completedLevel);
            // save the string to PlayerPrefs
            PlayerPrefs.SetString("completedLevel", completedLevelString);


            //Set the flag of save system to 1
            PlayerPrefs.SetInt("save", 1);

            load();

        }
        else
        {
            load();
        }
    }

    public void load()
    {
        Music_slider.value = PlayerPrefs.GetFloat("music"); 
        Effect_slider.value = PlayerPrefs.GetFloat("effect");

        backgroundAudio.volume = PlayerPrefs.GetFloat("music");
        buttonAudio.volume = PlayerPrefs.GetFloat("effect");

        //__________________LOCKED LEVEL___________________________
        // get the string from PlayerPrefs
        string lockedLevelString = PlayerPrefs.GetString("lockedLevel");
        // convert the string back to an array
        int[] lockedLevel = lockedLevelString.Split(',').Select(int.Parse).ToArray();

        for (int i = 0; i < 30; i++) 
        {
            if (lockedLevel[i] == 1)
            {
                lockedLevelImage[i].SetActive(true);
            }
            else 
            {
                lockedLevelImage[i].SetActive(false);
            }
        }


        //__________________COMPLETED LEVEL________________________
        // get the string from PlayerPrefs
        string completedLevelString = PlayerPrefs.GetString("completedLevel");
        // convert the string back to an array
        int[] completedLevel = completedLevelString.Split(',').Select(int.Parse).ToArray();

        for (int i = 0; i < 30; i++)
        {
            if (completedLevel[i] == 1)
            {
                greenticksImage[i].SetActive(true);
            }
            else
            {
                greenticksImage[i].SetActive(false);
            }
        }




        Debug.Log("LOCKED    : " + string.Join(",", lockedLevel));
        Debug.Log("COMPLETED : " + string.Join(",", completedLevel));
    }


    public void saveMusicAudioVolume()
    {
        PlayerPrefs.SetFloat("music", Music_slider.value);
        backgroundAudio.volume = Music_slider.value;
    }

    public void saveEffectAudioVolume()
    {
        PlayerPrefs.SetFloat("effect", Effect_slider.value);
        buttonAudio.volume = Effect_slider.value;
    }



    public void deleteAllSaves()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("ALL DELETED !!");
    }

    
    public void loadLevelScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void quitGame() 
    {
        Application.Quit();
    }

}
