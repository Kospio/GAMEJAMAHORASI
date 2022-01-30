using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;

    public AudioMixer audioMixer;

    [Header("UI")]
    public Canvas pauseCanvas;
    public Slider volumeSlider;


    public void SetVolume()
    {
        audioMixer.SetFloat("MusicVolume", volumeSlider.value);
    }

    private void Start()
    {
        pauseCanvas.enabled = false;
    }

    public void OpenClose()
    {
        GamePaused = GamePaused ? GamePaused = false : GamePaused = true;
        TryShowMenu();
    }
    private void TryShowMenu()
    {
        if (GamePaused) //Poner pausa
        {
            Time.timeScale = 0f;
            pauseCanvas.enabled = true;
        }
        else //Quitar pausa
        {
            Time.timeScale = 1f;
            pauseCanvas.enabled = false;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }


}
