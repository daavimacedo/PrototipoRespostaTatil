using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisaoCorpo : MonoBehaviour
{
    public AudioSource somColisor;
    public AudioSource somSucesso;
    public Rigidbody rb;
    //public XRBaseController controller;

    void Update()
    {   
          
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("parede"))
        {   
            somColisor.Play();
            //controller.SendHapticImpulse(0.9f, 1);
        } 
        if (collision.gameObject.CompareTag("colisor"))
        {   
            somColisor.Play();
            //controller.SendHapticImpulse(0.9f, 1);
        } 
        if (collision.gameObject.CompareTag("pilastra"))
        {   
            somColisor.Play();
            //controller.SendHapticImpulse(0.9f, 1);
        } 
        if (collision.gameObject.CompareTag("porta"))
        {   
            somSucesso.Play();
            //controller.SendHapticImpulse(0.9f, 1);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("parede"))
        {   
            //controller.SendHapticImpulse(0, 0);
            somColisor.Stop();

        }
        if (collision.gameObject.CompareTag("colisor"))
        {   
            //controller.SendHapticImpulse(0, 0);
            somColisor.Stop();

        }
        if (collision.gameObject.CompareTag("pilastra"))
        {   
            somColisor.Stop();
            //controller.SendHapticImpulse(0.9f, 1);
        } 
        if (collision.gameObject.CompareTag("porta"))
        {   
            //controller.SendHapticImpulse(0, 0);
            //somSucesso.Stop();
        }
    }
}
