using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
    public GameObject pee;
    public Transform spawnPoint;

    BattleState state;

    Action attackButtonPressed;

    //In the battle
    public Transform playerTransform;
    public Rigidbody2D playerRigidbody;

    public float speed = 1;

    //player stuff
    float playerHealth = 10;
    float playerDamage = 1;

    //Enemy stuff
    float enemyHealth = 10;
    float enemyDamage = 1;

    void Start()
    {
        attackUI = attackUIPrefab.GetComponent<AttackUI>();
        attackUI.mainPanel.SetActive(false);

        //Callback to button press in attakcUi
        attackUI.onAttackButtonPressed += OnAttackButton;
    }

    private void FixedUpdate()
    {
        //Player movement
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.up * y; move = move.normalized;  move *= Time.deltaTime;

        playerRigidbody.MovePosition(playerTransform.transform.position + move * speed);

        if(playerRigidbody.velocity.sqrMagnitude < 0.1 ) { playerRigidbody.velocity = Vector2.zero; }
    }

    public void OnButtonPressed()
    {
        StartFight(defaultEnemy);
    }

    public void StartFight(Enemy enemy)
    {
        state = BattleState.START;
        SetupBattle();

        state = BattleState.PLAYERTURN;
    }

    public void EndFight()
    {
        attackUI.mainPanel.SetActive(false);
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
        enemyHealth -= playerDamage;

        if(enemyHealth <= 0)
        {
            state = BattleState.WON;
        }

            state = BattleState.ENEMYTURN;
            EnemyTurn();
        
    }

    void EnemyTurn()
    {
        EnemyAttack();
    }
    void EnemyAttack()
    {
        playerTransform.position = Vector3.zero;

        StartCoroutine("AttackCourontine");
            
        state = BattleState.PLAYERTURN;
    }

    public void PelletHit()
    {
        playerHealth -= enemyDamage;

        if (playerHealth <= 0)
        {
            state = BattleState.LOST;
        }
    }

    public void OnAttackButton()
    {
        Debug.Log(state);
        if(state != BattleState.PLAYERTURN)
        {
            return;
        }
        
        PlayerAttack();
    }

    IEnumerator AttackCourontine()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(pee, spawnPoint.position, spawnPoint.rotation);
            spawnPoint.position = new Vector2 (playerTransform.position.x, spawnPoint.position.y);
            yield return new WaitForSeconds(.2f);
        }
        yield return null;
    }
}
