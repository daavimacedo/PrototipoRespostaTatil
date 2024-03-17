using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class OcultarVisao : MonoBehaviour
{
    public GameObject esfera;
    bool esferaAtiva;
    bool ultimoStatusBotaoPrimarioDireito = false;
    InputDevice xrInputDevice;
    

    void Start()
    {
     esfera.SetActive(true);
    }
    
    void Update()
    {
        bool botaoPrimarioDireitoValor = GetPrimaryButtonValue();

        if (botaoPrimarioDireitoValor && !ultimoStatusBotaoPrimarioDireito)
        {
            esferaAtiva = !esferaAtiva; // Inverte o estado do item
            esfera.SetActive(esferaAtiva);
        }

        ultimoStatusBotaoPrimarioDireito = botaoPrimarioDireitoValor;
    }

    bool GetPrimaryButtonValue()
    {
        List<InputDevice> xrInputDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, xrInputDevices);
        xrInputDevice = xrInputDevices[0];
    
        bool buttonValue;

        return (xrInputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out buttonValue) && buttonValue);
    }
}
