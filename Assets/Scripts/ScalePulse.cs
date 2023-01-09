using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePulse : MonoBehaviour
{
    [SerializeField] private float minimum = 1f;
    [SerializeField] private float maximum = 1.5f;
    [SerializeField] private float cyclesPerSecond = 2.0f;
    [SerializeField] private bool pulse = false;
    [SerializeField] private bool oneShot = false;
    [SerializeField] private bool startOnMax = true;
    private int turns = 0;
    private float scale;
    private bool increasing = true;

    void Start()
    {
        if (startOnMax)
        {
            scale = maximum;
        }
        else
        {
            scale = minimum;
        }
    }

    void Update()
    {
        if (pulse)
        {
            float t = Time.deltaTime;

            increasing = scale >= maximum ? false : increasing;
            increasing = scale <= minimum ? true : increasing;
            turns = (scale >= maximum && startOnMax) || (scale <= minimum && !startOnMax) ? turns + 1 : turns ;
            if (oneShot && turns > 0)
            {
                StopPulse();
            }
            scale = increasing ? scale += t * cyclesPerSecond : scale -= t * cyclesPerSecond;
            SetScale(scale);
        }
    }

    public void StartPulse()
    {
        pulse = true;
        turns = 0;
    }

    public void StopPulse()
    {
        if (startOnMax)
            SetScale(maximum);
        else
            SetScale(minimum);
        pulse = false;
    }

    private void SetScale(float a)
    {
        transform.localScale = new Vector3(a, a, 1);
    }
}
