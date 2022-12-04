using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class DialogueStartNode : DialogueBaseNode {

    [Output] public int exit;

    public override string GetString()
    {
        return "Start";
    }

}