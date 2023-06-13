using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    Rigidbody2D rigid;

    private bool toEnemy = true;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage,int per,Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        if (per > -1)
        {
            rigid.velocity = dir*15f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (toEnemy == true)
        {
            if (!collision.CompareTag("Enemy") || per == -1 || Weapon.instance.id == 4)
                return;
        }
        else
        {
            if (!collision.CompareTag("Player") || per == -1 || Weapon.instance.id == 4)
                return;
        }

        per--;

        if (per == -1)
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
