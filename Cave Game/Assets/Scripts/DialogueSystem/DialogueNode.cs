using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class DialogueNode : DialogueBaseNode {

    [Input] public int entry;
    [Output] public int exit;

    public string speakerName = "Name";
    public string dialogueLine = "Text";
    public Sprite sprite = null;

    public override string GetString()
    {
        return "DialogueNode/" + speakerName + "/" + dialogueLine;
    }
    public override Sprite GetSprite()
    {
        return sprite;
    }
}