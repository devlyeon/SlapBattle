using UnityEngine;
using UnityEngine.Events;

public class StartSound : MonoBehaviour
{
    public UnityEvent ON_START;

    private void Start()
    {
        ON_START.Invoke();
    }
}
