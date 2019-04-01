using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToScene1()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void ToScene2()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
