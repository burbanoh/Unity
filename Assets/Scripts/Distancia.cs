using UnityEngine;
using System.Collections;
using System;

public class Distancia : MonoBehaviour {
    public Transform antena;
    public Transform persona;
    float distancia;
    float anguloElevacion;

    float distanciaZ;
    float distanciaX;
    float distanciaY;
    float azimut;

    double p;
    float factor1;
    double factor2;
    double factor3;

    float PL_los;
    float fc = 73000000;
    float d1;
    double dbpp; //d'bp
    double dbp;
    float hutp; //h'ut -> altura user terminal
    float hut;
    float hbsp; //h'bs -> altura base station
    float hbs;
    float PL_Nlos;
    float wcalle;
    float hedificio;
    float h_hbs_div;


    void Update()
    {
        distancia = Vector3.Distance(antena.position, transform.position);
        distanciaX = antena.transform.position.x - transform.position.x;
        distanciaZ = antena.transform.position.z - transform.position.z;
        distanciaY = antena.transform.position.y - transform.position.y;         //36m

        azimut = Mathf.Atan(distanciaZ / distanciaX);

        //anguloElevacion = Vector3.Angle(antena.position, transform.position);    
        anguloElevacion = Mathf.Asin(distanciaY / distancia);

        //**************************Operaciones ITU**************************************************************

        //Obtener el primer factor Plos UMa ITU
        // factor1 = 0; //the minimum value
        if (18 / distancia < 1){
            factor1 = 18 / distancia;
        }
        else{
            factor1 = 1;
        }
        //Obtener el segundo factor Plos UMa ITU
        factor2 = 1 - System.Math.Exp(-distancia / 63);

        //Obtener el tercer factor Plos UMa ITU
        factor3 = System.Math.Exp(-distancia / 63);

        //Resultado LoS probability UMa ITU: Plos:
        p = factor1 * factor2 + factor3;

        dbpp = 0.000000056 * fc;
        dbp = 2 * Math.PI * 0.00000004267 * fc;
        d1 = distancia * Mathf.Cos(anguloElevacion);

        // ver fc=28Ghz, 34Ghz, 73Ghz , hut=1.5 , hbs=25m

        // Path loss LoS : PL_los
        if (p < 0.5) {
            if (distancia > 10 & distancia < dbpp){
                PL_los = 22 * Mathf.Log10(distancia) + 28 + 20 * Mathf.Log10(fc);
            }else if (distancia > dbpp & distancia < 5000){
                hbsp = 24F; // hbs-1 
                hutp = 0.5F; // hut -1
                PL_los = 40F * Mathf.Log10(d1) + 7.8F - 18F * Mathf.Log10(hbsp) - 18F * Mathf.Log10(hutp) + 2F * Mathf.Log10(fc);
            }
        }

        // Path no loss NLoS : PL_NLlos //ver Wpromedio=20m es variable, hpromedio=24m es variable
        else if (p > 0.5 & p < 1){
            wcalle = 20f; //w = ancho de la calle 20m promedio
            hedificio = 24f; //h = altura del edificio 24m promedio
            h_hbs_div = 0.96f; // division= h/hbs = 24/25 VER¡¿
            hbs = 25f; // altura base station
            hut = 1.5f; // altura user terminal deacuerdo al documento
            PL_Nlos = 161.04f - (7.1f * Mathf.Log10((float)wcalle)) + (7.5f * Mathf.Log10((float)hedificio))
                - ((24.37f - 3.7f * Mathf.Pow((float) h_hbs_div, 2)) * Mathf.Log10((float) hbs))
                + (43.42f - 3.1f * Mathf.Log10(hbs)) * (Mathf.Log10(distancia) - 3.0F)
                + (20 * Mathf.Log10((float) fc)) 
               - (3.2f * (Mathf.Log10(11.75f * (float) hut)) * (Mathf.Log10(11.75f * (float) hut)) - 4.97f);
         
        }
    }

    void OnGUI()
    {
        GUI.color = Color.yellow;
        GUI.Box(new Rect(980, 380, 300, 240), "Modelo :");
        GUI.backgroundColor = Color.black;
        GUI.Label(new Rect(1000, 390, 1000, 30), "---------------------------------------------------------------");

        GUI.Label(new Rect(1000, 425, 1000, 30), "d3d = " + distancia.ToString() + "[m] :  " + distanciaX.ToString() + "[m]   " + distanciaY.ToString() + "[m]   " + distanciaZ.ToString() + "[m]   ");
        GUI.Label(new Rect(1000, 445, 1000, 30), "anguloElevacion = " + anguloElevacion.ToString() + " [°]");
        GUI.Label(new Rect(1000, 465, 1000, 30), "azimut = " + azimut.ToString() + " [°]");
        GUI.Label(new Rect(1000, 485, 1000, 30), "Plos = " + p.ToString());
        GUI.Label(new Rect(1000, 500, 1000, 30), "PL_los = " + PL_los.ToString());

    }
}