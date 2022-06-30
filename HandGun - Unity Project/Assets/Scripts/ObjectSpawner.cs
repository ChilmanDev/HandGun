using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] objects;
    [HideInInspector] bool active;
    
    Transform[] spawnPoints;
    private float count = 0.0f, timer;
    // Start is called before the first frame update
    void Awake()
    {
        spawnPoints = new Transform[transform.childCount];

        for(int i = 0; i < transform.childCount; i++)
        {
            spawnPoints[i] = transform.GetChild(i);
        }

        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            count +=  Time.deltaTime;

            if(count >= timer)
            {
                count = 0;           
                Spawn();
            }
        }
    }

    public void Spawn()
    {
        int randObj = Random.Range(0, objects.Length);

        int randPos = Random.Range(0, transform.childCount);

        Instantiate(objects[randObj], spawnPoints[randPos].position, objects[randObj].transform.rotation);

        GameManager.Instance.AddTarget();
    }

    public void Activate(float _beatTempo, int _difficulty)
    {
        timer = _beatTempo * _difficulty;

        active = true;
    }

    public void Deactivate()
    {
        active = false;
    }
}
