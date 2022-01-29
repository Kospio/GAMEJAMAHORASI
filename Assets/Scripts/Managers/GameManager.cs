using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum DifficultLevely { Easy, Normal, Insane }
    public DifficultLevely currentDifficulty;


    private void Awake()
    {
        #region SINGLETON
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
        #endregion
    }

    public void changeDificulty(DifficultLevely newDifficulty)
    {
        currentDifficulty = newDifficulty;
    }

    public void ChangeScene(string levelName)
    {
        SceneManager.LoadScene(levelName); 
    }
}
