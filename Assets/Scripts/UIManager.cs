using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Text UI")]
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private TMP_Text enemyCountText;
    [SerializeField] private TMP_Text fpsText;

    [Header("Buttons")]
    [SerializeField] private Button stopResumeButton;
    [SerializeField] private Button nextWaveButton;
    [SerializeField] private Button destroyWaveButton;

    private IWaveControl waveControl;
    private bool isStopped = false;

    private void Start()
    {
        waveControl = FindObjectOfType<WaveManager>();

        stopResumeButton.onClick.AddListener(OnStopResumeClicked);
        nextWaveButton.onClick.AddListener(() => waveControl.ForceNextWave());
        destroyWaveButton.onClick.AddListener(() => waveControl.DestroyCurrentWave());
    }

    private void Update()
    {
        if (waveControl is WaveManager manager)
        {
            waveText.text = $"Wave: {manager.CurrentWave}";
            enemyCountText.text = $"Enemies Alive: {manager.EnemiesAlive}";
        }

        UpdateFPS();
    }

    private void OnStopResumeClicked()
    {
        if (waveControl.IsRunning)
        {
            waveControl.StopWaves();
            stopResumeButton.GetComponentInChildren<TMP_Text>().text = "Resume Waves";
        }
        else
        {
            waveControl.ResumeWaves();
            stopResumeButton.GetComponentInChildren<TMP_Text>().text = "Stop Waves";
        }
    }

    private void UpdateFPS()
    {
        float fps = 1f / Time.deltaTime;
        fpsText.text = $"FPS: {Mathf.RoundToInt(fps)}";
    }
}
