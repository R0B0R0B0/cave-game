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
    Demo
    1. The person against it
        -hp
        -attack
        -defense
        -more stats

    //Later
    2. What items and stats player has


    */

    public GameObject attackUIPrefab;
    public GameObject fightArea;
    AttackUI attackUI;
    public GameObject playArea;
    public BattleArea battleArea;
    public Transform canvas;

    public Enemy defaultEnemy;
    public GameObject pee;
    public Transform spawnPoint;

    public Transform playerSpawnPoint;

    BattleState state;

    //Actions
    Action attackButtonPressed;

    //Refenrences to instantiated gameobjects
    GameObject attackUIGameObject = null;


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
        //Setup the attackUI game object
        if (attackUIGameObject == null)
        {
            attackUIGameObject = Instantiate(attackUIPrefab,transform.GetChild(0));
        }
        attackUI = attackUIGameObject.GetComponent<AttackUI>();

        //Callback to button press in attakcUI
        attackUI.onAttackButtonPressed += OnAttackButton;

        attackUI.mainPanel.SetActive(false);


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

    #region Setup Functions

    void SetPlayArea(BoxCollider2D[] colliders, Vector2 size)
    {
        float thickness = 1;

        //Up
        colliders[0].size = new Vector2(size.x, thickness);
        colliders[0].offset = new Vector2(0, size.y / 2 + thickness / 2);
        //Dowm
        colliders[1].size = new Vector2(size.x, thickness);
        colliders[1].offset = new Vector2(0, -size.y / 2 - thickness / 2);
        //Right
        colliders[0].size = new Vector2(thickness, size.y);
        colliders[0].offset = new Vector2(size.x / 2 + thickness / 2, 0);
        //Left
        colliders[0].size = new Vector2(thickness, size.y);
        colliders[0].offset = new Vector2(0, -size.x / 2 - thickness / 2);

    }

    #endregion

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
        Vector2 size = Vector2.one;
        attackUI.mainPanel.SetActive(true);

        SetPlayArea(battleArea.colliders, size);

        GameManager.Instance.ChangeView(Cameras.Battle);
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
        attackUI.peitto.SetActive(true);
        playArea.SetActive(false);
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
        playArea.SetActive(true);

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
