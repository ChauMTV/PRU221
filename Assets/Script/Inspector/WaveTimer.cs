using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class WaveTimer : MonoBehaviour
{
    // Visualisation of remaining TO
    public Image timeBar;
    // Current wave text field
    public Text currentWaveText;
    // Max wave text field
    public Text maxWaveNumberText;
    // Effect of highlighted timer
    public GameObject highlightedFX;
    // Duration for highlighted effect
    public float highlightedTO = 0.2f;

    private WaveInfo waveInfo;

    private List<float> waves = new List<float>();
    private int currentWave;
    private float currentTimeout;
    private float counter;
    private bool finished;
    // Start is called before the first frame update


    void Awake()
    {
        waveInfo = FindObjectOfType<WaveInfo>();
        Debug.Assert(timeBar && highlightedFX && waveInfo && timeBar && currentWaveText && maxWaveNumberText, "Wrong initial settings");
    }
    void Start()
    {
        highlightedFX.SetActive(false);
        waves = waveInfo.wavesTimeout;
        currentWave = 0;
        counter = 0;
        finished = false;
        GetCurrentWaveCount();
        maxWaveNumberText.text = waves.Count.ToString();
        currentWaveText.text = "0";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (finished == false)
        {
            // Timeout expired
            if (counter <= 0f)
            {
                // Send event about next wave start
                EventManager.TriggerEvent("WaveStart", null, currentWave.ToString());
                currentWave++;
                currentWaveText.text = currentWave.ToString();
                // Highlight the timer for short time
                StartCoroutine("HighlightTimer");
                // All waves are sended
                if (GetCurrentWaveCount() == false)
                {
                    finished = true;
                    // Send event about timer stop
                    EventManager.TriggerEvent("TimerEnd", null, null);
                    return;
                }
            }
            counter -= Time.fixedDeltaTime;
            if (currentTimeout > 0f)
            {
                timeBar.fillAmount = counter / currentTimeout;
            }
            else
            {
                timeBar.fillAmount = 0f;
            }
        }
    }

    /// <summary>
    /// Highlights the timer coroutine.
    /// </summary>
    /// <returns>The timer.</returns>
    private IEnumerator HighlightTimer()
    {
        highlightedFX.SetActive(true);
        yield return new WaitForSeconds(highlightedTO);
        highlightedFX.SetActive(false);
    }

    private bool GetCurrentWaveCount()
    {
        bool res = false;
        if (waves.Count > currentWave)
        {
            counter = currentTimeout = waves[currentWave];
            res = true;
        }
        return res;
    }
}
