using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] private float dano = 10;
    [SerializeField] private float velocidad = 20;
    [SerializeField] private ParticleSystem explosion;

    private void Awake()
    {
        // Obtenemos el componente de Rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();

        // Le aplicamos una velocidad constante al frente
        rb.velocity = transform.forward * velocidad;
    }

    void OnCollisionEnter(Collision other)
    {
        // Si colisiona con un objeto de Layer 0: Default
        if (other.gameObject.layer == 0)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
