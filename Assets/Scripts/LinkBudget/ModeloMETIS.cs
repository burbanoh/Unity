using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeloMETIS : MonoBehaviour {

    private float b = 15f;
    private float R; // este es d2d
    private float l = 8f;
    private float hr = 9; //altura del edificio
    private float kf_28, kf_73;
    private float ka, kd, kb;
    private float angle;

    private float Lsf_28, Lrts_28, Lmsd_28;
    private float Lsf_73, Lrts_73, Lmsd_73;
    private float Lbsh;
    private float LR_28, LR_73;

    private float QM_28, QM_73;

    private float PLlosMETIS28ghz;
    private float PLNlosMETIS28ghz;

    private float PLlosMETIS73ghz;
    private float PLNlosMETIS73ghz;

    private float d2d, d3d;
    private float hm;
    private float Plos, dBP_28, dBP_73;

    private float lambda_28 = 10.714f;
    private float lambda_73 = 4.110f;

    private float ds_28;
    private float ds_73;


    // Use this for initialization
    void Start () {
        kf_28 = 15 * ((28 / 925) - 1);
        kf_73 = 15 * ((73 / 925) - 1);
    }
	
	// Update is called once per frame
	void Update () {

        Handle_METIS();
		
	}

    void Handle_METIS()
    {
        R = Datos.d2d;
        angle = Datos.angle2;
        Lsf_28 = -10 * Mathf.Pow(Mathf.Log10(lambda_28/(4*Mathf.PI*R)), 2);
        Lsf_73 = -10 * Mathf.Pow(Mathf.Log10(lambda_73/(4*Mathf.PI*R)), 2);


        Lrts_28 = -20 * Mathf.Log10(0.5f - Mathf.Atan(Mathf.Sin(angle) * Mathf.Sqrt( Mathf.Pow(Mathf.PI, 3) / (4 * lambda_28) * Datos.d3d2 * (1 - Mathf.Cos(angle)))) / Mathf.PI);
        Lrts_73 = -20 * Mathf.Log10(0.5f - Mathf.Atan(Mathf.Sin(angle) * Mathf.Sqrt( Mathf.Pow(Mathf.PI, 3) / (4 * lambda_73) * Datos.d3d2 * (1 - Mathf.Cos(angle)))) / Mathf.PI);

        ds_28 = lambda_28 / Mathf.Pow(Datos.dhb,2);
        ds_73 = lambda_73 / Mathf.Pow(Datos.dhb,2);

        if(l > ds_28)
        {
            if(Datos.hbs > hr)
            {
                ka = 54.0f;
                kd = 18;
                Lbsh = -18 * Mathf.Log10(1 + Datos.dhb);

            }else if(Datos.hbs <= hr && R > 500)
            {
                ka = 54 - 0.8f * Datos.dhb;

            }else if(Datos.hbs <= hr && R < 500)
            {
                ka = 54 - 1.6f * Datos.dhb * R / 1000;
            }

            if (Datos.hbs <= hr)
            {
                kd = 18 - 15 * (Datos.dhb/hr);
                Lbsh = 0;
            }

            if(Datos.hbs < hr)
            {
                QM_28 = 2.35f * Mathf.Pow(((Datos.dhb / R) * Mathf.Sqrt(b / lambda_28)), 0.9f);
                QM_73 = 2.35f * Mathf.Pow(((Datos.dhb / R) * Mathf.Sqrt(b / lambda_73)), 0.9f);
            }
            else if(Datos.hbs > hr)
            {
                QM_28 = b / R;
                QM_73 = b / R;
            }
            else
            {
                float theta = Mathf.Atan(Datos.dhb / b);
                float ro = Mathf.Sqrt(Mathf.Pow(Datos.dhb, 2) + Mathf.Pow(b, 2));
                QM_28 = (b / (2 * Mathf.PI * R) * Mathf.Sqrt(lambda_28 / ro) * (1/theta - 1/(2 * Mathf.PI + theta)));
                QM_73 = (b / (2 * Mathf.PI * R) * Mathf.Sqrt(lambda_73 / ro) * (1 / theta - 1 / (2 * Mathf.PI + theta)));
            }

        }

        if (l > ds_28)
        {
            Lmsd_28 = Lbsh + ka + kb + kd * Mathf.Log10(R / 1000) + kf_28 * Mathf.Log10(28) - 9 * Mathf.Log10(b);
        }
        else
        {
            Lmsd_28 = -10 * Mathf.Log10(Mathf.Pow(QM_28, 2));
        }

        if (l > ds_73)
        {
            Lmsd_73 = Lbsh + ka + kb + kd * Mathf.Log10(R / 1000) + kf_73 * Mathf.Log10(73) - 9 * Mathf.Log10(b);
        }
        else
        {
            Lmsd_73 = -10 * Mathf.Log10(Mathf.Pow(QM_73, 2));
        }


        //Encontrar LR_28
        if( Lrts_28 + Lmsd_28 > 0)
        {
            LR_28 = Lsf_28 + Lrts_28 + Lmsd_28;
            
        }
        else
        {
            LR_28 = Lsf_28;
            
        }

        //Encontrar LR_73
        if (Lrts_73 + Lmsd_73 > 0)
        {
            LR_73 = Lsf_73 + Lrts_73 + Lmsd_73;
        }
        else
        {
            LR_73 = Lsf_73;
        }

        PLlosMETIS28ghz = LR_28;
        PLNlosMETIS28ghz = LR_28;

        PLlosMETIS73ghz = LR_73;
        PLNlosMETIS73ghz = LR_73;

        Datos.LOS_METIS_28 = 65 - PLlosMETIS28ghz;
        Datos.NLOS_METIS_28 = 65 - PLNlosMETIS28ghz;

        Datos.LOS_METIS_73 = 74 - PLlosMETIS73ghz;
        Datos.NLOS_METIS_73 = 74 - PLNlosMETIS73ghz;
    }
}
