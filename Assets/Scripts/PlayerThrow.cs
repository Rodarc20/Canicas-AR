using UnityEngine;
using UnityEngine.UI;//aqui hare modificaciones al ui, por ejemplo la barra de fuerza o marcador de llegada

public class PlayerThrow : MonoBehaviour {
    public int m_PlayerNumber = 1;
    public Slider m_Fuerza;
    public float m_MinForce = 0f;
    public float m_MaxForce = 100f;
    public float m_MaxChargeTime = 2f;//un segudno en cargar toda la barra de fuerza
    public GameObject m_CanicaPlayerPrefab;
    public GameObject m_CanicaPlayer;//instancia de una canica de jugador
    private CanicaPlayer m_ScriptCP;//este script 
    public Transform m_PuntoLanzaminento;//el lugar donde se instancian las canicas lanzadas
    public Transform m_DireccionLanzamiento;//esta direccion la proporciona el Transform del cañón, como son referencias no es necesario actualizar, solo usar

    private string m_ThrowButton;
    private float m_CurrentThrowForce;
    private float m_ChargeSpeed;
    public bool m_Throwed;

    void Start(){
        m_ThrowButton = "Jump";
        m_ChargeSpeed = (m_MaxForce - m_MinForce) / m_MaxChargeTime;
        //Setup()//por ahora lo llamo dentro de spawnPlayer en el gamemanager
    }
    private void OnEnable(){
        m_CurrentThrowForce = m_MinForce;
    }

    public void Setup(){//la  unica forma de vlver a falso es que el gamemanager llame a esta funcion, al finalizar el turno, quitar los otros que lo hagan falso
        m_Throwed = false;
        m_CurrentThrowForce = m_MinForce;
        /*m_CanicaPlayer = Instantiate(m_CanicaPlayerPrefab, m_PuntoLanzaminento.position, m_PuntoLanzaminento.rotation) as GameObject;
        if(m_CanicaPlayer){
            m_ScriptCP = m_CanicaPlayer.GetComponent<CanicaPlayer>();
        }*/
    }
    private void Update(){
        //si me paso del maximo de la barra no debo lanzar la canica, por que puede que el jugador aun quiera modificar la direccion, por ello podra aun moverse, solo se disparara cuando el jugador suelte la tecla de deisparo
        if(m_CurrentThrowForce >= m_MaxForce && !m_Throwed){//si la fuerza esa mayor que el maximo, y aun no he disparado, entonces solo establesco el current en el max
            m_CurrentThrowForce = m_MaxForce;//se dispara solo cuando el jugador suslete la tecla
            m_Fuerza.value = m_CurrentThrowForce;//hay problemas con este if,buscar solucion
        }
        else if(Input.GetButtonDown(m_ThrowButton) && !m_Throwed){//cuando presioo por primera vez el boton
            //m_Throwed = false;
            m_CurrentThrowForce = m_MinForce;
            m_Fuerza.value = m_CurrentThrowForce;
        }
        else if(Input.GetButton(m_ThrowButton) && !m_Throwed){//cuando mantendo presionado el boton pero aun no he disparado
            m_CurrentThrowForce += m_ChargeSpeed * Time.deltaTime;
            m_Fuerza.value = m_CurrentThrowForce;
            //aqui tambien van modificaciones la slider de la fuerz de lanzamiento
        }
        if(Input.GetButtonUp(m_ThrowButton) && !m_Throwed){//cuadno suelto el boton y aun no he disparado, eliminado el elseif
            //m_Throwed = true;
            Fire();
        }
        //Touch
        if(Input.touchCount > 0){
            if(m_CurrentThrowForce >= m_MaxForce && !m_Throwed){//si la fuerza esa mayor que el maximo, y aun no he disparado, entonces solo establesco el current en el max
                m_CurrentThrowForce = m_MaxForce;//se dispara solo cuando el jugador suslete la tecla
                m_Fuerza.value = m_CurrentThrowForce;//hay problemas con este if,buscar solucion
            }
            else if(Input.GetTouch(0).phase == TouchPhase.Began && !m_Throwed){
                m_CurrentThrowForce = m_MinForce;
                m_Fuerza.value = m_CurrentThrowForce;
            }
            else if(Input.GetTouch(0).phase != TouchPhase.Ended && !m_Throwed){
                m_CurrentThrowForce += m_ChargeSpeed * Time.deltaTime;
                m_Fuerza.value = m_CurrentThrowForce;
            }
            if(Input.GetTouch(0).phase == TouchPhase.Ended && !m_Throwed){
                Fire();
            }
        }
    }
    private void Fire(){
        m_CanicaPlayer = Instantiate(m_CanicaPlayerPrefab, m_PuntoLanzaminento.position, m_PuntoLanzaminento.rotation) as GameObject;
        if(m_CanicaPlayer){
            m_ScriptCP = m_CanicaPlayer.GetComponent<CanicaPlayer>();
        }
        m_Throwed = true;
        m_ScriptCP.Fire(m_DireccionLanzamiento.forward * m_CurrentThrowForce);//ninguna de las dos funciona, el proble es que en cada update lo regresa a la posicion del jugador, cuando no este disparando
    }//una ve z que se ha disparado, debo deshabilitar los controles, la pelota sigue por su cuenta
}
//este script tambien debe tener la referencia, para poder cambiar el parent de las canicas instanciadas
//quiza tenga que usar otro marcador, para saber si esta presionando, pensar en eso al momento de programar, este ssciprt solo debe consultar a lo muhco dos variables del boton