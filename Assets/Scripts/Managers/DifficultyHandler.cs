using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DifficultyHandler : MonoBehaviour
{
    [Header("UI")]
    public UnityEvent EasyDifficulty;
    public UnityEvent NormalDifficulty;
    public UnityEvent InsaneDifficulty;

    void Start()
    {
        switch (GameManager.Instance.currentDifficulty)
        {
            case GameManager.DifficultLevely.Easy:
                EasyDifficulty.Invoke();
                break;

            case GameManager.DifficultLevely.Normal:
                NormalDifficulty.Invoke();
                break;

            case GameManager.DifficultLevely.Insane:
                InsaneDifficulty.Invoke();
                break;

            default:
                Debug.LogError("Invalid Difficulty Level");
                break;
        }
    }

}
