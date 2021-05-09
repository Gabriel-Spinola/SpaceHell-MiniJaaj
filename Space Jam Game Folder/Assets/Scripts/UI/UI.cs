using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class uiPlayer {

    public bool isAlive;
    public int life;
    public int Munition;
    public Text MunitionText;

    public GameObject heartUI;
    public Sprite heartOn, heartOff;

    public void live() {
        if(life > 0) {
            isAlive = true;
        } else {
            isAlive = false;
        }
    }

    public void updateMunitionText(){
        MunitionText.text = Munition.ToString();
    }
}

public class uiSystem {
    public GameObject gameOver;
    public GameObject victory;

    public void showGameOver() {
        gameOver.SetActive(true);
    }

    public void changeScenes(string lvlName){
        SceneManager.LoadScene(lvlName);
    }

    public void ShowVictory(){
        victory.SetActive(true);
        if(victory == true){
            //Player.instance.Movimento = false;
        }
    }

    public void exit() {
        Application.Quit();
    }
}
