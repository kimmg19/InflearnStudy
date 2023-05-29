using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject asteroid;
    public float time = 0;
    public float maxTime = 2;
    public List<GameObject> enemies;        //유니티에서 프리팹 연결.
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
        //2초에 한번씩 소행성,적 생성
        time += Time.deltaTime;
        if (time > maxTime)
        {
            int check = Random.Range(0, 2);
            if(check == 0)
            {
                //소행성 생성
                Vector3 vec = new Vector3(10, Random.Range(-4.0f, 4.0f), 0);
                Instantiate(asteroid, vec, Quaternion.identity);
            }
            else
            {
                //적 생성-0~2중 하나 받아 instantiate 함수를 통해 switch로 적 생성.
                int type=Random.Range(0, 3);
                Vector3 vec = new Vector3(10, Random.Range(-4.0f, 4.0f), 0);
                Instantiate(enemies[type], vec, Quaternion.identity);
            }
            
            
            time = 0;
        }
    }
}
