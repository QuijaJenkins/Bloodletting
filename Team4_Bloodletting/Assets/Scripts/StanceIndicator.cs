using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StanceIndicator : MonoBehaviour
{
    public GameHandler gamescript;
    public int stanceNumber;
    public GameObject Stance1;
    public GameObject Stance2;
    public GameObject Stance3;



    // Start is called before the first frame update
    void Start()
    {
       
        Stance1.SetActive(false);
        Stance2.SetActive(false);
        Stance3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        stanceNumber = gamescript.stanceNumber;
        if (stanceNumber == 1) {
            Stance1.SetActive(true);
            Stance2.SetActive(false);
            Stance3.SetActive(false);
        }
        else if (stanceNumber == 2) {
            Stance1.SetActive(false);
            Stance2.SetActive(true);
            Stance3.SetActive(false);
        } else if (stanceNumber == 3) {
            Stance1.SetActive(false);
            Stance2.SetActive(false);
            Stance3.SetActive(true);
        }

    }
}
