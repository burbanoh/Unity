using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Modelo3GPP : MonoBehaviour {

    #region Variables
    public Text lineaTxt;

    private float hE = 1.0f;
    private float hBS = 10.5f;
    private float h_BS = 9.50f;
    private float h_UT = 0.5f;


    private float PLlos3GPP28ghz;
    private float PNLlos3GPP28ghz;

    private float PLlos3GPP73ghz;
    private float PNLlos3GPP73ghz;


    private float PL_UMA_LOS_28;
    private float PL_UMA_LOS_73;
    private float PL;


    float a, b;

    private float d2d, d3d;
    private float hm;
    private float Plos, dBP_28, dBP_73;

    float PL_28, PL_73;
    #endregion

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            Handle_Modelo3GPP();
        }
    }

    void Handle_Modelo3GPP()
    {
        d3d = Datos.d3d;
        d2d = Datos.d2d;

        a = Random.value;
        b = Random.value;

        //73 GHz
        dBP_73 = 1773.33f;

        //28 GHz
        dBP_28 = 4623.33f;

        if (d2d <= 18)
        {
            Plos = 1.0f;
        }
        else
        {
            Plos = (18 / d2d + Mathf.Exp(-d2d / 63) * (1 - (18 / d2d)));
        }

        if (Plos > a)
        {
            lineaTxt.text = ("Tiene linea de vista");
            float PL1_28 = 28.0f + 22 * Mathf.Log10(d3d) + 20 * Mathf.Log10(28);
            float PL1_73 = 28.0f + 22 * Mathf.Log10(d3d) + 20 * Mathf.Log10(73);

            float PL2_28 = 28.0f + 40 * Mathf.Log10(d3d) + 20 * Mathf.Log10(28) - 9 * Mathf.Log10(Mathf.Pow((dBP_28), 2) + Mathf.Pow((hBS - h_UT),2));
            float PL2_73 = 28.0f + 40 * Mathf.Log10(d3d) + 20 * Mathf.Log10(73) - 9 * Mathf.Log10(Mathf.Pow((dBP_73), 2) + Mathf.Pow((hBS - h_UT),2));

            if (10 < d2d && d2d< dBP_28)
            {
                PL_UMA_LOS_28 = PL1_28;
            }else if (dBP_28 < d2d && d2d < 5000)
            {
                PL_UMA_LOS_28 = PL2_28;
            }

            if (10 < d2d && d2d < dBP_73)
            {
                PL_UMA_LOS_28 = PL1_73;
            }
            else if (dBP_28 < d2d && d2d < 5000)
            {
                PL_UMA_LOS_28 = PL2_73;
            }

        }
        else
        {
            lineaTxt.text = ("No tiene linea de vista");
            PL_28 = 32.4f + 20 * Mathf.Log10(28) + 30 * Mathf.Log10(d3d);
            PL_73 = 32.4f + 20 * Mathf.Log10(73) + 30 * Mathf.Log10(d3d);
        }

        PLlos3GPP28ghz = PL_UMA_LOS_28 + 4 * b;
        PNLlos3GPP28ghz = PL_28 + 7.8f * b;

        PLlos3GPP73ghz = PL_UMA_LOS_73 + 4f * b;
        PNLlos3GPP73ghz = PL_73 + 7.8f * b;

        Datos.LOS_3GPP_28 = 65 - PLlos3GPP28ghz;
        Datos.NLOS_3GPP_28 = 65 - PNLlos3GPP28ghz;

        Datos.LOS_3GPP_73 = 74 - PLlos3GPP73ghz;
        Datos.NLOS_3GPP_73 = 74 - PNLlos3GPP73ghz;
    }
}
