using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState
{
    START, PLAYERTURN, ENEMYTURN, WON, LOST
}

public class FightManager : MonoBehaviour
{
    /*What fight should have
    1. The person against it
        -hp
        -attack
        -defense
        -more stats
    2. What items and stats player has


    */

    public GameObject attackUIPrefab;
    public AttackUI attackUI;
    public Transform canvas;

    public Enemy defaultEnemy;

    BattleState state;

    Action attackButtonPressed;

    void Start()
    {
        attackUI = attackUIPrefab.GetComponent<AttackUI>();
        attackUI.mainPanel.SetActive(false);

        //Callback to button press in attakcUi
        attackUI.onAttackButtonPressed += OnAttackButton;
    }

    public void OnButtonPressed()
    {
        StartFight(defaultEnemy);
    }

    public void StartFight(Enemy enemy)
    {
        state = BattleState.START;
        SetupBattle();

        state= BattleState.PLAYERTURN;
    }

    public void EndFight()
    {
        attackUIPrefab.SetActive(false);
    }

    void SetupBattle()
    {
        attackUI.mainPanel.SetActive(true);
    }

    void PlayerTurn()
    {

    }

    void PlayerAttack()
    {

    }

    void OnAttackButton()
    {
        if(state != BattleState.PLAYERTURN)
        {
            return;
        }
        
        PlayerAttack();
    }
}
