using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public static Weapon instance;

    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    float timer;
    Player player;

    private void Awake()
    {
        player = GameManager.instance.player;
        instance = this;
    }

    void Update()
    {
            switch (id)
            {
                case 0:
                    transform.Rotate(Vector3.back * speed * Time.deltaTime);
                    break;
                case 4:
                    Fire();
                    break;
                default:
                    timer += Time.deltaTime;
                    if (timer > speed)
                    {
                        timer = 0f;
                        Fire();
                    }
                    break;
            }
    }

    public void Init(ItemData data)
    {
        //Basic Set
        name = "Weapon " + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;


        //Property Set
        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;

        for(int index = 0; index < GameManager.instance.pool.prefabs.Length; index++)
        {
            if(data.projectile== GameManager.instance.pool.prefabs[index])
            {
                prefabId = index;
                break;
            }
        }

        switch (id)
        {
            case 0:
                speed = 150;
                Batch();
                break;
            case 1:
                speed = 0.3f;
                break;
            case 2:
                speed = 0.1f;
                break;
            case 3:
                speed = 0.5f;
                break;
            case 4:
                LaserFlame();
                break;
            case 5:
                LaserFlame();
                break;
        }

        player.BroadcastMessage("ApplyGear",SendMessageOptions.DontRequireReceiver);
    }

    public void LevelUp(float damage,int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
            Batch();

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    void Batch()
    {
        for(int index = 0; index < count; index++)
        {
            Transform bullet;
            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage, -1,Vector3.zero);
        }
    }

    void Fire()
    {
        if (!player.scanner.nearestTarget)
        {
            if (id == 4)
            {
                GameObject Laser= transform.Find("LaserBullet(Clone)").gameObject;
                Laser.SetActive(false);
                return;
            }
            else if (id == 5)
            {
                GameObject Flame = transform.Find("FlameBullet(Clone)").gameObject;
                Flame.SetActive(false);
                return;
            }
            return;
        }

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        if (id == 3)
        {
            Transform bullet01 = GameManager.instance.pool.Get(prefabId).transform;
            bullet01.position = transform.position;
            bullet01.rotation = Quaternion.FromToRotation(Vector3.up, dir);
            bullet01.GetComponent<Bullet>().Init(damage, count, dir);

            Transform bullet02 = GameManager.instance.pool.Get(prefabId).transform;
            bullet02.position = transform.position;
            bullet02.rotation = Quaternion.FromToRotation(Vector3.up, Quaternion.Euler(0, 0, -10) * dir);
            bullet02.GetComponent<Bullet>().Init(damage, count, Quaternion.Euler(0, 0, -10) * dir);

            Transform bullet03 = GameManager.instance.pool.Get(prefabId).transform;
            bullet03.position = transform.position;
            bullet03.rotation = Quaternion.FromToRotation(Vector3.up, Quaternion.Euler(0, 0, 10) * dir);
            bullet03.GetComponent<Bullet>().Init(damage, count, Quaternion.Euler(0, 0, 10) * dir);
        }
        else if (id == 4)
        {
            GameObject Laser = transform.Find("LaserBullet(Clone)").gameObject;
            Laser.SetActive(true);
            transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        }
        else if (id == 5)
        {
            GameObject Flame = transform.Find("FlameBullet(Clone)").gameObject;
            Flame.SetActive(true);
            transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        }
        else
        {
            Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
            bullet.position = transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
            bullet.GetComponent<Bullet>().Init(damage, count, dir);
        }
    }

    void LaserFlame()
    {
        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.parent = transform;
        bullet.position = transform.position;

        if (id == 4)
        {
            bullet.Translate(bullet.up * 9f, Space.World);
        }
        else if(id==5)
        {
            bullet.Translate(bullet.up * 3f, Space.World);
        }

        bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);
    }
}
