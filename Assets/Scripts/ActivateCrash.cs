using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActivateCrash : MonoBehaviour
{
    public GameObject aguanteCrash, credits, exitButton, randomButton,crash;

    public void Activate()
    {
        aguanteCrash.SetActive(!aguanteCrash.activeSelf);
        credits.SetActive(!credits.activeSelf);
        exitButton.SetActive(!exitButton.activeSelf);
        randomButton.SetActive(!randomButton.activeSelf);
        crash.SetActive(!crash.activeSelf);
    }
}
