using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float timeDelay = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] ParticleSystem successParticle;

    AudioSource audioSource;
    bool isTranstioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if(Input.GetKey(KeyCode.L)){
            LoadNextLevel();
        } else if (Input.GetKey(KeyCode.C)){
            collisionDisabled = !collisionDisabled;
        }
    }
    void OnCollisionEnter(Collision other) {
        if(isTranstioning || collisionDisabled){ return; }
        switch (other.gameObject.tag)
        {
            case "Friendly":
            Debug.Log("Game has been started");
            break;

            case "Finish":
            StartSuccessSequence();
            break;

            default:
            StartCrashSequence();
            break;
        }    
    }
    void StartSuccessSequence()
    {
        successParticle.Play();
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        isTranstioning = true;
        Invoke("LoadNextLevel", timeDelay);

    }

    void StartCrashSequence()
    {
        crashParticle.Play();
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        isTranstioning = true;
        Invoke("LoadLevel", timeDelay);
    }

    void LoadLevel()
        {
            int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentLevelIndex); 
        }
    void LoadNextLevel()
        {
            int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentLevelIndex + 1;
            if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }
            SceneManager.LoadScene(nextSceneIndex);
        }
}
