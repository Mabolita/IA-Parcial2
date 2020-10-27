using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Image subMenuImage1;
    public Image subMenuImage2;
    public static SceneLoader Instance { get; private set; }

    private AsyncOperation _loadSceneOperation;


    //Cargar escenas
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level 1");
    }
     
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Win()
    {
        SceneManager.LoadScene("Win");
    }

    public void Lose()
    {
        SceneManager.LoadScene("Lose");
    }


    //Activar y desactivar SubMenus
    public void ActivateSubMenu1()
    {
        subMenuImage1.gameObject.SetActive(true);
    }
    public void DeactivateSubMenu1()
    {
        subMenuImage1.gameObject.SetActive(false);
    }

    //Activar y desactivar SubMenus
    public void ActivateSubMenu2()
    {
        subMenuImage2.gameObject.SetActive(true);
    }
    public void DeactivateSubMenu2()
    { 
        subMenuImage2.gameObject.SetActive(false);
    }


    //Escena de carga
    IEnumerator LoadSceneAsync(string sceneName)
    {
        yield return new WaitForSeconds(1f);

        _loadSceneOperation = SceneManager.LoadSceneAsync(sceneName);

        if (!_loadSceneOperation.isDone)
            yield return null;
    }


    //Cerrar juego

    public void QuitGame()
    {
        Application.Quit();
    }
}
