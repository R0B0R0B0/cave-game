using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackUI : MonoBehaviour
{
    [Header("Panels")]
    //Panels
    public GameObject mainPanel;
    public GameObject topPanel;
    public GameObject middlePanel;
    public GameObject bottomPanel;
    public GameObject peitto;

    [Header("Buttons")]
    //Buttons
    public Button attackButton;

    [Header("Cosmetics")]
    //Cosmetics
    public Text enemyName;
    public Image enemyImage;

    [Header("Information")]
    //Information
    public Slider enemyHealthbar;
    public Slider playerHealthbar;

    [Header("PLayer")]
    //Player
    public Transform playerTranform;

    public Action onAttackButtonPressed;

    public void OnAttackButtonPressed()
    {
        onAttackButtonPressed.Invoke();
    }
}
