using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Blood : MonoBehaviour
{
    public GameObject blood;
    public bool canIns;

    private void Update() {
        if (canIns) {
            Instantiate(blood);
        }
    }
}
