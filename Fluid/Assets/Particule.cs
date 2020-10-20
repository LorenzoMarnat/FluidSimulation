using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particule : MonoBehaviour
{
    public Vector3 speed;

    public Collider[] neighbors;

    public float h;

    public Vector3 oldPosition;

    // Update is called once per frame
    /*void Update()
    {
        speed += Time.deltaTime * gravity;

        Vector3 oldPosition = transform.position;
        transform.position += Time.deltaTime * speed;

        GetNeighbors();
        Density();

        OutOfBorders();

        speed = (transform.position - oldPosition) / Time.deltaTime;

    }*/

    public void OutOfBorders()
    {
        if (transform.position.y <= -11)
            transform.position = new Vector3(transform.position.x, -10.9f, transform.position.z);
        if (transform.position.y >= 11)
            transform.position = new Vector3(transform.position.x, 10.9f, transform.position.z);

        if (transform.position.x <= -3)
            transform.position = new Vector3(-2.9f, transform.position.y, transform.position.z);
        if (transform.position.x >= 3)
            transform.position = new Vector3(2.9f, transform.position.y, transform.position.z);

        if (transform.position.z <= -3)
            transform.position = new Vector3(transform.position.x, transform.position.y, -2.9f);
        if (transform.position.z >= 3)
            transform.position = new Vector3(transform.position.x, transform.position.y, 2.9f);
    }

    /*private void Density()
    {
        float density = 0;
        float densityNear = 0;

        foreach (Collider neighbor in neighbors)
        {
            float q = Vector3.Distance(transform.position, neighbor.gameObject.transform.position) / h;

            if (q < 1)
            {
                density += Mathf.Pow((1 - q), 2);
                densityNear += Mathf.Pow((1 - q), 3);
            }
        }

        float pressure = k * (density - densityNear);
        float pressureNear = kNear * densityNear;

        Vector3 offset = Vector3.zero;

        foreach (Collider neighbor in neighbors)
        {
            float q = Vector3.Distance(transform.position, neighbor.transform.position) / h;

            if (q < 1)
            {
                Vector3 distance = Mathf.Pow(Time.deltaTime, 2) * ((pressure * (1 - q)) + (pressureNear * Mathf.Pow(1 - q, 2))) * Vector3.Normalize(neighbor.gameObject.transform.position - transform.position);

                neighbor.transform.position += distance / 2f;

                offset -= distance / 2f;
            }
        }
        transform.position += offset;
    }*/

    public void GetNeighbors()
    {
        neighbors = Physics.OverlapSphere(transform.position, h);
    }
}
