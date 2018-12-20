using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DatosEnPantalla : MonoBehaviour {

    [Header("Distancias")]
    public Text distanciaIndir;
    public Text anguloDir;
    public Text anguloIndir;
    public Text d3d;
    public Text d2d;
    public Text d3d1;
    public Text d3d2;

    [Header("Datos ITU")]
    public Text LOS_ITU_28;
    public Text NLOS_ITU_28;
    public Text LOS_ITU_73;
    public Text NLOS_ITU_73;
    public Text LineaTxt;

    [Header("Datos 3GPP")]
    public Text LOS_3GPP_28;
    public Text NLOS_3GPP_28;
    public Text LOS_3GPP_73;
    public Text NLOS_3GPP_73;

    [Header("Datos METIS")]
    public Text LOS_METIS_28;
    public Text NLOS_METIS_28;
    public Text LOS_METIS_73;
    public Text NLOS_METIS_73;


    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        #region Distancias
        distanciaIndir.text = Datos.d3dInd.ToString("N2") + "m";
        anguloDir.text = Datos.angle1.ToString("N2") + "°";
        anguloIndir.text = Datos.angle2.ToString("N2") + "°";

        d3d.text = Datos.d3d.ToString("N2") + "m";
        d2d.text = Datos.d2d.ToString("N2") + "m";

        d3d1.text = Datos.d3d1.ToString("N2") + "m";
        d3d2.text = Datos.d3d2.ToString("N2") + "m";
        #endregion

        #region ITU
        LOS_ITU_28.text = Datos.LOS_ITU_28.ToString("N2");
        NLOS_ITU_28.text = Datos.NLOS_ITU_28.ToString("N2");

        LOS_ITU_73.text = Datos.LOS_ITU_73.ToString("N2");
        NLOS_ITU_73.text = Datos.NLOS_ITU_73.ToString("N2");
        #endregion


        #region 3GPP
        LOS_3GPP_28.text = Datos.LOS_3GPP_28.ToString("N2");
        NLOS_3GPP_28.text = Datos.NLOS_3GPP_28.ToString("N2");

        LOS_3GPP_73.text = Datos.LOS_3GPP_73.ToString("N2");
        NLOS_3GPP_73.text = Datos.NLOS_3GPP_73.ToString("N2");
        #endregion

        #region METIS
        LOS_METIS_28.text = Datos.LOS_METIS_28.ToString("N2");
        NLOS_METIS_28.text = Datos.NLOS_METIS_28.ToString("N2");

        LOS_METIS_73.text = Datos.LOS_METIS_73.ToString("N2");
        NLOS_METIS_73.text = Datos.NLOS_METIS_73.ToString("N2");
        #endregion




    }
}
