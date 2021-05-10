using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UISystem : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameOver;
    public GameObject victory;
    public GameObject tutorial;
    public GameObject menu;

    void Start() {

    }
    public void showGameOver() {
        gameOver.SetActive(true);
    }
    public void showMenu() {
        menu.SetActive(true);
        tutorial.SetActive(false);
    }
    public void changeScenes(string lvlName){
        SceneManager.LoadScene(lvlName);
        gameOver.SetActive(false);
        victory.SetActive(false);
        tutorial.SetActive(false);
    }
    public void ShowVictory(){
        victory.SetActive(true);
        if(victory == true){
            //Player.instance.Movimento = false;
        }
    }

    public void ShowTutorial(){
        menu.SetActive(false);
        tutorial.SetActive(true);
    }

    public void exit() {
        Application.Quit();
    }
}
