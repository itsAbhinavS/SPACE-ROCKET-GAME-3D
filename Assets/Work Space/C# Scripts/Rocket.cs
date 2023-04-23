using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;
using TMPro;


public class Rocket : MonoBehaviour
{
    //Manage Rocket
    public GameObject R2;

    [Space(20)]
    //death panel
    public GameObject puaseButton;
    public GameObject death_Panel;
    public GameObject pause_Panel;
    public GameObject next_Panel;
    public GameObject input_Panel;

    [Space(20)]
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 1500f;
    [SerializeField] float levelLoadDelay = 2f;

    //audio system
    [Space(20)]
    [SerializeField] AudioSource mainEngine;
    [SerializeField] AudioSource success;
    [SerializeField] AudioSource death;
    [SerializeField] AudioSource buttonAudio;
    [SerializeField] AudioSource musicAudio;

    //particle system
    [Space(20)]
    //[SerializeField] ParticleSystem MainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;


    [Space(20)]
    //Level text
    public TextMeshProUGUI currentLevel;


    //input
    int space = 0, left = 0, right = 0;
    float volume = 0.3f;


    //Particle
    public ParticleSystem R2_ParticleSystem;
    ParticleSystem currentParticleSystem;
    float incThrust;
    float decThrust;

    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State { Alive, Dead, Transcening }
    State state = State.Alive;

    bool CollisionAreDisabled = false;

    void Start()
    {
        success.volume = PlayerPrefs.GetFloat("effect"); 
        death.volume = PlayerPrefs.GetFloat("effect"); 
        buttonAudio.volume = PlayerPrefs.GetFloat("effect"); 
        musicAudio.volume = PlayerPrefs.GetFloat("music");

        currentLevel.text = SceneManager.GetActiveScene().buildIndex.ToString();

        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        R2.SetActive(true);
        currentParticleSystem = R2_ParticleSystem;
        decThrust = 1f;
        incThrust = 1.5f;
    }

