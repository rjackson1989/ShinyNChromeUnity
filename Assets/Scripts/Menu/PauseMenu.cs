using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    public void selectPauseOption(int option)
    {
        switch (option)
        {
            case 0:
                SceneManager.LoadScene(0);
                break;
            case 1:
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            
        }
    }
}
