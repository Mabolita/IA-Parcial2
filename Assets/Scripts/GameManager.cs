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
    }

    private void Start()
    {
        _currentCheckpoint = _spawnPoint;
    }


    private void Update()
    {
        points = CanvasController.gearsCount;

        if (CanvasController.lose == true)
        {
            Lose();
        }

    }

    public void SetNewCheckpoint(Transform checkPoint)
    {
        _currentCheckpoint = checkPoint;
    }

    public void Lose()
    {
        SceneManager.LoadScene("Lose");
    }

    public void Win()
    {
        SceneManager.LoadScene("Win");
    }
}
