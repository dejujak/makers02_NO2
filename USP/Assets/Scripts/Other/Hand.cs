using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer spriter;
    public bool isReverse;

    SpriteRenderer player;

    Vector3 rightPos = new Vector3(-0.3f, -0.3f, 0);
    Vector3 rightPosReverse = new Vector3(0.3f, -0.3f, 0);
    Quaternion leftRot = Quaternion.Euler(0, 0, 35);
    Quaternion leftRotReverse = Quaternion.Euler(0, 0, 135);

    private void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    private void LateUpdate()
    {
        isReverse = (player.flipX);

        if (isLeft)
        {
            transform.localRotation = isReverse ? leftRotReverse : leftRot;
            spriter.flipY = isReverse;
            spriter.sortingOrder = isReverse ? 4 : 6;
        }

        if (GameManager.instance.player.scanner.nearestTarget)
        {
            
            Vector3 targetPos = GameManager.instance.player.scanner.nearestTarget.position;
            Vector3 dir = targetPos - transform.position;
            transform.localRotation = Quaternion.FromToRotation(Vector3.left, dir);

            bool isRotA = transform.localRotation.eulerAngles.z > 90 && transform.localRotation.eulerAngles.z < 270;
            bool isRotB= transform.localRotation.eulerAngles.z <- 90 && transform.localRotation.eulerAngles.z >-270;
            spriter.flipY = isRotA || isRotB;

            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriter.flipX = isReverse;
            if (isReverse)
            {
                spriter.flipX = !isReverse;
            }
            spriter.sortingOrder = 6;
        }
        else
        {
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriter.flipX = isReverse;
            spriter.sortingOrder = 6;
        }
    }
}
