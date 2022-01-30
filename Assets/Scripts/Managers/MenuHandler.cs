using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public Dropdown dropDown;

    public void changeDificulty()
    {
        switch (dropDown.value)
        {
            case 0:
                GameManager.Instance.currentDifficulty = GameManager.DifficultLevely.Easy;
                break;

            case 1:
                GameManager.Instance.currentDifficulty = GameManager.DifficultLevely.Normal;
                break;

            case 2:
                GameManager.Instance.currentDifficulty = GameManager.DifficultLevely.Insane;
                break;

            default:
                Debug.LogError("Nivel de dificultad no asignado");
                break;
        }

    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
