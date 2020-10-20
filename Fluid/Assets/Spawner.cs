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
            Vector3 pos = new Vector3(Random.Range(-1f, 1f), Random.Range(-5f, 5f), Random.Range(-1f, 1f));
            Instantiate(particule, pos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
