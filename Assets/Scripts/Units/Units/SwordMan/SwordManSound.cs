using UnityEngine;

public class SwordManSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] _attackSounds;
    [SerializeField] private AudioClip[] _dieSounds;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void PlayAttackSound()
    {
        _audioSource.PlayOneShot(_attackSounds[Random.Range(0, _attackSounds.Length)]);
    }

    private void PlayDieSound()
    {
        _audioSource.PlayOneShot(_dieSounds[Random.Range(0, _dieSounds.Length)]);
    }
}