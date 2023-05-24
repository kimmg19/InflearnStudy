using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScript : MonoBehaviour
{
    public float speed = 1.3f;
    SpriteRenderer spr;
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        //Vector.left=방향지정
        transform.position += Vector3.left * Time.deltaTime * speed; 
        Vector3 pos=transform.position;

        //spr.bounds.size.x-바운딩 박스의 가로 값
        if (pos.x + spr.bounds.size.x / 2 < -8)
        {
            float size=spr.bounds.size.x * 2;
            pos.x += size;
            transform.position = pos;
        }
    }
}
