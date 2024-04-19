using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerFunction : MonoBehaviour
{
    public UnityEvent function;
    
    public void LoadFunction()
    {
        function.Invoke();
    }
}
