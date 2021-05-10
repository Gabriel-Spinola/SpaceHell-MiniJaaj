using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private int numOfHearts;

    [SerializeField] private Image[] hearts;

    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;


    void Start()
    {
        GameObject.Find("Health Bar");       
    }

    // Update is called once per frame
    void Update()
    {
        int dam = 5;

        if (Player.I.health <= 80) {
            hearts[0].sprite = emptyHeart;
        }
        if (Player.I.health <= 60) {
            hearts[1].sprite = emptyHeart;
        }
        if (Player.I.health <= 40) {
            hearts[2].sprite = emptyHeart;
        }
        if (Player.I.health <= 20) {
            hearts[3].sprite = emptyHeart;;
        }
        if (Player.I.health <= 0) {
            hearts[4].sprite = emptyHeart;
        }

        print(Player.I.health);

        for (int i = 0; i < numOfHearts; i++) {
            hearts[i].enabled = i < numOfHearts;
        }
    }
}
