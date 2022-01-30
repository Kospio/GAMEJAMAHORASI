using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public Dropdown dropDown;
    public RawImage silueta;

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

    public void StartButtonAtajo()
    {
        StartCoroutine(StartButton()); 
    }

    public IEnumerator StartButton()
    {
        silueta.gameObject.GetComponent<Animator>().SetTrigger("NextScene");
        yield return new WaitForSeconds(1.7f);
        SceneManager.LoadScene("Tutorial");
    }

}
