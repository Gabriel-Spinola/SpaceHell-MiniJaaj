using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy I { get; private set; }

    [SerializeField] private int health;

    private void Awake() => I = this;

    private void Update() 
    {
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage) => health -= damage;
}
