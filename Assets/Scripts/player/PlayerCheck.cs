using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerCheck : MonoBehaviour {
    
    XboxInputMenu xboxInputMenu;
    
    void Start()
    {
        xboxInputMenu = GetComponent<XboxInputMenu>();
    }
    
    void OnTriggerEnter(Collider col)
    {/*
        if (col.gameObject.tag == "Player")
        {
            if()
            SceneManager.LoadScene("Game");//Load Scene with game
            
            if ()
            {
                Play Endgame cutscene or animation.

            }
        }*/
    }
}
