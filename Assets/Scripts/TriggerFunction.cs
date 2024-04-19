using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerFunction : MonoBehaviour
{
    public UnityEvent function;
    public UnityEvent function2;
    public UnityEvent function3;

    public void LoadFunction()
    {
        function.Invoke();
    }
    public void LoadFunction2()
    {
        function2.Invoke();
    }
    public void LoadFunction3()
    {
        function3.Invoke();
    }
}
