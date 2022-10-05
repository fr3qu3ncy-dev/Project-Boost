using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float mainThrust;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private AudioClip mainEngine;
    
    [SerializeField] private ParticleSystem thrustLeftParticles;
    [SerializeField] private ParticleSystem thrustRightParticles;
    [SerializeField] private ParticleSystem thrustMainParticles;
    
    private new Rigidbody rigidbody;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 120;
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
    
    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    private void StopRotating()
    {
        thrustLeftParticles.Stop();
        thrustRightParticles.Stop();
    }

    private void RotateRight()
    {
        if (!thrustLeftParticles.isPlaying) thrustLeftParticles.Play();
        ApplyRotation(-rotationSpeed);
    }

    private void RotateLeft()
    {
        if (!thrustRightParticles.isPlaying) thrustRightParticles.Play();
        ApplyRotation(rotationSpeed);
    }

    private void StopThrusting()
    {
        thrustMainParticles.Stop();
        audioSource.Pause();
    }

    private void StartThrusting()
    {
        rigidbody.AddRelativeForce(Vector3.up * (mainThrust * Time.deltaTime));
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!thrustMainParticles.isPlaying)
        {
            thrustMainParticles.Play();
        }
    }

    private void ApplyRotation(float rotation)
    {
        rigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * (rotation * Time.deltaTime));
        rigidbody.freezeRotation = false;
    }
}
