using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kivi : MonoBehaviour
{
    void OnRockDropped()
    {
        GameManager.Instance.ChangeView(Cameras.Base);
    }
}
