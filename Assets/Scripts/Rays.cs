using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rays : MonoBehaviour
{
    LineRenderer lr;
    public int maxReflections = 20;
    private Vector3 position;
    private Vector3 direction;
    private int pCount;


    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, transform.position);
        lr.positionCount = maxReflections;
    }

    // Update is called once per frame
    void Update()
    {
        pCount = 0;
        ReflectRay(transform.position, transform.forward);
    }

    void ReflectRay(Vector3 position, Vector3 direction)
    {

        lr.SetPosition(0, transform.position);

        for (int i = 0; i < maxReflections; i++)
        {
            Ray ray = new Ray (position, direction);
            RaycastHit hit;

            if (pCount < maxReflections - 1)
                pCount++;

            if (Physics.Raycast(ray, out hit, 300))
            {

                position = hit.point;
                direction = Vector3.Reflect(direction, hit.normal);
                lr.SetPosition(pCount, hit.point);

                if (hit.transform.tag != "Mirror")
                {
                    for (int j = (i + 1); j < maxReflections; j++)
                    {
                        lr.SetPosition(j, hit.point);
                    }
                    break;
                }
                else
                {
                    lr.SetPosition(pCount, hit.point);
                }

            }
            else
            {
                lr.SetPosition(pCount, ray.GetPoint(5));
            }
        }

    }

}
