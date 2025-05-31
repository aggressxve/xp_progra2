using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestruccion : MonoBehaviour
{
    [SerializeField] private float tiempo;
    void Start()
    {
        Invoke("Destruir", tiempo);
    }

    private void Destruir()
    {
        Destroy(gameObject);
    }
}