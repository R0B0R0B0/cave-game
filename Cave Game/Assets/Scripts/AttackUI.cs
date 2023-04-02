using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackUI : MonoBehaviour
{
    //Panels
    public GameObject mainPanel;

    //Buttons
    public Button attackButton;

    //Cosmetics
    public Text enemyName;
    public Image enemyImage;


    public Action onAttackButtonPressed;

    public void OnAttackButtonPressed()
    {
        onAttackButtonPressed.Invoke();
    }
}
