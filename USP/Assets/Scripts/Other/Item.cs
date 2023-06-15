using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public static Item instance;
    public ItemData data;
    public int level;
    public Weapon weapon;
    public Gear gear;

    Image icon;
    Text textLevel;

    private void Awake()
    {
        instance = this;

        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
    }

    private void LateUpdate()
    {
        textLevel.text = "Lv." + (level+1);
    }

    public void OnClick()
    {
        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                GameManager.instance.curWI = data.itemId;
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon=newWeapon.AddComponent<Weapon>();
                    weapon.Init(data);
                    if (GameManager.instance.attackLevel > 0)
                    {
                        GameManager.instance.attackLevel--;
                    }
                    if (GameManager.instance.rateLevel > 0)
                    {
                        GameManager.instance.rateLevel--;
                    }
                    GameManager.instance.player.weaponObjs.Add(newWeapon);
                }
                else
                {
                    for(int i=0;i< GameManager.instance.player.weaponObjs.Count; i++)
                    {
                        if (GameManager.instance.player.weaponObjs[i].GetComponent<Weapon>().id == GameManager.instance.curWI)
                        {
                            GameManager.instance.player.weaponObjs[i].gameObject.SetActive(true);
                        }
                    }
                    Hand hand = GameManager.instance.player.hands[(int)data.itemType];
                    hand.spriter.sprite = data.hand;
                    hand.gameObject.SetActive(true);
                }
                /*else
                {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage += data.baseDamage * data.damages[level];
                    nextCount += data.counts[level];

                    weapon.LevelUp(nextDamage, nextCount);
                }*/

                level++;
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
            case ItemData.ItemType.DamageUp:
            case ItemData.ItemType.DotUp:
                if (level == 0)
                {
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(data);
                }
                else
                {
                    float nextRate = data.damages[level];
                    gear.LevelUp(nextRate);
                }

                level++;
                break;
            case ItemData.ItemType.Heal:
                GameManager.instance.maxHealth += 10;
                GameManager.instance.health = GameManager.instance.maxHealth;
                break;
        }

        if (level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }    
}
