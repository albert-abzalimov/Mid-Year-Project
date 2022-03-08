using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public GameObject main;
    public GameObject controls;
    public void StartGame()
    {
        if (SceneManager.GetActiveScene().buildIndex > 0){
            SceneManager.LoadScene(0);
            return;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void OpenControl(){
        main.SetActive(false);
        controls.SetActive(true);
    }
    public void Back(){
        main.SetActive(true);
        controls.SetActive(false);
    }
}
