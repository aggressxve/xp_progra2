using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBala : MonoBehaviour
{
    [SerializeField] private Bala pfBala;

    void OnEnable()
    {
        /* Cada vez que se habilite el script o el gameobject
        Va a instanciar la bala en la posicion del mismo objeto */
        Instantiate(pfBala, transform.position, transform.rotation);
    }
}