    /*public void selectTheRocket(int selected)
    {
        R1.SetActive(false);
        R2.SetActive(false);

        switch (selected)
        {
            case 1:
                R1.SetActive(true);
                currentParticleSystem = R1_ParticleSystem;
                decThrust = 1f;
                incThrust = 1.5f;
                break;

            case 2:
                R2.SetActive(true);
                currentParticleSystem = R2_ParticleSystem;
                decThrust = 1f;
                incThrust = 1.5f;
                break;
        }
    }
*/
    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotationInput();


        }
        if (Debug.isDebugBuild)
        { RespondTotheDebugKeys(); }
    }


    void RespondTotheDebugKeys()
    {
        if (Input.GetKey(KeyCode.L))
        { StartSccessSequence(); }

        else if (Input.GetKey(KeyCode.C))
        {
            CollisionAreDisabled = !CollisionAreDisabled;
        }

    }



    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive || CollisionAreDisabled) { return; }

        switch (collision.gameObject.tag)
        {
            case "friendly":

                break;
            case "finish":
                StartSccessSequence();
                break;
            case "fuel":
                print("fuel added");
                break;
            default:
                StartDeathSequence();
                break;
        }

    }


    private void StartSccessSequence()
    {
        mainEngine.volume = 0f;
        Save();
        state = State.Transcening;
        audioSource.Stop();
        success.Play();
        successParticles.Play();
        Invoke("nextPanel", 2);
        puaseButton.SetActive(false);
        input_Panel.SetActive(false);
    }

    public void nextPanel() 
    {
        next_Panel.SetActive(true);
    }

    private void StartDeathSequence()
    {
        state = State.Dead;
        audioSource.Stop();
        deathParticles.Play();
        death.Play();
        pause_Panel.SetActive(false);
        input_Panel.SetActive(false);
        mainEngine.volume = 0;
        Invoke("deathPanel", levelLoadDelay);
    }


    public void PauseGame() 
    {
        Time.timeScale = 0f;
        mainEngine.volume = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        mainEngine.volume = 0.1f;
    }

    public void retryLevel() 
    {
        Invoke("LoadSceneDelayed", 0.1f);
    }

    public void LoadSceneDelayed() 
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevelIndex);
    }

    public void goToHomePage() 
    {
        SceneManager.LoadScene(0);
    }

    private void deathPanel()
    {
        death_Panel.SetActive(true);
    }

    private void Save()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex - 1;
        if (currentLevel + 1 != 30)
        {
            //__________________UPDATE LOCKED LEVEL___________________________
            // get the string from PlayerPrefs
            string lockedLevelString = PlayerPrefs.GetString("lockedLevel");
            // convert the string back to an array
            int[] lockedLevel = lockedLevelString.Split(',').Select(int.Parse).ToArray();
            lockedLevel[currentLevel + 1] = 0;
            // convert the array to a string
            lockedLevelString = string.Join(",", lockedLevel);
            // save the string to PlayerPrefs
            PlayerPrefs.SetString("lockedLevel", lockedLevelString);



            //__________________UPDATE COMPLETED LEVEL________________________
            // get the string from PlayerPrefs
            string completedLevelString = PlayerPrefs.GetString("completedLevel");
            // convert the string back to an array
            int[] completedLevel = completedLevelString.Split(',').Select(int.Parse).ToArray();
            completedLevel[currentLevel] = 1;
            // convert the array to a string
            completedLevelString = string.Join(",", completedLevel);
            // save the string to PlayerPrefs
            PlayerPrefs.SetString("completedLevel", completedLevelString);



            Debug.Log("LOCKED    : " + string.Join(",", lockedLevel));
            Debug.Log("COMPLETED : " + string.Join(",", completedLevel));
        }
        else 
        {

            //__________________UPDATE COMPLETED LEVEL________________________
            // get the string from PlayerPrefs
            string completedLevelString = PlayerPrefs.GetString("completedLevel");
            // convert the string back to an array
            int[] completedLevel = completedLevelString.Split(',').Select(int.Parse).ToArray();
            completedLevel[currentLevel] = 1;
            // convert the array to a string
            completedLevelString = string.Join(",", completedLevel);
            // save the string to PlayerPrefs
            PlayerPrefs.SetString("completedLevel", completedLevelString);


            Debug.Log("COMPLETED : " + string.Join(",", completedLevel));
        }
    }

    public void LoadScene() 
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextScene);
    }


    private void RespondToThrustInput()
    {
        float thrustPower = mainThrust;

        if (Input.GetKey(KeyCode.Space) | space == 1)
        {
            mainEngine.volume = Mathf.Lerp(mainEngine.volume, PlayerPrefs.GetFloat("effect"), 2f * Time.deltaTime);
            ApplyThrust(thrustPower);
            incLifetime();
        }
        else
        {
            mainEngine.volume = Mathf.Lerp(mainEngine.volume, 0.05f, 5f * Time.deltaTime);
            decLifetime();
            audioSource.Stop();
            //MainEngineParticles.Stop();
        }


    }

    private void ApplyThrust(float thrustPower)
    {
        rigidBody.AddRelativeForce(Vector3.up * thrustPower * Time.deltaTime); // for thrusting

        //if (!audioSource.isPlaying) // so it's not layering up
        //{
        //    audioSource.PlayOneShot(MainEngine);
        //}

        //MainEngineParticles.Play();
    }

    private void RespondToRotationInput()
    {

        float rotationSpeed = rcsThrust;

        rigidBody.freezeRotation = true; // take manual control

        if (Input.GetKey(KeyCode.A) | left == 1)

        {

            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D) | right == 1)
        {

            transform.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);
        }

        rigidBody.freezeRotation = false; //resume physics 
    }


    public void ButtonInput(string buttonLetter)
    {
        switch (buttonLetter)
        {
            //SPACE
            case "SPACE":
                space = 1;
                break;
            case "~SPACE":
                space = 0;
                break;

            //LEFT
            case "LEFT":
                left = 1;
                break;
            case "~LEFT":
                left = 0;
                break;


            //RIGHT
            case "RIGHT":
                right = 1;
                break;
            case "~RIGHT":
                right = 0;
                break;
        }
    }


    public void incLifetime()
    {
        var main = currentParticleSystem.main;
        main.startLifetime = incThrust;
    }

    public void decLifetime()
    {
        var main = currentParticleSystem.main;
        main.startLifetime = decThrust;
    }
}
