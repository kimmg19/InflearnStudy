using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Sprites;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int type = 0;
    public int hp = 3;
    public float speed = 4;
    public float coin = 0;
    void Start()
    {
        switch (type)
        {
            case 0:
                hp = 10;
                speed = 1.4f;
                coin = 3;
                break;
            case 1:
                hp = 20;
                speed = 1.3f;
                coin = 4;
                break;
            case 2:
                hp = 50;
                speed = 1.2f;
                coin = 5;
                break;
        }
    }

    
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
