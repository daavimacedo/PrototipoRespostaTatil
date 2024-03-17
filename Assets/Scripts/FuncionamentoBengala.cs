using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class FuncionamentoBengala : MonoBehaviour
{
    //Objetos bengala
    public GameObject bengalaDireita;
    public GameObject bengalaEsquerda;

    //Controle
    InputDevice xrInputDevice;

    //Status dos objetos
    bool bengalaDireitaAtiva = false;
    bool bengalaEsquerdaAtiva = false;

    //Variáveis de memória
    bool ultimoStatusBotaoGatilhoDireito = false;
    bool ultimoStatusBotaoGatilhoEsquerdo = false;
    
    //Sons
    public AudioSource somAtivacaoBengalaDireita;
    public AudioSource somDesativacaoBengalaDireita;
    public AudioSource somAtivacaoBengalaEsquerda;
    public AudioSource somDesativacaoBengalaEsquerda;

    void Start()
    {
        bengalaDireita.SetActive(false);
        bengalaEsquerda.SetActive(false);
    }
    
    void Update()
    {
        bool botaoGatilhoDireitoValor = GetTriggerButtonValueRight();
        bool botaoGatilhoEsquerdoValor = GetTriggerButtonValueLeft();

        if (botaoGatilhoDireitoValor && !ultimoStatusBotaoGatilhoDireito) //Se o status anterior é diferente do atual
        {
            bengalaDireitaAtiva = !bengalaDireitaAtiva; // Inverte o estado do item

            if(bengalaDireitaAtiva){
                somAtivacaoBengalaDireita.Play();
                bengalaDireita.SetActive(true);
                if (bengalaEsquerdaAtiva){
                    bengalaEsquerda.SetActive(false);
                    bengalaEsquerdaAtiva = false;
                }
            }else{
                somDesativacaoBengalaDireita.Play();
                bengalaDireita.SetActive(false);
            }

        }
        ultimoStatusBotaoGatilhoDireito = botaoGatilhoDireitoValor; // Variável que guarda o valor do último botão direito

        if (botaoGatilhoEsquerdoValor && !ultimoStatusBotaoGatilhoEsquerdo) //Se o status anterior é diferente do atual
        {
            bengalaEsquerdaAtiva = !bengalaEsquerdaAtiva; // Inverte o estado do item


            if(bengalaEsquerdaAtiva){
                somAtivacaoBengalaEsquerda.Play();
                bengalaEsquerda.SetActive(true);
                if (bengalaDireitaAtiva){
                    bengalaDireita.SetActive(false);
                    bengalaDireitaAtiva = false;
                }
            }else{
                somDesativacaoBengalaEsquerda.Play();
                bengalaEsquerda.SetActive(false);
            }

        }
        ultimoStatusBotaoGatilhoEsquerdo = botaoGatilhoEsquerdoValor; // Variável que guarda o valor do último botão esquerdo
    }
    
    //Método para pegar o valor do botão de gatilho do botão direito
    bool GetTriggerButtonValueRight()
    {
        List<InputDevice> xrInputDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, xrInputDevices);
        xrInputDevice = xrInputDevices[0];
    
        bool buttonValue;

        return (xrInputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out buttonValue) && buttonValue);
    }
    
    //Método para pegar o valor do botão de gatilho do botão esquerdo
    bool GetTriggerButtonValueLeft()
    {
        List<InputDevice> xrInputDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, xrInputDevices);
        xrInputDevice = xrInputDevices[0];
    
        bool buttonValue;

        return (xrInputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out buttonValue) && buttonValue);
    }
}

