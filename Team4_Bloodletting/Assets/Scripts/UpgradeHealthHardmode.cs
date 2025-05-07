using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UpgradeHealthHardmode : MonoBehaviour
{
    public TMP_Text healthText;
    // Start is called before the first frame update
    void Start()
    {
        if (GameHandler.hard)
        {
            healthText.text = "Health" + Environment.NewLine + "(+10%)";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
