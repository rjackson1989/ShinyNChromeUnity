using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadOnClick : MonoBehaviour {

    public void LoadLevel(int level)
    {
        if (level == 3) { Application.Quit(); }
        SceneManager.LoadScene(level);
    }
}
