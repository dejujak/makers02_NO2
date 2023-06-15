using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;

    public void Init(ItemData data)
    {
        //Basic Set
        name = "Gear " + data.itemId;
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        //Property Set
        type = data.itemType;
        rate = data.damages[0];
        ApplyGear();
    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }

    void ApplyGear()
    {
        switch (type)
        {
            case ItemData.ItemType.Glove:
                RateUp();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
            case ItemData.ItemType.DamageUp:
                DamageUp();
                break;
            case ItemData.ItemType.DotUp:
                DotUp();
                break;
        }
    }

    void RateUp()
    {
            GameManager.instance.rateLevel++;
    }

    void DamageUp()
    {
            GameManager.instance.attackLevel++;
    }

    void DotUp()
    {
        GameManager.instance.dotLevel++;
    }


    void SpeedUp()
    {
        float speed = 3;
        GameManager.instance.player.speed = speed + speed * rate;
    }
}
