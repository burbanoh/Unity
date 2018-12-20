using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Datos : MonoBehaviour{

    public static float hbs = 10.5f;
    public static float dhb = 1.5f;
    public static float hm = 1.3f;
    public static float dHm = 4.7f;

    public static float d2d, d3d, d3dInd, d3d1, d3d2, angle1, angle2;

    public static float LOS_ITU_28, NLOS_ITU_28;
    public static float LOS_3GPP_28, NLOS_3GPP_28;
    public static float LOS_METIS_28, NLOS_METIS_28;

    public static float LOS_ITU_73, NLOS_ITU_73;
    public static float LOS_3GPP_73, NLOS_3GPP_73;
    public static float LOS_METIS_73, NLOS_METIS_73;

    void Update()
    {
        angle1 = Mathf.Cos(DegreeToRadian(angle1));
        angle2 = Mathf.Cos(DegreeToRadian(angle2));
        d2d = d3d * angle1;
    }


    float DegreeToRadian(float Degree)
    {
        return Mathf.PI / 180 * Degree;
    }

    float RadianToDegree(float Radian)
    {
        return 180 * Radian / Mathf.PI;
    }
}
