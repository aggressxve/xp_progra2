using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    #region CORE
    private void Awake()
    {
        Awake_ObtenerComponentes();
    }

    private void Update()
    {
        Update_Movimiento();
    }
    #endregion CORE   

    #region COMPONENTES
    private CharacterController cc;
    private Animator animator;
    private void Awake_ObtenerComponentes()
    {
        cc = GetComponent<CharacterController>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    #endregion COMPONENTES

    #region MOVIMIENTO
    [Header("Movimiento")]
    [SerializeField] private float velocidad;

    private Vector3 axis;
    private Vector3 mov = Vector3.zero;
    public const float Gravedad = -9.81f;

    // Bloqueos
    public bool bloquearRotacion;

    private void Update_Movimiento()
    {
        // Registrar el axis del Input
        axis = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        // Normalizamos el axis, para evitar que tenga mas velocidad en las diagonales
        axis.Normalize();

        // Rotamos el personaje a donde apunta el mouse
        Rotar();

        // Enviamos nuestros parametros al animator
        animator.SetFloat("x", axis.x);
        animator.SetFloat("y", axis.z);

        // Calculamos el movimiento en X y Z
        Vector3 movXZ = axis * velocidad;
        mov.x = movXZ.x;
        mov.z = movXZ.z;

        // Está en el piso?
        if (cc.isGrounded)
        {
            mov.y = 0;
        }
        else // Está en el aire?
        {
            // Aplicamos la gravedad simulada
            mov.y += Gravedad * Time.deltaTime;
        }

        // Aplicamos el movimiento al Character Controller
        cc.Move(mov * Time.deltaTime);
    }

    private void Rotar()
    {
        if (bloquearRotacion) return;
        // Crear un rayo que va desde la camara hacia donde apunta el Mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Obtenemos la capa Piso
        LayerMask capa = LayerMask.GetMask("Piso");

        // Lanzamos el rayo que solo puede colisionar con objetos que tengan la capa Piso
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, capa))
        {
            // Pisicion donde colisiona el rayo
            Vector3 colision = hit.point;

            // Igualamos la altura de la colision a nuestra altura
            colision.y = transform.position.y;

            // Va a rotar a donde apuntamos
            transform.LookAt(colision);
        }
    }
    #endregion MOVIMIENTO
}
