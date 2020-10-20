using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particule : MonoBehaviour
{
    private float mass;

    public float densityZero = 1f;
    public float h = 1f;
    public float k = 0.1f;
    public float kNear = 0.1f;

    public Vector3 gravity = new Vector3(0, -9.8f, 0);

    public Vector3 acceleration;
    public Vector3 speed;

    private GameObject[] neighbors;
    // Start is called before the first frame update
    void Start()
    {
        mass = Random.Range(5f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        speed += Time.deltaTime * gravity;

        Vector3 oldPosition = transform.position;
        transform.position += Time.deltaTime * speed;

        GetNeighbors();
        Density();

        speed = (transform.position - oldPosition) / Time.deltaTime;

        /*NewAcceleration();
        NewSpeed();
        NewPosition();*/
        OutOfBorders();
    }

    private void NewAcceleration()
    {
        acceleration = mass * gravity;
        acceleration -= speed * speed.magnitude;
        acceleration /= mass;
    }
    private void NewSpeed()
    {
        speed += acceleration * Time.deltaTime;
    }
    private void NewPosition()
    {
        transform.position += speed * Time.deltaTime;
    }

    private void OutOfBorders()
    {
        if(transform.position.y <= -11)
        {
            transform.position = new Vector3(transform.position.x, -11, transform.position.z);
            GetComponent<Particule>().enabled = false;
        }
    }

    private void Density()
    {
        float density = 0;
        float densityNear = 0;

        foreach(GameObject neighbor in neighbors)
        {
            float q = Vector3.Distance(transform.position, neighbor.transform.position) / h;

            if(q < 1)
            {
                density += (1 - q) * (1 - q);
                densityNear += (1 - q) * (1 - q) * (1 - q);
            }
        }

        float pressure = k*(density - densityNear);
        float pressureNear = kNear * densityNear;

        Vector3 offset = Vector3.zero;

        foreach (GameObject neighbor in neighbors)
        {
            float q = Vector3.Distance(transform.position, neighbor.transform.position) / h;

            if (q < 1)
            {
                Vector3 distance = Mathf.Pow(Time.deltaTime, 2) * ((pressure * (1 - q))+(pressureNear*Mathf.Pow(1-q,2))) * Vector3.Normalize(neighbor.transform.position - transform.position);

                neighbor.transform.position += distance / 2f;

                offset -= distance / 2f;
            }
        }
        transform.position += offset;
    }

    private void GetNeighbors()
    {
        neighbors = GameObject.FindGameObjectsWithTag("Player");
    }
}
