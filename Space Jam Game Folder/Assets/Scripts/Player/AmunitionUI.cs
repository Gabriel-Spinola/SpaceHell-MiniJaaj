using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmunitionUI : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Awake() => text = GetComponent<TextMeshProUGUI>();

    private void Update() {
        text.SetText($"{ Weapon.I.currentAmmo } balas");

        print(Weapon.I.currentAmmo + "asas");
    }
}
