using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygen : MonoBehaviour
{
    float timer;
    int OxygenNumber = 100; //��ҷ� 
    public GameObject OxygenCase;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 2)
        {
            OxygenNumber += -5;
            timer = 0; //��ҷ��� 2������������ �⺻������  5�� �پ�� 
        }

        if (OxygenNumber == 0)
        {
            Destroy(OxygenCase);
        }
    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OxygenNumber -= 10; //�÷��̾�� ������� ������ 10�� �پ��� 
        }
        

    }

}
