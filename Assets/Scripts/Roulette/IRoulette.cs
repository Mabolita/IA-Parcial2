using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRoulette <TKey>
{
    TKey Run(Dictionary<TKey, int> dic);
}
