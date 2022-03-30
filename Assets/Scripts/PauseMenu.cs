using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;

    public AudioMixer audioMixer;

    [Header("UI")]
    public Canvas pauseCanvas;
    public Slider volumeSlider;
    public Button restartButton;


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
            restartButton.enabled = false;
        }
        else //Quitar pausa
        {
            Time.timeScale = 1f;
            pauseCanvas.enabled = false;
            restartButton.enabled = true;
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


}
