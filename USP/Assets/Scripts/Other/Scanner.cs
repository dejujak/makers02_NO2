using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;
    public Transform nearestTarget;

    private void Update()
    {
        /*switch (Weapon.instance.id)
        {
            case 1:
            case 3:
                scanRange = 3;
                break;
            case 5:
                scanRange = 3+0.5f*GameManager.instance.rateLevel;
                break;
            case 2:
                scanRange = 6;
                break;
            case 4:
                scanRange = 5+GameManager.instance.rateLevel;
                break;
        }*/
    }

    private void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        nearestTarget = GetNearest();
    }

    Transform GetNearest()
    {
        Transform result = null;
        float diff = 100;

        foreach(RaycastHit2D target in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos);

            if (curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }

        return result;
    }
}
