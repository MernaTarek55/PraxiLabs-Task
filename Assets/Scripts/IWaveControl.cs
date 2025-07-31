public interface IWaveControl
{
    void StopWaves();
    void ResumeWaves();
    void ForceNextWave();
    void DestroyCurrentWave();
    bool IsRunning { get; }
}
