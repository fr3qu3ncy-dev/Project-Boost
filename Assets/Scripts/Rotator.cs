using UnityEngine;

public class Rotator : MonoBehaviour
{
    private Vector3 startingRotation;
    [SerializeField] private Vector3 rotationVector;
    private float rotationFactor;
    [SerializeField] private float period = 2F;
    [SerializeField] private float delay;
    
    void Start()
    {
        startingRotation = transform.rotation.eulerAngles;
    }

    void Update()
    {
        if (period <= Mathf.Epsilon) return;
        float cycles = (Time.time + delay) / period;
        
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(tau * cycles);

        rotationFactor = (rawSinWave + 1) / 2F;
        
        Vector3 offset = rotationVector * rotationFactor;
        transform.rotation = Quaternion.Euler(startingRotation + offset);
    }
}
