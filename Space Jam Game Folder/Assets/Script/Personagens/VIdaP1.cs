using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VIdaP1 : MonoBehaviour
{
    [Header("Vida")]
    public int Hearts;
    public TextMeshProUGUI Text;
    public GameObject HeartImage;
    public GameObject HalfHeartImage;
    public GameObject EmptyHeartImage;
    public static VIdaP1 instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        Life();
        Text.text = Hearts.ToString();
    }
    void Life()
    {
        if(Hearts == 3)
        {
            HeartImage.SetActive(true);
            HalfHeartImage.SetActive(false);
            EmptyHeartImage.SetActive(false);

        }        
        else if((Hearts == 2) || (Hearts == 1))
        {
            HeartImage.SetActive(false);
            HalfHeartImage.SetActive(true);
            EmptyHeartImage.SetActive(false);            
        }
        else if(Hearts == 0)
        {
            HeartImage.SetActive(false);
            HalfHeartImage.SetActive(false);
            EmptyHeartImage.SetActive(true);
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            Hearts -= 1;
        }
    }
}
