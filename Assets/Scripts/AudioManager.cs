using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get ; private set; }

    [Header("----------- Audio Source ---------")]
    [SerializeField]
    AudioSource musicSource;
    [SerializeField]
    AudioSource SFXSource;

    [Header("----------- Audio Clip ---------")]
    public AudioClip m_backgroundMusic;
    public AudioClip m_deathSound;
    public AudioClip m_takeDamageSound;
    public AudioClip m_shootArrowSound;
    public AudioClip m_stabAttackSound;
    public AudioClip m_AoeAttackSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //Speelt de audio background af.
        musicSource.clip = m_backgroundMusic;
        musicSource.Play();
    }

    //Op het moment dat de code aangeroepen word speelt die de aangeroepen audio af.
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
