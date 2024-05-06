using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnCompleteTest()
    {
        SceneManager.LoadScene("CompleteTest");
    }
    public void OnAnalysisTest()
    {
        SceneManager.LoadScene("AnalysisTest");
    }
    public void OnCreateTest()
    {
        SceneManager.LoadScene("CreateTest");
    }
    public void OnSettings()
    {
        SceneManager.LoadScene("Settings");
    }
    public void OnExitApplication()
    {
        Application.Quit(-1);
    }
}
