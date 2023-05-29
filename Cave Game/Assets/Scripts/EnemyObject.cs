using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : MonoBehaviour
{
    public void AnimationStarted()
    {
        GameManager.Instance.FightManager.AnimationEnded(false);
    }
    public void AnimationEnded()
    {
        GameManager.Instance.FightManager.AnimationEnded(true);
    }
}
