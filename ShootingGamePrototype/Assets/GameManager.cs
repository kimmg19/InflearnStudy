using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject asteroid;
    public float time = 0;
    public float maxTime = 2;
    public List<GameObject> enemies;
    public static GameManager instance;
    public float coin;

    private void Awake()
    {
        instance = this; 
    }

    private void Start()
    {
        coin = 0;
    }

    void Update()
    {
        //2檬俊 茄锅究 家青己 积己
        time += Time.deltaTime;
        if (time > maxTime)
        {
            int check = Random.Range(0, 2);
            if(check == 0)
            {
                Vector3 vec = new Vector3(10, Random.Range(-4.0f, 4.0f), 0);
                Instantiate(asteroid, vec, Quaternion.identity);
            }
            else
            {
                int type=Random.Range(0, 3);
                Vector3 vec = new Vector3(10, Random.Range(-4.0f, 4.0f), 0);
                Instantiate(enemies[type], vec, Quaternion.identity);
            }
            
            
            time = 0;
        }
    }
}
