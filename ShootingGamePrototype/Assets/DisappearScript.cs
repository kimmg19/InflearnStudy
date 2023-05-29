using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearScript : MonoBehaviour
{
    public float time = 1.0f;
    void Start()
    {
        //time초 후에 코인 삭제
        Destroy(gameObject, time);
    }
}