using UnityEngine;

public class Laser : MonoBehaviour
{

    private int maxReflections = 20;

    private int posCount;
    private LineRenderer lr;

    [SerializeField]
    private Vector3 _offSet;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = maxReflections;

    }
    private void Update()
    {
        posCount = 0;
        ReflectRay(transform.position + _offSet, transform.forward);
    }
    private void ReflectRay(Vector3 position, Vector3 direction)
    {
        lr.SetPosition(0, transform.position + _offSet);

        for (int i = 0; i < maxReflections; i++)
        {

            Ray ray = new Ray(position, direction);
            RaycastHit hit;

            if (posCount < maxReflections - 1)
                posCount++;

            if (Physics.Raycast(ray, out hit, 300))
            {

                position = hit.point;
                direction = Vector3.Reflect(direction, hit.normal);
                lr.SetPosition(posCount, hit.point);

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

                    lr.SetPosition(posCount, hit.point);
                }
            }
            else
            {

                lr.SetPosition(posCount, ray.GetPoint(300));

            }


        }

    }

}