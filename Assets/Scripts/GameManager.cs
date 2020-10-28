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
        DontDestroyOnLoad(gameObject);
        Cursor.lockState=CursorLockMode.Locked;
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

        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    Win();
        //}

    }

    public void SetNewCheckpoint(Transform checkPoint)
    {
        _currentCheckpoint = checkPoint;
    }

    public void Lose()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Lose");
    }

    public void Win()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Win");
        audioSource.clip = null;

    }
}
