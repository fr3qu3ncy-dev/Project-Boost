using UnityEngine;

public class Scaler : MonoBehaviour
{
    private Vector3 startingScale;
    [SerializeField] private Vector3 scaleVector;
    private float scaleFactor;
    [SerializeField] private float period = 2F;
    [SerializeField] private float delay;
    
    void Start()
    {
        startingScale = transform.localScale;
    }

    void Update()
    {
        if (period <= Mathf.Epsilon) return;
        float cycles = (Time.time + delay) / period;
        
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(tau * cycles);

        scaleFactor = (rawSinWave + 1) / 2F;
        
        Vector3 offset = scaleVector * scaleFactor;
        transform.localScale = startingScale + offset;
    }
}
