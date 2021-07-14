using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public int ringCount;
    Text ringText;


    void Start()
    {
        ringCount = 0;
        ringText = GameObject.Find("RingText").GetComponent<Text>();
    }

    void Update()
    {
        
    }

    public void AddRings(int count)
    {
        ringCount += count;
        ringText.text = "Rings: " + ringCount;
    }





}
