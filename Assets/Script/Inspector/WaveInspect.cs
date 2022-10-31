using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
[ExecuteInEditMode]
/// <summary>
/// Waves info inspector.
/// </summary>
public class WaveInspect : MonoBehaviour
{
    [HideInInspector]
    // Timeouts between waves
    public List<float> timeouts
    {
        get
        {
            return wavesInfo.wavesTimeout;
        }
        set
        {
            wavesInfo.wavesTimeout = value;
        }
    }

    // Waves info component
    private WaveInfo wavesInfo;

    /// <summary>
    /// Raises the enable event.
    /// </summary>
    void OnEnable()
    {
        wavesInfo = GetComponent<WaveInfo>();
        Debug.Assert(wavesInfo, "Wrong stuff settings");
    }

    /// <summary>
    /// Update this instance.
    /// </summary>
    public void Update()
    {
        wavesInfo.Update();
    }
}
#endif
