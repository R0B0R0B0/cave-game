using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public abstract class DialogueBaseNode : Node {

    public virtual string GetString() { return null; }

    public virtual Sprite GetSprite() { return null; }  
}