using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//добавление всех Update в список
public class UpdateManager : MonoBehaviour
{
    private void Update()
    {
        for (int i = 0; i < MonoCache.allUpdate.Count; i++) MonoCache.allUpdate[i].Tick();
    }
}
