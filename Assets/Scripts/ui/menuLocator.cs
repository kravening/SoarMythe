using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class menuLocator : MonoBehaviour
{
    public string loadLevel;
    XboxInputMenu XboxInput;
    bool aButton;
    [SerializeField]
    GameObject[] Buttons;

    void Start()
    {
        //xbox360WiredInputController = GetComponent<Xbox360Wired_InputController>();
    }

    public void pickButton()
    {
        if (aButton == true)
        {/*
            if ()
            {
                
            }*/
        }
    }
}
