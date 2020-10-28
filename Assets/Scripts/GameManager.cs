using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int points;
    [SerializeField] private Transform _spawnPoint;
    AudioSource audioSource;
    private Transform _currentCheckpoint;

    public Transform CurrentCheckPoint => _currentCheckpoint;

    private int num;
    private void Awake()
    {
        Cursor.lockState=CursorLockMode.Locked;
        Cursor.visible = false;
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        _currentCheckpoint = _spawnPoint;
        audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        points = CanvasController.gearsCount;

        if (CanvasController.lose == true)
        {
            Lose();
            CanvasController.lose = false;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            num--;
            if (num < 0)
            {
                num = 0;
            }
            SceneManager.LoadScene(num);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            audioSource.clip = null;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            num++;
            if (num > SceneManager.sceneCountInBuildSettings-1)
            {
                num = SceneManager.sceneCountInBuildSettings-1;
            }
            SceneManager.LoadScene(num);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            audioSource.clip = null;
        }
    }

    public void SetNewCheckpoint(Transform checkPoint)
    {
        _currentCheckpoint = checkPoint;
    }

    public void Lose()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        audioSource.clip = null;
        SceneManager.LoadScene("Lose");
    }

    public void Win()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        audioSource.clip = null;
        SceneManager.LoadScene("Win");

    }

    public void Menu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        audioSource.clip = null;
        SceneManager.LoadScene("Menu");
    }
}
