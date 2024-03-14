using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
public class HandVibration : MonoBehaviour
{
    public AudioSource alarmeParede;
    public AudioSource alarmePorta;
    public AudioSource alarmeObjetoGeral;
    public AudioSource alarmeObjetoMedioBaixo;
    public AudioSource alarmeObjetoAlto;
    public Rigidbody rb;
    private bool isColliding=false;
    public XRBaseController controller;
    public FeedbackManager fbmanager;

    bool isUniqueSoundActive = false;
    bool lastSecondaryButtonState = false;
    InputDevice xrInputDevice;

    void Update()
    {   
        if (isColliding){
            print("Esta tocando");
            controller.SendHapticImpulse(0.9f, 10);
        } else
        {
            print("Parou de tocar");
        }

        //Alteração de som
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
                alarmeParede.Play();
            }else{
                alarmeObjetoGeral.Play();
            }
            controller.SendHapticImpulse(0.9f, 1);
        } 
        if (collision.gameObject.CompareTag("porta"))
        {   
            isColliding=true;

            if (!isUniqueSoundActive){
                alarmePorta.Play();
            }else{
                alarmeObjetoGeral.Play();
            }
            controller.SendHapticImpulse(0.9f, 1);
        }
        if (collision.gameObject.CompareTag("objetoMedioBaixo"))
        {   
            isColliding=true;
            
            if (!isUniqueSoundActive){
                alarmeObjetoMedioBaixo.Play();
            }else{
                alarmeObjetoGeral.Play();
            }
            controller.SendHapticImpulse(0.9f, 1);
        } 
        if (collision.gameObject.CompareTag("objetoAlto"))
        {   
            isColliding=true;
            
            if (!isUniqueSoundActive){
                alarmeObjetoAlto.Play();
            }else{
                alarmeObjetoGeral.Play();
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
                alarmeParede.Stop();
            }else{
                alarmeObjetoGeral.Stop();
            }
        }
        if (collision.gameObject.CompareTag("porta"))
        {   
            isColliding=false;
            controller.SendHapticImpulse(0, 0);
            
            if (!isUniqueSoundActive){
                alarmePorta.Stop();
            }else{
                alarmeObjetoGeral.Stop();
            }
        }
        if (collision.gameObject.CompareTag("objetoMedioBaixo"))
        {   
            isColliding=false;
            controller.SendHapticImpulse(0, 0);
            
            if (!isUniqueSoundActive){
                alarmeObjetoMedioBaixo.Stop();
            }else{
                alarmeObjetoGeral.Stop();
            }
        }
        if (collision.gameObject.CompareTag("objetoAlto"))
        {   
            isColliding=false;
            controller.SendHapticImpulse(0, 0);
            
            if (!isUniqueSoundActive){
                alarmeObjetoAlto.Stop();
            }else{
                alarmeObjetoGeral.Stop();
            }
        }
        if (collision.gameObject.CompareTag("chao"))
        {   
            controller.SendHapticImpulse(0, 0);
        }
    }

    bool GetSecondaryButtonValue()
    {
        List<InputDevice> xrInputDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, xrInputDevices);
        xrInputDevice = xrInputDevices[0];
    
        bool buttonValue;

        return (xrInputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out buttonValue) && buttonValue);
    }
}

