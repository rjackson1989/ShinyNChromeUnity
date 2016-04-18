using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadOnClick : MonoBehaviour {

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
}
