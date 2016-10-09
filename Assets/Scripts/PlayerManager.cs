using System;
using UnityEngine;

[Serializable]
public class PlayerManager {//no estoy usando esta cosa, seria util si usara varios jugadores
    public Color m_PlayerColor;
    public Transform SpawnPosition;
    [HideInInspector] public int m_PlayerNumber;
    [HideInInspector] public GameObject m_Player;//esta sera la instancia de un objeto jugador
    public int m_Lanzamientos;
    public int m_Objetivos;

    /*private PlayerAim m_Aim;
    private PlayerThrow m_Throw;//esto son para poder habilitar y deshabilitar el control una vez que se realizo un lanzamiento, aun que dberia hacerlo de forma iterna

    
    public void Setup(){
        //creaa canicas de lanzamineto
    }
    public void NewThrow(){
        //PlayerThrow.Setup();
    }
    public void EnableControl(){
        m_Aim.enabled = true;
        m_Throw.enabled = true;

    }
    public void DisableControl(){
        m_Aim.enabled = false;
        m_Throw.enabled = false;
    }*/
}