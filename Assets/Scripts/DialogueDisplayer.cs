using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class DialogueDisplayer : MonoBehaviour
{
    [Header("--UI--")]
    public Image playerImage;
    public TextMeshProUGUI dialogueText;
    private int currentDialogue = 0;
    public RawImage silueta;

    [Header("--DIALOGUE OBJECTS--")]
    public List<DialogueNode> dialogues;

    [Header("--EVENTS--")]
    public UnityEvent onFirstNode;
    public UnityEvent onLastNode;

    private void Start()
    {
        playerImage.sprite = dialogues[0].characterSprite;
        dialogueText.text = dialogues[0].dialogueText;
        onFirstNode.Invoke();
    }

    public void NextDialogue()
    {
        if(currentDialogue < dialogues.Count - 1)
        {
            currentDialogue++;
            DisplayDialogue(currentDialogue);
        }
        else if(currentDialogue == dialogues.Count - 1) //ultimo dialogo
        {
            onLastNode.Invoke();
            StartCoroutine(NextScene()); 
        }
    }
    public void PreviousDialogue()
    {
        if (currentDialogue > 0)
        {
            currentDialogue--;
            DisplayDialogue(currentDialogue);
        }
    }

    private void DisplayDialogue(int dialogueNumber)
    {
        playerImage.sprite = dialogues[dialogueNumber].characterSprite;
        dialogueText.text = dialogues[dialogueNumber].dialogueText;
    }

    public IEnumerator NextScene()
    {
        silueta.gameObject.GetComponent<Animator>().SetTrigger("NextScene");
        yield return new WaitForSeconds(1.7f);
        SceneManager.LoadScene("Level1");
    }
}
