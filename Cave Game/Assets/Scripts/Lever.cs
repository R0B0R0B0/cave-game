using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Usable
{
    public Animator animator;
    public SpriteRenderer renderer;
    public Sprite used;
    public Collider2D colldier;

    bool hasBeenUsed;
    public override void Use()
    {
        if (hasBeenUsed) { return; }
        base.Use();

        GameManager.Instance.ChangeView(Cameras.Rock);
        animator.SetTrigger("Activate");
        renderer.sprite = used;
        colldier.gameObject.SetActive(false);
        hasBeenUsed = true;
    }
}
