using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroneSounds : MonoBehaviour
{
    [SerializeField] private AudioClip _buildSound;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void BuildSound()
    {
        _audioSource.PlayOneShot(_buildSound);
    }
}