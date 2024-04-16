using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerFunction : MonoBehaviour
{
    public UnityEvent function;
    
    void LoadFunction()
    {
        function.Invoke();
    }
}
