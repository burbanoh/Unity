using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeloITU : MonoBehaviour {

    #region Variables
    public Text lineaTxt;

    private float a, b;

    private float d2d, d3d;
    private float Plos;

    float PL_28, PL_73;
    #endregion



    #region BuilIn Methods
    // Use this for initialization
    private void Awake()
    {
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Handle_ModeloITU();
        if (Input.GetKeyDown("space"))
        {
            
        }
    }
    #endregion


    #region Custom Methods
    void Handle_ModeloITU()
    {
        d3d = Datos.d3d;
        d2d = Datos.d2d;

        a = Random.value;
        b = Random.value;

        if (d2d <= 18)
        {
            Plos = 1.0f;
        }
        else
        {
            Plos = (18 / d2d + Mathf.Exp(-d2d / 63) * (1 - (18 / d2d)));
        }

        Debug.Log("Plos: "+ Plos);
        

        if (Plos > a)
        {
            lineaTxt.text = ("Tiene linea de vista");
            PL_28 = 61.4f + 20 * Mathf.Log10(Datos.d3d);
            PL_73 = 69.59f + 28.6f * Mathf.Log(Datos.d3d);

        }
        else
        {
            lineaTxt.text =  ("No tiene linea de vista");
            if (d3d < 100)
            {
                PL_28 = 96.9f + 15.1f * Mathf.Log10(Datos.d3d);
            }
            else
            {
                PL_28 = 127.0f + 87.0f * Mathf.Log10(Datos.d3d / 100);
            }
            PL_73 = 69.59f + 36.7f * Mathf.Log(Datos.d3d);
        }

        Datos.LOS_ITU_28 = 65 - PL_28 + 4 * b;
        Datos.NLOS_ITU_28 = 65 - PL_28 + 7.8f * b;
        
        Datos.LOS_ITU_73 = 74 - PL_73 + 4 * b;
        Datos.NLOS_ITU_73 = 74 - PL_73 + 7.8f * b;

    }
    #endregion

}
