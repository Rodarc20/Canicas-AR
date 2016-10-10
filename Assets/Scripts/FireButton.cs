using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class FireButton : MonoBehaviour, IVirtualButtonEventHandler {
    public GameObject buttonObject;
    public Text text;
    public void Start(){
        //podria buscar aqui el boton, oo puedo asignarlo a travez del inspctor
        buttonObject = GameObject.Find("FireButton");
        buttonObject.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
    }
    
    public void OnButtonPressed(VirtualButtonAbstractBehaviour vab){
        text.color = Color.white;
        print("Boton Presionado");
    }
    public void OnButtonReleased(VirtualButtonAbstractBehaviour vab){
        text.color = Color.black;
        print("Boton Liberado");
    }
}
//este script deberia tener la opcion de deshabilitar o habilitar el boton, si es que estoy en una zona en la que puedo lanzar
//funciona pero hay que corregir los otrs scripts
//este script debe tener un booleano que para qeu puesa ser consultado desde afuera, o podria modificar un booleano externo en cada cambio, para asi poder hacer lo mismo que haci antes para llenar la barra de fuerza
//evaluar las idstintas tecnicas