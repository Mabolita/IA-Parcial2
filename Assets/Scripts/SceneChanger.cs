using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    //Cargar escenas
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }



    //Cerrar juego
    public void QuitGame()
    {
        Application.Quit();
    }
}
