using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HandVibration : MonoBehaviour
{
 
    //Variáveis para produzir as respostas tatéis 
    public Rigidbody rb; //Corpo do objeto principal - O controle e todos seus filhos
    private bool isColliding=false;
    
    //Objeto de onde sairá o som
    [SerializeField]
    private AudioSource pontaBengala;
    [SerializeField]
    private AudioSource mao;
    [SerializeField]
    private AudioSource geral;

    //Sons 
    
    [SerializeField]
    private AudioClip somParede;
    [SerializeField]
    private AudioClip somPilastra;
    [SerializeField]
    private AudioClip somLixeira;
    [SerializeField]
    private AudioClip somPorta;
    [SerializeField]
    private AudioClip somGeral;

    //Variáveis para troca de modos sonoros
    public XRBaseController controller;
    bool isUniqueSoundActive = true;
    bool lastSecondaryButtonState = false;

    //public FeedbackManager fbmanager;

  

    InputDevice xrInputDevice;
    void Start()
    {
        geral.clip=somGeral;
    }

    void Update()
    {   //Manter a vibração
        if (isColliding){
            print("Esta tocando");
            controller.SendHapticImpulse(0.9f, 10);
        } else
        {
            print("Parou de tocar");
        }

        //Alteração do modo de som
        bool secondaryButtonValue = GetSecondaryButtonValue();

        if (secondaryButtonValue && !lastSecondaryButtonState)
        {
            isUniqueSoundActive = !isUniqueSoundActive; // Inverte o estado do item
        }

        lastSecondaryButtonState = secondaryButtonValue;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("parede"))
        {   
            //fbmanager.NotifyEvent("parede");
            isColliding=true;

            if (!isUniqueSoundActive){
                if (IsChildOfObjectParent(collision)) 
                {
                    // Colisão com o objeto filho - pontaBengala
                    pontaBengala.clip=somParede;
                    pontaBengala.Play();
                }
                else
                {
                    // Colisão com o objeto pai - mao
                    mao.clip=somParede;
                    mao.Play();
                }
            }else{
                geral.Play();
            }
            controller.SendHapticImpulse(0.9f, 1);
        } 
        if (collision.gameObject.CompareTag("porta"))
        {   
            isColliding=true;
            if (IsChildOfObjectParent(collision)) 
            {
                pontaBengala.clip=somPorta;
                pontaBengala.Stop();
            }
            else
            {
                mao.clip=somPorta;
                mao.Stop();
            }
            controller.SendHapticImpulse(0.9f, 1);
        }
        if (collision.gameObject.CompareTag("lixeira"))
        {   
            isColliding=true;
            
            if (!isUniqueSoundActive){
                if (IsChildOfObjectParent(collision)) 
                {
                    pontaBengala.clip=somLixeira;
                    pontaBengala.Play();
                }
                else
                {
                    mao.clip=somLixeira;
                    mao.Play();
                } 
            }else{
                geral.Play();
            }
            controller.SendHapticImpulse(0.9f, 1);
        } 
        if (collision.gameObject.CompareTag("pilastra"))
        {   
            isColliding=true;
            
            if (!isUniqueSoundActive){
                if (IsChildOfObjectParent(collision)) 
                {
                    pontaBengala.clip=somPilastra;
                    pontaBengala.Play();
                }
                else
                {
                    mao.clip=somPilastra;
                    mao.Play();
                } 
            }else{
                geral.Play();
            }
            controller.SendHapticImpulse(0.9f, 1);
        }
        if (collision.gameObject.CompareTag("chao"))
        {   
            controller.SendHapticImpulse(0.5f, 0.5f);
            
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("parede"))
        {   
            //fbmanager.RemoveEvent("parede");
            isColliding=false;
            controller.SendHapticImpulse(0, 0);
            
            if (!isUniqueSoundActive){
                /*
                if (IsChildOfObjectParent(collision)) 
                {
                    // Colisão com o objeto filho
                    pontaBengala.Stop();
                }
                else
                {
                    // Colisão com o objeto pai
                    mao.Stop();
                } 
                */
            }else{
                geral.Stop();
            }
        }
        if (collision.gameObject.CompareTag("porta"))
        {   
            isColliding=false;
            controller.SendHapticImpulse(0, 0);
            /*
            if (IsChildOfObjectParent(collision)) 
            {
                pontaBengala.Stop();
            }
            else
            {
                mao.Stop();
            } 
            */ 
        }
        if (collision.gameObject.CompareTag("lixeira"))
        {   
            isColliding=false;
            controller.SendHapticImpulse(0, 0);
            
            if (!isUniqueSoundActive){
                /*
                if (IsChildOfObjectParent(collision)) 
                {
                    pontaBengala.Stop();
                }
                else
                {
                    mao.Stop();
                } 
                */
            }else{
                geral.Stop();
            }
        }
        if (collision.gameObject.CompareTag("pilastra"))
        {   
            isColliding=false;
            controller.SendHapticImpulse(0, 0);
            
            if (!isUniqueSoundActive){
                /*
                if (IsChildOfObjectParent(collision)) 
                {
                    pontaBengala.Stop();
                }
                else
                {
                    mao.Stop();
                } 
                */
            }else{
                geral.Stop();
            }
        }
        if (collision.gameObject.CompareTag("chao"))
        {   
            controller.SendHapticImpulse(0, 0);
        }
    }

    //Método para identificar que um objeto é filho de outro objeto pai
    private bool IsChildOfObjectParent(Collision collision)
    {
        Transform parent = collision.transform.parent;
        while (parent != null)
        {
            if (parent == transform)
                return true;
            parent = parent.parent;
        }
        return false;
    }

    //Método para pegar imput do botão secundário (B)
    bool GetSecondaryButtonValue()
    {
        List<InputDevice> xrInputDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, xrInputDevices);
        xrInputDevice = xrInputDevices[0];
    
        bool buttonValue;

        return (xrInputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out buttonValue) && buttonValue);
    }
}

