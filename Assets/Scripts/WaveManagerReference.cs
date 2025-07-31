// WaveManagerReference.cs
using UnityEngine;

public class WaveManagerReference : MonoBehaviour
{
    public static WaveManager Instance;

    private void Awake()
    {
        Instance = GetComponent<WaveManager>();
    }
}
