using UnityEngine;
using System.Collections;

public class MedirLR : MonoBehaviour {

    public Transform antena;
    public Transform persona;
    float anguloElevacion; 
    double LR;
    float Lfs;
    double Lrts;
    double Lmsd;
    Vector3 PosicionAntena;
    Vector3 PosicionPersona;
    float lambda;
    float R;
    float x; // para que Lrts salga sin errores esto debería ser float o poner ese PI en numeros
	float y;
    float ds;
    float distanciaZ;
    float distanciaX;
    float distanciaY;
    float distancia;

    // Update is called once per frame
    void Update()
    {

        distancia = Vector3.Distance(antena.position, transform.position);
        distanciaX = antena.transform.position.x - transform.position.x;
        distanciaZ = antena.transform.position.z - transform.position.z;
        distanciaY = antena.transform.position.y - transform.position.y;         //36m //-000
        anguloElevacion = Mathf.Asin(distanciaY / distancia);
        
        Lfs = -10* Mathf.Log10(lambda * Mathf.Pow(4 * Mathf.PI * R, -1));

        x = 0.5f - Mathf.Pow(Mathf.PI, -1) * Mathf.Atan((Mathf.Sign(anguloElevacion)) * y);
        y = Mathf.Pow(Mathf.Pow(Mathf.PI,3)/4/lambda*distancia*(1-Mathf.Cos(anguloElevacion)), 1/2); // ver porque no acepta 0.5
        Lrts = -20*Mathf.Log10(x);

        ds = lambda * Mathf.Pow(Mathf.Pow(distanciaX,2) + Mathf.Pow(distanciaZ,2), 1 /2);
        
        


            if (Lrts + Lmsd > 0)
            {
                LR = Lfs + Lrts + Lmsd;
            }
            else
            {
                LR = Lfs;
            }
                
    }
}


// float perimeter = 2 * Mathf.PI * radius;

// Lfs = Debug.Log(Mathf.Log(lambda/(4*Mathf.PI*R), 10))// para el PI;
// Pow(float f, float p) devuelve f a la potencia de p;
//  Lfs = Mathf.Log10(Mathf.Pow(Mathf.Log(lambda / (4 * Mathf.PI * R))),2);