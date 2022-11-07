    using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
public class WaveInfo : MonoBehaviour
{
    public List<float> wavesTimeout = new List<float>();
    // Start is called before the first frame update

#if UNITY_EDITOR
    [HideInInspector]
    private float defaultWaveTimeout = 30f;
    [HideInInspector]
    private PointSpawner[] spawners;


    // Update is called once per frame
    public void Update()
    {
        spawners = FindObjectsOfType<PointSpawner>();
        int waveCount = 0;
        foreach (PointSpawner spawn in spawners)
        {
            if (spawn.waves.Count > waveCount)
            {
                waveCount = spawn.waves.Count;
            }
        }
        if (wavesTimeout.Count < waveCount)
        {
            for (int i = wavesTimeout.Count; i < waveCount; i++)
            {
                wavesTimeout.Add(defaultWaveTimeout);
            }
        }
        else if (wavesTimeout.Count > waveCount)
        {
            wavesTimeout.RemoveRange(waveCount, wavesTimeout.Count - waveCount);
        }
    }
#endif
}