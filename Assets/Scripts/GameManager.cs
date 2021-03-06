using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class GameManager : MonoBehaviour {
    public int m_NumeroCanicas = 5;
    public int m_LanzamientoNumero = 0;
    //deberia tener algunos delay
    public Text m_Score;
    public Text m_WinText;
    public Slider m_ForceSlider;

    public GameObject m_ObjetivoPrefab;
    public GameObject m_PlayerPrefab;//esta deberia ser una referencia al prefab, y un jugador manager, para que cunete los que entran y salen, este es una bola
    //este es un prefab jugaddor
    public GameObject m_Player;//esta es la instancia de una bola//este es el jugador, no la pelota
    public Rigidbody m_CanicaPlayer;//instancia de la canica del jugador
    public Collider m_GameZone;
    public Transform m_SpawnPosition;
    private int m_Puntos = 0;
    public Transform[] m_Objetivos;
    public PlayerThrow m_PlayerThrow;//este se asigna en el inspector
    public void Awake(){

    }

    public void Start(){
        //SpawnPlayer();//por ahora ejecutara otra funcion, de asociar scripts, o quiza ya esten asociados
        m_PlayerThrow.m_Fuerza = m_ForceSlider;
        SpawnObjectives();
    }
    public void SpawnPlayer(){
        //m_Player = Instantiate(m_PlayerPrefab, m_SpawnPosition.position, m_SpawnPosition.rotation) as GameObject;
        //m_Player.GetComponent<PlayerAim>().m_CenterGameZone = m_GameZone.GetComponent<Transform>();
        //m_Player.GetComponent<PlayerAim>().m_SpawnPoint = m_SpawnPosition;
        m_Player.GetComponent<PlayerThrow>().m_Fuerza = m_ForceSlider;
        //NuevoLanzamiento();
        /*m_Player.GetComponent<PlayerThrow>().Setup();//quiza al instanciarse, solo deberia geenrar su propia bola
        m_CanicaPlayer = m_Player.GetComponent<PlayerThrow>().m_CanicaPlayer.GetComponent<Rigidbody>();//debieria haber una mejor forma de acceder a esta canica, quiza obtener la referencia a travez de una funcion de playerthrow
        */
    }

    public void SpawnObjectives(){//almacenados
        m_Objetivos = new Transform [m_NumeroCanicas];//o quiza geerar esto al comienxo
        for(int i = 0; i < m_Objetivos.Length; i++){//deberi usar m_Obejtivos.Length
            GameObject obj = Instantiate(m_ObjetivoPrefab, posicionValida(), Quaternion.identity) as GameObject;//
            m_Objetivos[i] = obj.transform;//no lo estaba haciendo
        }        
    }

    private Vector3 posicionValida(){
        Vector3 res;
        Transform posicion = Instantiate(m_SpawnPosition, new Vector3 (0f, 0.5f, 0f), Quaternion.identity) as Transform;//donde probare las posiciion generada, este es una clon del objeto trasnsform
        //no es aconsejable usar el transform de este gamebject GameManager, falla
        posicion.position = new Vector3 (0f, 0.5f, Random.Range(0f, 8f));//podira mezclasr la anterior
        posicion.RotateAround(transform.position, Vector3.up, Random.Range(0f, 360f));//obtener defrente la rotacion*/
        while(!EsValido(posicion)){
            posicion.position = new Vector3 (0f, 0.5f, Random.Range(0f, 8f));//podira mezclasr la anterior
            posicion.RotateAround(transform.position, Vector3.up, Random.Range(0f, 360f));//obtener defrente la rotacion*/
        }
        res = posicion.position;
        Destroy(posicion.gameObject);
        return res;
    }

    private bool EsValido(Transform posicion){
        bool result = true;
        for(int i = 0; i < m_Objetivos.Length; i++){//odira reducir un if aqui a dentro, si recibiera el i desde el ques se llamo en el spawnobjectives
            if(m_Objetivos[i]){
                result = result && Vector3.Distance(posicion.position, m_Objetivos[i].position) >= 1f;
                /*if(Vector3.Distance(posicion.position, m_Objetivos[i].position) >= 1f){//aun que si tuviera un if para que retorne defrente la funcion no pasaria por todas siempre, no se de que forma es mejor
                }*/
            }
        }
        return result;
    }

    private void NuevoLanzamiento(){
        m_PlayerThrow.Setup();//quiza al instanciarse, solo deberia geenrar su propia bola
        //m_CanicaPlayer = m_PlayerThrow.m_CanicaPlayer.GetComponent<Rigidbody>();//debieria haber una mejor forma de acceder a esta canica, quiza obtener la referencia a travez de una funcion de playerthrow
    }
    void OnTriggerExit(Collider other){
        //cuando todas las canicas se detengan, el turno finalizo
        GameObject m_canica = other.gameObject; //GameObject m_canica = other.GetComponent<GameObject>();
        if(m_canica.layer == LayerMask.NameToLayer("Objetivo")){//tengo que revisar que sea un objetivo, para sumar, y ver si es un jugador para no sumar, en ambos casos la bola se elimna}
            m_Puntos++;//esto esta bien para los objetivos
            Destroy(other.gameObject, 1f);//para que desaparezcan dos segundo despues, ahora el problema es que cuando destruyo una, no la he quitado del array
            SetTextScore();//otr opcion es solo llamar cuando haya modicificacion
        }
        if(m_Puntos == m_NumeroCanicas)
            m_WinText.color = Color.white;//aqui debo finalizar el juego
    }
    public void SetTextScore(){
        string s = "Puntos: " + m_Puntos + "\nLanzamientos: " + m_LanzamientoNumero;
        m_Score.text = s;
    }
    //debo almacenar todos los rigidbody de todas las canicas, cuando en
    public void FixedUpdate(){
        //aqui debo verificar que todas las pelotas esten quietas para dar por finalizado el turno
        //tambien deberia comprobar que mi cnica haya sido disparada para incrementar el lnuemro lanzamiento
        bool finalizoLanzamiento = m_PlayerThrow.m_Throwed;//estaba entrue, deberia poner si el script lanzar ha lanzado
        //if(m_CanicaPlayer){//en lugar de usar canica player, tambien debo usar los scripts
        if(m_PlayerThrow.m_CanicaPlayer){//en lugar de usar canica player, tambien debo usar los scripts
        //si exite esta canica, es por que fue disparada, no necesito hacer la comprobacion en CanicaPlayer, ese script al final quedo solo para aplicar algunas fuerzas
            finalizoLanzamiento = finalizoLanzamiento && (m_PlayerThrow.m_CanicaPlayer.GetComponent<Rigidbody>().IsSleeping() && m_PlayerThrow.m_CanicaPlayer.GetComponent<CanicaPlayer>().m_Fired);//di la calinca no se mueve, y ya fue disparada,entoces debe finalizar el alnzamineto
        //finalizoLanzamiento = finalizoLanzamiento && m_CanicaPlayer.IsSleeping();//di la calinca no se mueve, y ya fue disparada,entoces debe finalizar el alnzamineto
        //no se si sea buena idea quitar el fired por lo del issleepong, en un pequeño instante, antes de aplicar la fuerza
        }
        //por ahora evito erroes con el if:while
        //print("Jugador: " + finalizoLanzamiento);

        for(int i = 0; i < m_Objetivos.Length; i++){
            if(m_Objetivos[i]){//este IsSleeping, por que creo que nunca la la velocidad e la poelota entra en el rango minimo que estableci, para la canica funciona bien, pero para los objtivos aprece que no
                finalizoLanzamiento = finalizoLanzamiento && m_Objetivos[i].GetComponent<Rigidbody>().IsSleeping();//si esta quito, retorna verdadero, si se mueve falso,
                //print("Objetivo " + i + ": " + m_Objetivos[i].GetComponent<Rigidbody>().IsSleeping());
            //laidea es ir comprobanto que todo este quieto, si alguno no esta quieto, finalizoLanzamineto deberia terminar en falso
            }
        }
        if(finalizoLanzamiento){
            print("Finalizo Lanzamiento");
            Destroy(m_PlayerThrow.m_CanicaPlayer.gameObject, 1f);//para que desaparezcan dos segundo despues//esto funciona en Colliders no en Rigidbody por lo visto

            /*m_Player.GetComponent<PlayerThrow>().Setup();//quiza no deberia reinicar la camara, o tener dos funcines, una para reinicar balon, y otra para reiniciar posicion
            m_CanicaPlayer = m_Player.GetComponent<PlayerThrow>().m_CanicaPlayer.GetComponent<Rigidbody>();//debieria haber una mejor forma de acceder a esta canica, quiza obtener la referencia a travez de una funcion de playerthrow
            */
            NuevoLanzamiento();//en realida no prepara uno nuevo, solo regresa al estado inical
            //estas dos lineas siempre van juntas, deberia ponerlas dentro d una funcion
            m_LanzamientoNumero++;//este incremento no ha funcionado
            SetTextScore();
            //como ya finalizo el lanzamiento, toca un cambio de turno, pero or ahora solo le dare una nueva pelota al jugadro
        }//revisar las logicas, a veces no entra en esta cosa
    }
}   
//para este juego ya no instancion nada solo lo controlo, siempre estara instancionado el primer jugador, es decir el del cañon
//por ahora quien tendra los scripts del jugador sera el segundo marcador, si no fucniona bien, por eso de tracker y no, entonces creare unbjeto jugadro dentro de este, que tendra dentro del modelo del tanque y los scripts necesarios