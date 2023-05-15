using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
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

    public Transform playerSpawnPoint;

    BattleState state;

    Action attackButtonPressed;

    //In the battle
    public Transform playerTransform;
    public Rigidbody2D playerRigidbody;

    public float speed = 1;

    //player stuff
    float playerHealth = 10;
    float playerMaxHealth = 10;
    float playerDamage = 1;

    //Enemy stuff
    float enemyHealth = 10;
    float enemyMaxHealth = 10;
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
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.up * y; move = move.normalized; 

        playerRigidbody.velocity = move * speed * Time.fixedDeltaTime;

       // if(playerRigidbody.velocity.sqrMagnitude < 0.1 ) { playerRigidbody.velocity = Vector2.zero; }
    }

    #region Fight Functions
    //Start/End
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
    //setup
    void SetupBattle()
    {
        attackUI.mainPanel.SetActive(true);
    }
    //Player
    void PlayerTurn()
    {       
        //Check
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        //UI
        Debug.Log("2");
        attackUI.peitto.SetActive(true);
    }
    void PlayerAttack()
    {
        //Check
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        //UI

        //Logic
        enemyHealth -= playerDamage;
        OnEnemyHealthChanged();

        if (enemyHealth <= 0)
        {
            state = BattleState.WON;
            EndFight();
        }
        
        state = BattleState.ENEMYTURN;
        EnemyTurn();
    } 
    //Enemy
    void EnemyTurn()
    {
        //Check
        if (state != BattleState.ENEMYTURN)
        {
            return;
        }
        //UI 


        //Action
        StartCoroutine(EnemyAttack());
    }
    IEnumerator EnemyAttack()
    {
        //Check
        if (state != BattleState.ENEMYTURN)
        {
            yield return null;
        }

        //UI
        attackUI.peitto.SetActive(false);

        //Action
        playerTransform.position = playerSpawnPoint.position;
        for (int i = 0; i < 10; i++)
        {
            spawnPoint.position = new Vector2(playerTransform.position.x, spawnPoint.position.y);
            Instantiate(pee, spawnPoint.position, spawnPoint.rotation);

            float x = 0;

            float a = 1;
            float b = 2;

            float k = 0;
            float h = 0; 

            float y = a * Mathf.Sin((x - h) / b) + k;
    

            yield return new WaitForSeconds(.5f);
        }

        yield return new WaitForSeconds(1);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
        yield return null;

    }
    #endregion

    //Hit callbacks
    public void PelletHit()
    {
        playerHealth -= enemyDamage;
        OnPlayerHealthChanged();

        if (playerHealth <= 0)
        {
            state = BattleState.LOST;
            EndFight();
        }
    }

    #region Callbacks

    //Button callbacks
    public void OnAttackButton()
    {
        Debug.Log(state);
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        PlayerAttack();
    }

    public void OnButtonPressed()
    {
        StartFight(defaultEnemy);
    }
    #endregion

    #region UI Functions
    //UI changes

    public void OnPlayerHealthChanged()
    {
        attackUI.playerHealthbar.SetValueWithoutNotify(playerHealth/playerMaxHealth);
    }
    public void OnEnemyHealthChanged()
    {
        attackUI.enemyHealthbar.SetValueWithoutNotify(enemyHealth / enemyMaxHealth);
    }
    #endregion
}
