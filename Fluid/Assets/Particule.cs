using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particule : MonoBehaviour
{
    public float mass;

    private float gravity = -9.8f;

    public Vector3 acceleration;
    public Vector3 speed;

    //private float Cd = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        mass = Random.Range(1, 10);
    }

    // Update is called once per frame
    void Update()
    {
        NewAcceleration();
        NewSpeed();
        NewPosition();
    }

    private void NewAcceleration()
    {
        acceleration = new Vector3(0, gravity, 0);
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
}
