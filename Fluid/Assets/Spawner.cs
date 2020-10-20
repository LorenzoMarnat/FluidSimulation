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
            Vector3 pos = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f));
            Instantiate(particule, pos, Quaternion.identity);
        }
    }
}
