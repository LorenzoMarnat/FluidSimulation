using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int count = 1000;
    public GameObject particule;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0;i <count;i++)
        {
            Vector3 pos = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
            Instantiate(particule, pos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
