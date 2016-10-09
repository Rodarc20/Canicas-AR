using UnityEngine;

public class GroundControler : MonoBehaviour {
    public void OnTriggerEnter(Collider other){//cambiar esto a Stay hace que sea muy pesado
        GameObject canica = other.gameObject;
        if(canica.layer == LayerMask.NameToLayer("Jugador")){
            CanicaPlayer canicaPlayer = canica.GetComponent<CanicaPlayer>();
            if(canicaPlayer.m_Fired){
                //Rigidbody canicaRigidbody = canica.GetComponent<Rigidbody>();
                canicaPlayer.m_Desaceleracion = 1f;
            }
        } 
        if(canica.layer == LayerMask.NameToLayer("Objetivo")){
            CanicaObjetivo canicaObjetivo = canica.GetComponent<CanicaObjetivo>();
            canicaObjetivo.m_Desaceleracion = 1f;
            //Rigidbody canicaRigidbody = canica.GetComponent<Rigidbody>();
            //los objetivos tambien deben tener un script para poider agregar alguna desaceleracion
        }
    }
    //debo almacenar todos las canicas que ingresen, sean de jugador o objetivo, y pasarles la desaceleracion
    public void OnTriggerExit(Collider other){//para devolver las desaceleraciones a su lugar
        GameObject canica = other.gameObject;
        if(canica.layer == LayerMask.NameToLayer("Jugador")){
            CanicaPlayer canicaPlayer = canica.GetComponent<CanicaPlayer>();
            if(canicaPlayer.m_Fired){
                //Rigidbody canicaRigidbody = canica.GetComponent<Rigidbody>();
                canicaPlayer.m_Desaceleracion = 0f;
            }
        } 
        if(canica.layer == LayerMask.NameToLayer("Objetivo")){
            CanicaObjetivo canicaObjetivo = canica.GetComponent<CanicaObjetivo>();
            canicaObjetivo.m_Desaceleracion = 0f;
            //Rigidbody canicaRigidbody = canica.GetComponent<Rigidbody>();
        }
    }
}
//otra alternativa es que esta calse tenga su funcion fixed update, y todos las canicas que entren se les aplicadesaceleracion en su moviemineto, el problema es qperderia control en la condicion para poner a 0 el movimiento
//o tal vez no
