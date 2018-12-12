using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DatosEnPantalla : MonoBehaviour {

    public Text distanciaDir, distanciaIndir, anguloDir, anguloIndir;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        distanciaDir.text = GeneradorDeRayos.distanciaDirecta.ToString("N2") + "m";
        distanciaIndir.text = GeneradorDeRayos.distanciaIndirecta.ToString("N2") + "m";
        anguloDir.text = GeneradorDeRayos.anguloDirecto.ToString("N2") + "°";
        anguloIndir.text = GeneradorDeRayos.anguloIndirecto.ToString("N2") + "°";
    }
}
