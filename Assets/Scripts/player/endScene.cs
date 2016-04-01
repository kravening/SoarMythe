using UnityEngine;
using UnityEngine.SceneManagement;

public class endScene : MonoBehaviour {
    
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //spot for Play endscene
            SceneManager.LoadScene("MainMenu");//Load Scene with Menu
        }
    }
}
