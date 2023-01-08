using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePulse : MonoBehaviour
{
    [SerializeField] private float minimum = 1f;
    [SerializeField] private float maximum = 1.5f;
    [SerializeField] private float cyclesPerSecond = 2.0f;
    [SerializeField] private bool pulse = false;
    private float scale;
    private bool increasing = true;

    void Start()
    {
        scale = maximum;
    }

    void Update()
    {
        if (pulse)
        {
            float t = Time.deltaTime;

            if (scale >= maximum)
            {
                increasing = false;
            }
            if (scale <= minimum) increasing = true;
            scale = increasing ? scale += t * cyclesPerSecond : scale -= t * cyclesPerSecond;
            SetScale(scale);

        }
    }

    public void StartPulse()
    {
        pulse = true;
    }

    public void StopPulse()
    {
        SetScale(maximum);
        pulse = false;
    }

    private void SetScale(float a)
    {
        transform.localScale = new Vector3(a, a, 1);
    }
}
