using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Node", menuName = "ScriptableData/DialogueNode")]
public class DialogueNode : ScriptableObject
{
    [Space]
    public Sprite characterSprite;
    [Space]
    [TextArea(10,10)] public string dialogueText;
}
