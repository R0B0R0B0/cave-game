using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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
    AttackUI attackUI;
    public GameObject playArea;
    public GameObject battleAreaObject;
    public BattleArea battleArea;
    public Transform canvas;
    public GameObject borderGfx;


    //  public GameObject pee;
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
    int turnNumber;
    Animator enemyAnimator;
    GameObject enemyFightObject;

    public float speed = 1;

    //player stuff
    float playerHealth = 10;
    float playerMaxHealth = 10;
    float playerDamage = 1;

    //Enemy stuff
    Enemy enemy;

    float enemyHealth = 10;
    float enemyMaxHealth = 10;
    float enemyDamage = 1;
    float enemyDefence = 0;


    //Other
    bool hasAnimationEnded;

    //PLayer movement in fight
    //I recommend 7 for the move speed, and 1.2 for the force damping - GurbluciDevlogs
    public float moveSpeed;
    Vector2 forceToApply;
    Vector2 playerInput;
    public float forceDamping;
    bool canMove;

    void Start()
    {
        //Setup the attackUI game object
        if (attackUIGameObject == null)
        {
            if (GameObject.FindWithTag("FightUI"))
            {
                Debug.Log("Found fight ui");
                attackUIGameObject = GameObject.FindGameObjectWithTag("FightUI");
            }
            else
            {
                Debug.Log("Creating fight ui");
                attackUIGameObject = Instantiate(attackUIPrefab, canvas);
            }
        }
        attackUI = attackUIGameObject.GetComponent<AttackUI>();

        //Callback to button press in attakcUI
        attackUI.onAttackButtonPressed += OnAttackButton;

        attackUI.mainPanel.SetActive(false);


    }

    private void Update()
    {
        playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    void FixedUpdate()
    {
        Vector2 moveForce = playerInput * moveSpeed;
        moveForce += forceToApply;
        forceToApply /= forceDamping;
        if (Mathf.Abs(forceToApply.x) <= 0.01f && Mathf.Abs(forceToApply.y) <= 0.01f)
        {
            forceToApply = Vector2.zero;
        }

        if (canMove)
        {
            playerRigidbody.velocity = moveForce;
        }
    }

    #region Setup Functions

    //setup
    void SetupBattle()
    {
        //for testing purposes
        Vector2 size = enemy.playAreaSize;
        attackUI.mainPanel.SetActive(true);

        //Edits the colliders
        SetPlayArea(battleArea.colliders, size, borderGfx);

        //Changes from the current camera to fight camera
        GameManager.Instance.ChangeView(Cameras.Battle);

        //Disables player
        GameManager.Instance.player.SetActive(false);

        //Setup variables
        enemyMaxHealth = enemy.health;
        enemyHealth = enemyMaxHealth;
        enemyDamage = enemy.damage;

    }

    void SetPlayArea(BoxCollider2D[] colliders, Vector2 size, GameObject gfx)
    {
        float thickness = .1f;

        //Up
        colliders[0].size = new Vector2(size.x, thickness);
        colliders[0].offset = new Vector2(0, size.y / 2 + thickness / 2);
        //Dowm
        colliders[1].size = new Vector2(size.x, thickness);
        colliders[1].offset = new Vector2(0, -size.y / 2 - thickness / 2);
        //Right
        colliders[2].size = new Vector2(thickness, size.y);
        colliders[2].offset = new Vector2(size.x / 2 + thickness / 2, 0);
        //Left
        colliders[3].size = new Vector2(thickness, size.y);
        colliders[3].offset = new Vector2(-size.x / 2 - thickness / 2, 0);

        gfx.transform.localScale = size;
    }

    #endregion

    #region Fight Functions
    //Start/End
    public void StartFight(Enemy enemy)
    {
        this.enemy = enemy;

        enemyFightObject = Instantiate(enemy.enemyFight, spawnPoint.position, Quaternion.identity, playArea.transform);

        enemyAnimator = enemyFightObject.GetComponent<Animator>();

        state = BattleState.START;

        SetupBattle();

        state = BattleState.PLAYERTURN;
    }
    public void EndFight()
    {
        attackUI.mainPanel.SetActive(false);

        //Activates player
        GameManager.Instance.player.SetActive(true);

        //Changes from the current camera to main camera
        GameManager.Instance.ChangeView(Cameras.Base);
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
        battleAreaObject.SetActive(false);
        attackUI.peitto.SetActive(true);

        canMove = false;
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
        enemyHealth -= playerDamage;// * (enemyDefence * 0.2f); Debug.Log("Damage " + playerDamage * (enemyDefence * 0.2f));
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
        battleAreaObject.SetActive(true);
        attackUI.peitto.SetActive(false);

        //Action
        StartEnemyAttack();
    }

    void StartEnemyAttack()
    {
        if(state != BattleState.ENEMYTURN) { return; }

        canMove = true;

        switch (enemy.attackOrder[turnNumber])
        {
            case 0:
                enemyAnimator.SetInteger("BattleIndex", 0);
                break;
            case 1:
                Debug.Log("1 fweuigfiwudwjfif");
                enemyAnimator.SetInteger("BattleIndex", 1);
                break;
            case 2:
                enemyAnimator.SetInteger("BattleIndex", 2);
                break;
            case 3:
                enemyAnimator.SetInteger("BattleIndex", 3);
                break;
            default:
                enemyAnimator.SetInteger("BattleIndex", 0);
                break;
        }

        enemyAnimator.SetBool("StartAttack", true);


    }

    void EndEnemyAttack()
    {
        turnNumber++;
        if (turnNumber >= enemy.attackOrder.Length)
        {
            turnNumber = 0;
        }
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    /*
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
    */
    #endregion

    //Callbacks
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

    public void AnimationEnded(bool hasAnimationEnded)
    {
        enemyAnimator.SetBool("StartAttack", false);
        EndEnemyAttack();
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
    #endregion

    #region UI Functions
    //UI changes

    public void OnPlayerHealthChanged()
    {
        attackUI.playerHealthbar.SetValueWithoutNotify(playerHealth / playerMaxHealth);
    }
    public void OnEnemyHealthChanged()
    {
        attackUI.enemyHealthbar.SetValueWithoutNotify(enemyHealth / enemyMaxHealth);
    }
    #endregion
}
