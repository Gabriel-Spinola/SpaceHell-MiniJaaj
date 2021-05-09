using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedRooms : MonoBehaviour
{
    public static LockedRooms I { get; private set; }

    [SerializeField] private int amountOfEnemiesKilledNeeded;
    
    private void Update()
    {
        if (amountOfEnemiesKilledNeeded <= Player.I.enemiesKilled) {
            Destroy(gameObject);
        }
    }

    private void OnDestroy() 
    {
        Player.I.level++;
    }
}
