using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public static SceneControl Instance;

    public GameObject panelLoading;


    public float delay;
    private string nameScene;

    private void Awake()
    {
        Instance = this;
    }
    public void ChangeScene(string scene)
    {
        panelLoading.SetActive(true);
        nameScene = scene;

        Invoke(nameof(LoadScene), delay);
    }
    private void LoadScene()
    {
        SceneManager.LoadScene(nameScene);
    }
    public void RestartScene()
    {
        ChangeScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        panelLoading.SetActive(true);
        Invoke(nameof(Quit), delay);
    }
    private void Quit()
    {
        Application.Quit();
    }
}
