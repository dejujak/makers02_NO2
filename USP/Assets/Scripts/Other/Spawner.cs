using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    float timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        

        int stage = GameManager.instance.GetStage();
        if (stage >= 0 
            && stage < spawnData.Length 
            && GameManager.instance.GetPauseProgressBeforeEnemyAllDie() == false )
        {
            
            timer += Time.deltaTime;
            if (timer > spawnData[stage].spawnTime)
            {
                Debug.Log("stage : " + stage);
                if ( spawnData[stage].boss )
                {
                    Debug.Log("SetPauseProgressBeforeEnemyAllDie");
                    GameManager.instance.SetPauseProgressBeforeEnemyAllDie();
                }
                timer = 0f;
                Spawn(stage, spawnData[stage]);
            }
        }
        
    }

    void Spawn(int stage, SpawnData _spawnData)
    {
        GameObject enemy=GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[stage]);
        enemy.transform.localScale = new Vector3(_spawnData.size, _spawnData.size, 1);
        if (_spawnData.randomDash )
        {
            enemy.GetComponent<Enemy>().SetRandomDash();

        }

        if(_spawnData.attack)
        {
            enemy.GetComponent<Enemy>().SetAttack();
        }
    }
}

[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;
    public bool boss;
    public bool randomDash;
    public bool attack;
    public float size = 1;

}
