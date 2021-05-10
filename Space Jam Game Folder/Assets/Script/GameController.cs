using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public int ScoreUpdate;
    public TextMeshProUGUI ScoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScoreTextUpdate();
    }
    void ScoreTextUpdate()
    {
        ScoreText.text = ScoreUpdate.ToString();
/**        ScoreUpdate = ArmaMao.instance.Municao;
**/
    }
}
