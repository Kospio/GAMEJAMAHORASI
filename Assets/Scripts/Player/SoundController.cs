using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource SFXAudioSource;
    public AudioSource musicaKawaiAudioSource; 
    public AudioSource musicaMetalAudioSource; 

    public AudioClip pasos; 
    public AudioClip muerte; 
    public AudioClip agarrar; 
    public AudioClip cambioDimension; 
    public AudioClip teleport; 
    public AudioClip victoria;

    public AudioClip kawai; 
    public AudioClip metal; 

    public void Paso()
    {
        SFXAudioSource.clip = pasos; 
        SFXAudioSource.Play(); 
    }

    public void Muerte()
    {
        SFXAudioSource.clip = muerte;
        SFXAudioSource.Play();
    }

    public void Agarrar()
    {
        SFXAudioSource.clip = agarrar;
        SFXAudioSource.Play();
    }

    public void CambioDimension()
    {
        if (musicaKawaiAudioSource.volume != 1)
        {
            musicaKawaiAudioSource.volume = 1;
            musicaMetalAudioSource.volume = 0; 
        }

        else
        {
            musicaMetalAudioSource.volume = 1; 
            musicaKawaiAudioSource.volume = 0; 
        }

        SFXAudioSource.clip = cambioDimension;
        SFXAudioSource.Play();
    }

    public void Teleport()
    {
        SFXAudioSource.clip = teleport;
        SFXAudioSource.Play();
    }

    public void Victoria()
    {
        SFXAudioSource.clip = victoria;
        SFXAudioSource.Play();
    }
}
