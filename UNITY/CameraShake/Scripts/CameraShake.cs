using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoSingleton<CameraShake>
{
    public CinemachineVirtualCamera vcam;
    private static CinemachineBasicMultiChannelPerlin noise;
    private static List<float> shakesAmp;
    [SerializeField] private float nerfShake = 1;
    public override void Init()
    {
        //noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        shakesAmp = new List<float>(20);
    }

    /// <summary>
    /// Make a shaking effect on the camera
    /// </summary>
    /// <param name="amp"></param>
    /// <param name="duration"></param>
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
    /// Get maximum of values
    /// </summary>
    /// <param name="values">Values to get maximum</param>
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
