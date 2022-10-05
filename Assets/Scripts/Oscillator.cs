using UnityEngine;

public class Oscillator : MonoBehaviour
{
    private Vector3 startingPosition;
    [SerializeField] private Vector3 movementVector;
    private float movementFactor;
    [SerializeField] private float period = 2F;
    
    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        if (period <= Mathf.Epsilon) return;
        float cycles = Time.time / period;
        
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(tau * cycles);

        movementFactor = (rawSinWave + 1) / 2F;
        
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
