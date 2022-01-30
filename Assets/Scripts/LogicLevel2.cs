using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicLevel2 : MonoBehaviour
{
    public GameObject checkpoint3;

    public GameObject objetoQueGira1;
    public GameObject objetoQueGira2;

    private int checkpoint2 = 0;

    [Space(10)]
    [Header("UI")]
    public RawImage silueta;
    public void CheckCheckpoint2()
    {
        checkpoint2++;
        if (checkpoint2 == 2)
        {
            checkpoint3.SetActive(true);
        }
    }

    public void CosasQueGiran()
    {
        Vector3 Rot = objetoQueGira1.transform.eulerAngles;
        Rot.y += 90;
        objetoQueGira1.transform.eulerAngles = Rot;

        Vector3 Rot2 = objetoQueGira2.transform.eulerAngles;
        Rot2.y += 90;
        objetoQueGira2.transform.eulerAngles = Rot2;
    }

    public void Checkpoint3Catched()
    {
        Debug.Log("GANAS");
        StartCoroutine(ChangeScene()); 
    }

    public IEnumerator ChangeScene()
    {
        silueta.gameObject.GetComponent<Animator>().SetTrigger("NextScene");
        yield return new WaitForSeconds(1.7f);
        SceneManager.LoadScene("Level2");
    }
}
