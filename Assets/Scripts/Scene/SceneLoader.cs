﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    public TextMeshProUGUI gearsText;
    public Image subMenuImage1;
    public Image subMenuImage2;
    public Image Controles;
    public Image MenuNormal;
    public static SceneLoader Instance { get; private set; }
    public bool ingame;

    private CanvasController canvasController;
    private AsyncOperation _loadSceneOperation;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if (ingame)
        {
        gearsText.text = GameManager.Instance.points.ToString();
        }
    }

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

    public void Control()
    {
        Controles.gameObject.SetActive(!Controles.gameObject.activeSelf);
        MenuNormal.gameObject.SetActive(!MenuNormal.gameObject.activeSelf);
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
