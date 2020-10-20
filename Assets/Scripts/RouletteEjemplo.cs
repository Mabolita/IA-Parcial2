using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteEjemplo : MonoBehaviour, IRoulette<string>
{
    public string Run(Dictionary<string, int> dic)
    {
        int total = 0;
        //para saber el porcentaje total
        foreach (var item in dic)
        {
            total += item.Value;
        }
        //elejir un numero random para la aleotaridad
        int random = Random.Range(0, total);
        //verifica cual toco
        foreach (var item in dic)
        {
            random = random - item.Value;
            if (random < 0)
            {
                return item.Key;
            }
        }
        return default;
    }
}
