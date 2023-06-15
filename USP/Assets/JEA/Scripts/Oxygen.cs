using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygen : MonoBehaviour
{
    float timer;
    int OxygenNumber = 100; //산소량 
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
            timer = 0; //산소량은 2초지날때마다 기본적으로  5씩 줄어듦 
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
            OxygenNumber -= 10; //플레이어와 산소통이 닿으면 10씩 줄어든다 
        }
        

    }

}
