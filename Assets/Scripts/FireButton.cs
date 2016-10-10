using UnityEngine;
using Vuforia;

public class FireButton : MonoBehaviour, IVirtualButtonEventHandler {
    public GameObject buttonObject;
    public void Start(){
        //podria buscar aqui el boton, oo puedo asignarlo a travez del inspctor
        buttonObject = GameObject.Find("FireButton");
        buttonObject.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
    }
    
    public void OnButtonPressed(VirtualButtonAbstractBehaviour vab){
        print("Boton Presionado");
    }
    public void OnButtonReleased(VirtualButtonAbstractBehaviour vab){
        print("Boton Liberado");
    }
}
//este script deberia tener la opcion de deshabilitar o habilitar el boton, si es que estoy en una zona en la que puedo lanzar
//funciona pero hay que corregir los otrs scripts