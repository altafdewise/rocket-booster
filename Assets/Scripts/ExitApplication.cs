using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitApplication : MonoBehaviour
{
    
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape)){
            Debug.Log("You have exit the game.");
            Application.Quit();
        }
    }
}
