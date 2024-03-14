using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class AtivacaoBengalaEsquerda : MonoBehaviour
{
    public GameObject bengala;
    public XRBaseController controller;
    bool isItemActive = false;
    bool lastPrimaryButtonState = false;
    InputDevice xrInputDevice;
    public AudioSource somAtivacaoBengala;
    public AudioSource somDesativacaoBengala;

    void Start()
    {
        bengala.SetActive(false);
    }
    
    void Update()
    {
        bool primaryButtonValue = GetPrimaryButtonValue();

        if (primaryButtonValue && !lastPrimaryButtonState)
        {
            isItemActive = !isItemActive; // Inverte o estado do item

            if(isItemActive){
                somAtivacaoBengala.Play();
            }else{
                somDesativacaoBengala.Play();
            }
            bengala.SetActive(isItemActive);
        }

        lastPrimaryButtonState = primaryButtonValue;
    }

    bool GetPrimaryButtonValue()
    {
        List<InputDevice> xrInputDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, xrInputDevices);
        xrInputDevice = xrInputDevices[0];
    
        bool buttonValue;

        return (xrInputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out buttonValue) && buttonValue);
    }
}
