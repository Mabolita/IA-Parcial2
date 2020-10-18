using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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
    }

    private void Start()
    {
        _currentCheckpoint = _spawnPoint;
    }

    
    

    public void SetNewCheckpoint(Transform checkPoint)
    {
        _currentCheckpoint = checkPoint;
    }
}
