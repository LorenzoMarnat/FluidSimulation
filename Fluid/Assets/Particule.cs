using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particule : MonoBehaviour
{
    public Vector3 speed;

    public Collider[] neighbors;

    public float h;

    public Vector3 oldPosition;

    public bool appliedViscosity = false;

    public void OutOfBorders()
    {
        if (transform.position.y <= -11)
            transform.position = new Vector3(transform.position.x, -10.9f, transform.position.z);
        if (transform.position.y >= 11)
            transform.position = new Vector3(transform.position.x, 10.9f, transform.position.z);

        if (transform.position.x <= -4)
            transform.position = new Vector3(-3.9f, transform.position.y, transform.position.z);
        if (transform.position.x >= 4)
            transform.position = new Vector3(3.9f, transform.position.y, transform.position.z);

        if (transform.position.z <= -1)
            transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
        if (transform.position.z >= 1)
            transform.position = new Vector3(transform.position.x, transform.position.y, 0.5f);
    }

    public void GetNeighbors()
    {
        neighbors = Physics.OverlapSphere(transform.position, h);
    }
}
