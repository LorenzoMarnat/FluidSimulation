using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int count = 1000;
    public GameObject particule;

    [Range(0f,10f)]
    public float densityZero = 1f;
    [Range(0f, 2f)]
    public float h = 1f;
    [Range(0f, 10f)]
    public float k = 0.1f;
    [Range(0f, 10f)]
    public float kNear = 0.1f;
    [Range(0f, 2f)]
    public float alpha = 0.1f;
    [Range(0f, 2f)]
    public float beta = 0.1f;

    public Vector3 gravity = new Vector3(0, -9.8f, 0);

    private GameObject[] particules;
    // Start is called before the first frame update
    void Start()
    {
        particules = new GameObject[count];
        for(int i = 0;i <count;i++)
        {
            Vector3 pos = new Vector3(Random.Range(-3f, 3f), Random.Range(-2f, 2f), Random.Range(0f, 0f));
            particules[i] = Instantiate(particule, pos, Quaternion.identity);
            particules[i].GetComponent<Particule>().h = h;
        }
    }
    // Update is called once per frame
    void Update()
    {
        foreach (GameObject p in particules)
        {
            Particule pt = p.GetComponent<Particule>();
            pt.speed += Time.deltaTime * gravity;
            pt.GetNeighbors();
        }

        Viscosity();

        foreach (GameObject p in particules)
        {
            Particule pt = p.GetComponent<Particule>();
            pt.oldPosition = pt.transform.position;
            pt.transform.position += Time.deltaTime * pt.speed;
        }

        Density();

        foreach (GameObject p in particules)
        {
            Particule pt = p.GetComponent<Particule>();
            pt.OutOfBorders();

            pt.speed = (p.transform.position - pt.oldPosition) / Time.deltaTime;

            pt.appliedViscosity = false;
        }
    }

    private void Density()
    {
        foreach (GameObject p in particules)
        {
            Particule pt = p.GetComponent<Particule>();

            float density = 0;
            float densityNear = 0;

            foreach (Collider neighbor in pt.neighbors)
            {
                float q = Vector3.Distance(p.transform.position, neighbor.gameObject.transform.position) / h;

                if (q < 1)
                {
                    density += Mathf.Pow((1 - q), 2);
                    densityNear += Mathf.Pow((1 - q), 3);
                }
            }

            float pressure = k * (density - densityZero);
            float pressureNear = kNear * densityNear;

            Vector3 offset = Vector3.zero;

            foreach (Collider neighbor in pt.neighbors)
            {
                float q = Vector3.Distance(p.transform.position, neighbor.gameObject.transform.position) / h;

                if (q < 1)
                {
                    Vector3 distance = Mathf.Pow(Time.deltaTime, 2) * ((pressure * (1 - q)) + (pressureNear * Mathf.Pow(1 - q, 2))) * Vector3.Normalize(neighbor.gameObject.transform.position - p.transform.position);

                    neighbor.gameObject.transform.position += distance / 2f;

                    offset -= distance / 2f;
                }
            }
            p.transform.position += offset;
        }
    }

    private void Viscosity()
    {
        foreach(GameObject p in particules)
        {
            Particule pt = p.GetComponent<Particule>();
            pt.appliedViscosity = true;

            foreach (Collider neighbor in pt.neighbors)
            {
                Particule ptNeighbor = neighbor.gameObject.GetComponent<Particule>();

                if (!ptNeighbor.appliedViscosity)
                {
                    float q = Vector3.Distance(p.transform.position, neighbor.gameObject.transform.position) / h;

                    if (q < 1)
                    {
                        float radialVelocity = Vector3.Dot((pt.speed - ptNeighbor.speed), Vector3.Normalize(neighbor.gameObject.transform.position - p.transform.position));

                        if(radialVelocity > 0)
                        {
                            Vector3 impulse = Time.deltaTime * (1-q) * ((alpha*radialVelocity)+(beta*Mathf.Pow(radialVelocity,2))) * Vector3.Normalize(neighbor.gameObject.transform.position - p.transform.position);
                            pt.speed -= impulse / 2f;
                            ptNeighbor.speed += impulse / 2f;
                        }
                    }
                }
            }
        }
    }
}
