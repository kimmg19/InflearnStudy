using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void GoGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
