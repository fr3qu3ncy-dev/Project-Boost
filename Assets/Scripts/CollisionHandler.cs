using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float crashDelay = 1F;
    [SerializeField] private float nextLevelDelay = 1F;
    
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AudioClip landingSound;

    [SerializeField] private ParticleSystem successParticles;
    [SerializeField] private ParticleSystem crashParticles;

    private AudioSource audioSource;

    private bool isTransitioning;
    private bool ignoreCollisions;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        CheckDebugKeys();
    }

    private void CheckDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ignoreCollisions = !ignoreCollisions;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || ignoreCollisions) return;
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                StartNextLevelSequence();
                Debug.Log("You finished!");
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartCrashSequence()
    {
        isTransitioning = true;
        PlaySound(crashSound);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(RestartLevel), crashDelay);
    }

    private void StartNextLevelSequence()
    {
        isTransitioning = true;
        PlaySound(landingSound);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadNextLevel), nextLevelDelay);
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(clip);
    }
    
    private void RestartLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevel, LoadSceneMode.Single);
        isTransitioning = false;
    }

    private void LoadNextLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int nextLevel = currentLevel + 1;
        if (SceneManager.sceneCountInBuildSettings == nextLevel)
        {
            nextLevel = 0;
        }
        
        SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
        isTransitioning = false;
    }
}
