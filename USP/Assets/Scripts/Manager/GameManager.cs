using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 2 * 10f;

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

    public int curWI;
    public int attackLevel;
    public int rateLevel;
    public int dotLevel;

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
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }

        //if (curWI != 0)
        //{
        //    GameObject weapon = GameObject.Find("Weapon " + curWI);
        //    if (weapon == null)
        //        return;
        //    weapon.gameObject.SetActive(true);
        //}
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
