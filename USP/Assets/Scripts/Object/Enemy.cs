using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool isLive;
    public bool CheckLive() { return isLive;  }

    bool isRandomDash = false;
    bool isAttack = false;

    private float fDurationTimeToDash = 1.0f;
    private float fRemainTimeToDash = 0;
    private float fRangeToDash = 5;
    private bool isDashing = false;
    private Vector2 vecDash = new Vector2(0,0);

    public void SetRandomDash() { isRandomDash = true; }

    public void SetAttack() { isAttack = true; }


    Rigidbody2D rigid;
    Collider2D coll;
    SpriteRenderer spriter;
    Animator anim;
    WaitForFixedUpdate wait;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        wait = new WaitForFixedUpdate();
    }

  

    void FixedUpdate()
    {
        if (!isLive||anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        Vector2 dirVec = target.position - rigid.position;
        float finalSpeed = speed;

        if ( isRandomDash == true )
        {
            if ( isDashing == false )
            {
                if (dirVec.magnitude < fRangeToDash )
                {
                    isDashing = true;
                    vecDash = dirVec.normalized;
                    fRemainTimeToDash = fDurationTimeToDash;

                    ////////////////////////////////////////////////
                    //bullet
                    /*
                    Transform bullet;
                    bullet = GameManager.instance.pool.Get(2).transform;
                    
                    bullet.localPosition = transform.localPosition;


                    bullet.GetComponent<Bullet>().Init(0, -1, Vector3.zero);
                    */
                    ///////////////////////////////////////////////////////////////////////
                }
            }
            else
            {
                fRemainTimeToDash -= Time.fixedDeltaTime;
                if (fRemainTimeToDash <= 0 )
                {
                    isDashing = false;
                }
                else
                {
                    dirVec = vecDash;
                    finalSpeed *= Random.Range(2, 5.0f);
                }
                
            }
        }
  
       
       
        Vector2 nextVec = dirVec.normalized * finalSpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!isLive)
            return;

        spriter.flipX = target.position.x < rigid.position.x;
    }

    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet")||!isLive||(Weapon.instance.id==4||Weapon.instance.id==5))
            return;

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());

        if (health > 0)
        {
            anim.SetTrigger("Hit");
        }
        else
        {
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead",true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet")||(Weapon.instance.id!=4&&Weapon.instance.id!=5))
            return;

        health -= collision.GetComponent<Bullet>().damage;
        //StartCoroutine(KnockBack());

        if (health > 0)
        {
            //anim.SetTrigger("Hit");
        }
        else
        {
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }
    }

    IEnumerator KnockBack()
    {
        yield return wait;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
