using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Transform Enemy;
    private Transform Healthbar;
    [SerializeField] private float HealthbarElevation = 0;

    void Start()
    {
        Enemy = Array.Find(GetComponentsInChildren<Transform>(), p => p.CompareTag("Enemy"));
        Healthbar = transform.Find("HealthWrapper").transform;
    }

    void Update()
    {
        Healthbar.position = new Vector2(Enemy.position.x, Enemy.position.y + HealthbarElevation);
    }
}
