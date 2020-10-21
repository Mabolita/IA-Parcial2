using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    [SerializeField] private GameObject _checkPointText;


    private void OnTriggerEnter(Collider other)
    {
        if (!_checkPointText.activeSelf) return;

        if (other.gameObject.layer == 8)
        {
            _checkPointText.SetActive(false);
            GameManager.Instance.SetNewCheckpoint(transform);
        }
    }
}
