using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoSceneChange : MonoBehaviour
{
    public string scene;
    private void Update()
    {
        if (Input.anyKey)
            SceneManager.LoadScene(scene);
    }
}
