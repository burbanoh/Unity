using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorDeRayos : MonoBehaviour {

    #region Variables

    [Header("Objetos Para la interaccion del rayo")]
    //Objetivo (Persona a la que sigue el rayo)
    public Transform target;

    //Vertices del edificio donde se mueve el rayo
    public Transform verticeVariable, verticeFijo;

    //Variables para medir las distancias de los rayos
    public static float distanciaDirecta;
    public static float disInd1, disInd2;
    public static float distanciaIndirecta;
    public static float anguloDirecto, anguloIndirecto;

    //Direcciones de los vertices, se usan para direccionar el rayo
    private Vector3 dirVerticeVarible, dirVerticeFijo;

    //Direccion del vector del rayo directo e indirecto hacia el usuario objetivo
    private Vector3 dirTargetDirecto, dirTargetIndirecto;

    // Rayos directos y hit1 y hit2 indirectos
    private RaycastHit hit1, hit2;
    private RaycastHit hitDirect;

    //Variable de control para saber cuando esta en un vertice fijo y cuando no.
    bool ativarVerticeFijo = true;
    #endregion


    #region Buliltin Methods
    // Use this for initialization
    void Start()
    {
        distanciaDirecta = 0.0f;
        distanciaIndirecta = 0.0f;
        disInd2 = 0.0f;
        disInd1 = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        HandleRayDirect();
        HandleRayIndirect();     
    }
    #endregion

    #region Custom Methods

    void HandleRayDirect()
    {
        //Direccion del Rayo directo
        dirTargetDirecto = target.position - transform.position;

        //Genera rayo directo hacia el usuario
        if (Physics.Raycast(transform.position, dirTargetDirecto, out hitDirect, Mathf.Infinity))
        {
            //Primer rayo Directo solo se renderiza si no golpea con un obstaculo
            if (!hitDirect.transform.gameObject.CompareTag("Obstaculo"))
            {
                Datos.d3d = hitDirect.distance;
                Datos.angle1 = 180 * Mathf.Atan(Mathf.Abs(dirTargetDirecto.y) / Mathf.Abs(dirTargetDirecto.x)) / Mathf.PI;
                Debug.DrawRay(transform.position, dirTargetDirecto, Color.yellow);
            }
            else
            {
                Datos.d3d = 0f;
            }
        }
    }

    void HandleRayIndirect()
    {
        //Direccion del primer tramo del rayo indirecto hacia el vertice variable que se va actualizando dependiendo de la posicion del usuario 
        dirVerticeVarible = new Vector3(verticeVariable.position.x, verticeVariable.position.y, target.position.z) - transform.position;

        //Genera primer tramo del rayo indirecto hacia el el vertice variable o fijo
        if (Physics.Raycast(transform.position, dirVerticeVarible, out hit1, Mathf.Infinity))
        {
            //Genera la direccion del tramo 2 del rayo hacia el usuario
            dirTargetIndirecto = target.position - hit1.point;

            // Si el rayo golpea con el vertice fijo se activa el control para que quede con el vertice fijo
            if (hit1.transform.gameObject.CompareTag("VerticeFijo"))
            {
                //Debug.Log("Estoy en vertice fijo");
                ativarVerticeFijo = true;
                verticeFijo = hit1.transform.gameObject.transform;
            }

            //Genera el segundo tramo del rayo indirecto hacia el usuario
            Physics.Raycast(hit1.transform.position, dirTargetIndirecto, out hit2, Mathf.Infinity);


            //Compara si el rayo esta golpeando con el vertice variable y prosigue con esa logica
            if (hit1.transform.gameObject.CompareTag("VerticeVariable"))
            {
                //Debug.Log("Estoy en vertice variable");
                //Si esta en el vertice varibale es porque no esta el en fijo, entonces desactivamos esa variable
                ativarVerticeFijo = false;

                //Dibuja el tramo 1 del rayo indirecto apuntando al vertice varibale 
                Debug.DrawRay(transform.position, dirVerticeVarible, Color.yellow);

                //Dibuja el tramo 2 del rayo hacia el usuario siempre que este no toque a un obstaculo
                if (!hit2.transform.gameObject.CompareTag("Obstaculo"))
                {
                    Datos.d3d1 = disInd1 = dirVerticeVarible.magnitude;
                    Datos.d3d2 = disInd2 = dirTargetIndirecto.magnitude;
                    Datos.angle2 = 180 * Mathf.Asin(Mathf.Abs(dirTargetIndirecto.y) / disInd2) / Mathf.PI;
                    Debug.DrawRay(hit1.point, dirTargetIndirecto, Color.red);
                }
                else
                {
                    distanciaIndirecta = 0;

                }
            }

            Physics.Raycast(hit1.transform.position, dirTargetIndirecto, out hit2, Mathf.Infinity);

            //Compara si el rayo esta golpeando con el vertice fijo y prosigue con esa logica
            if (ativarVerticeFijo)
            {
                //Direccion hacia el vertice fijo
                dirVerticeFijo = verticeFijo.position - transform.position;

                //Dibuja el tramo 1 del rayo indirecto apuntando al vertice fijo 
                Debug.DrawRay(transform.position, dirVerticeFijo, Color.yellow);

                //Dibuja el rayo del tramo 2
                dirTargetIndirecto = target.position - verticeFijo.position;

                //Se genera nuevamente el rayo desde el punto fijo hacia el usuario, porque el otro  rayo ya no esta tocando el punto del usuario
                Physics.Raycast(verticeFijo.position, dirTargetIndirecto, out hit2, Mathf.Infinity);

                //Dibuja el tramo 2 del rayo hacia el usuario siempre que este no toque a un obstaculo
                if (!hit2.transform.gameObject.CompareTag("Obstaculo"))
                {
                    Datos.d3d2 = dirTargetIndirecto.magnitude;
                    Datos.angle2 = 180 * Mathf.Asin(Mathf.Abs(dirTargetIndirecto.y) / Datos.d3d2) / Mathf.PI;
                    Debug.DrawRay(verticeFijo.position, dirTargetIndirecto, Color.red);
                }
                else
                {
                    distanciaIndirecta = 0;
                }
            }

            Datos.d3dInd = Datos.d3d1 + Datos.d3d2;
        }
    }
    #endregion



}
