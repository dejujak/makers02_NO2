using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 99999999f;

    [Header("# Player Info")]
    public int health;
    public int maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;

    private float gameTimeForStage = 0;
    private int stage = 0;
    private bool pauseProgressBeforeEnemyAllDie = false;
    public int GetStage() { return stage; }
    public void SetPauseProgressBeforeEnemyAllDie()
    {
        pauseProgressBeforeEnemyAllDie = true;
    }

    public bool GetPauseProgressBeforeEnemyAllDie()
    {
        return pauseProgressBeforeEnemyAllDie;
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if ( stage <= 8 )
            gameTime += Time.deltaTime;

        /*
        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }

        */
   

        if (GetPauseProgressBeforeEnemyAllDie() == false )
        {
            gameTimeForStage += Time.deltaTime;
            if (gameTimeForStage > 10.0f)
            {
                stage += 1;
                gameTimeForStage = 0.0f;
            }
        }
        else
        {
            if ( pool.CheckAllDie(0) == true )
            {
                Debug.Log("pauseProgressBeforeEnemyAllDie = false");
                pauseProgressBeforeEnemyAllDie = false;
                stage += 1;
            }
            //if ( pool.poo)
        }
        
    }

    public void GetExp()
    {
        exp++;

        if (exp == nextExp[level])
        {
            level++;
            exp = 0;

        }
    }
}
