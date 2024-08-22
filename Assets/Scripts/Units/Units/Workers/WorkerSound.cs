using UnityEngine;

public class WorkerSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] _minerSounds;
    [SerializeField] private AudioClip[] _loggingSounds;

    private AudioSource _audioSource;
    private Woodcutter _woodcutter;
    private Miner _miner;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        if (TryGetComponent(out Miner miner))
            _miner = miner;
        else if (TryGetComponent(out Woodcutter woodcutter))
            _woodcutter = woodcutter;
    }

    private void WorkingSound()
    {
        if (_miner != null)
            _audioSource.PlayOneShot(_minerSounds[Random.Range(0, _minerSounds.Length)]);
        else if (_woodcutter != null)
            _audioSource.PlayOneShot(_loggingSounds[Random.Range(0, _loggingSounds.Length)]);
    }
}