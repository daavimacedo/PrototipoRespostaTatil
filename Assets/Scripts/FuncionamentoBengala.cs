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
    public XRBaseController controller;
    InputDevice xrInputDevice;

    //Status dos objetos
    bool bengalaDireitaAtiva = false;
    bool bengalaEsquerdaAtiva = false;

    //Variáveis de memória
    bool ultimoStatusBotaoPrimarioDireito = false;
    bool ultimoStatusBotaoPrimarioEsquerdo = false;
    
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
        bool botaoPrimarioDireitoValor = GetPrimaryButtonValueRight();
        bool botaoPrimarioEsquerdoValor = GetPrimaryButtonValueLeft();

        if (botaoPrimarioDireitoValor && !ultimoStatusBotaoPrimarioDireito) //Se o status anterior é diferente do atual
        {
            bengalaDireitaAtiva = !bengalaDireitaAtiva; // Inverte o estado do item

            if(bengalaDireitaAtiva){
                somAtivacaoBengalaDireita.Play();
                bengalaDireita.SetActive(true);
                if (bengalaEsquerdaAtiva){
                    bengalaEsquerda.SetActive(false);
                }
            }else{
                somDesativacaoBengalaDireita.Play();
                bengalaDireita.SetActive(false);
            }

        }
        ultimoStatusBotaoPrimarioDireito = botaoPrimarioDireitoValor; // Variável que guarda o valor do último botão direito

        if (botaoPrimarioEsquerdoValor && !ultimoStatusBotaoPrimarioEsquerdo) //Se o status anterior é diferente do atual
        {
            bengalaEsquerdaAtiva = !bengalaEsquerdaAtiva; // Inverte o estado do item


            if(bengalaEsquerdaAtiva){
                somAtivacaoBengalaEsquerda.Play();
                bengalaEsquerda.SetActive(true);
                if (bengalaDireitaAtiva){
                    bengalaDireita.SetActive(false);
                }
            }else{
                somDesativacaoBengalaEsquerda.Play();
                bengalaEsquerda.SetActive(false);
            }

        }
        ultimoStatusBotaoPrimarioEsquerdo = botaoPrimarioEsquerdoValor; // Variável que guarda o valor do último botão esquerdo
    }
    //Método para pegar o valor do botão primário do botão direito
    bool GetPrimaryButtonValueRight()
    {
        List<InputDevice> xrInputDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, xrInputDevices);
        xrInputDevice = xrInputDevices[0];
    
        bool buttonValue;

        return (xrInputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out buttonValue) && buttonValue);
    }
    //Método para pegar o valor do botão primário do botão esquerdo
    bool GetPrimaryButtonValueLeft()
    {
        List<InputDevice> xrInputDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, xrInputDevices);
        xrInputDevice = xrInputDevices[0];
    
        bool buttonValue;

        return (xrInputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out buttonValue) && buttonValue);
    }
}
