using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particule : MonoBehaviour
{
    public float mass;

    public Vector3 gravity = new Vector3(0, -9.8f, 0);

    public Vector3 acceleration;
    public Vector3 speed;

    //private float Cd = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        mass = Random.Range(5, 10);
    }

    // Update is called once per frame
    void Update()
    {
        NewAcceleration();
        NewSpeed();
        NewPosition();
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
}
