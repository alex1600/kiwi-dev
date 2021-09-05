using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    private static CinemachineBasicMultiChannelPerlin noise;
    private static List<float> shakesAmp;
    [SerializeField] private float nerfShake = 1;
    
    private void Awake()
    {
        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        shakesAmp = new List<float>(20);
    }

    /// <summary>
    /// Make a shaking effect on the camera
    /// </summary>
    /// <param name="amp">Shake amplitude</param>
    /// <param name="duration">Shake duration</param>
    public static void Shake(float amp, float duration)
    {
        float realAmp = amp * Instance.nerfShake;
        Instance.StartCoroutine(Shaker(realAmp, duration));
    }

    private static IEnumerator Shaker(float amp, float duration)
    {
        shakesAmp.Add(amp);

        noise.m_AmplitudeGain = Max(shakesAmp.ToArray());

        yield return new WaitForSeconds(duration);

        shakesAmp.Remove(amp);

        if (shakesAmp.Count == 0)
        {
            noise.m_AmplitudeGain = 0;
        }
        else
        {
            noise.m_AmplitudeGain = Max(shakesAmp.ToArray());
        }
    }

    /// <summary>
    /// Get the maximum value of an array
    /// </summary>
    /// <param name="values">Array of values</param>
    /// <returns>Maximum value</returns>
    private static float Max(params float[] values)
    {
        float max = float.MinValue;
        for (int i = 0; i < values.Length; i++)
        {
            if (values[i] > max)
                max = values[i];
        }
        return max;
    }

}
