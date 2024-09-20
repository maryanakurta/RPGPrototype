using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chos : MonoBehaviour
{
    public Player anme;
    public bool isPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void findPos() {
        if (isPlayer)
        {
            anme.chos();
        }
    }

    public void findPosB() {
        if (isPlayer) {
            anme.chosD();
        }
    }
}
