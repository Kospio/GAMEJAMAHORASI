using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueDisplayer : MonoBehaviour
{
    [Header("--UI--")]
    public Image playerImage;
    public TextMeshProUGUI dialogueText;
    private int currentDialogue = 0;

    [Header("--DIALOGUE OBJECTS--")]
    public List<DialogueNode> dialogues;

    private void Start()
    {
        playerImage.sprite = dialogues[0].characterSprite;
        dialogueText.text = dialogues[0].dialogueText;
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
            //cambio de escena al juego
            SceneManager.LoadScene("Level1");
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
}
