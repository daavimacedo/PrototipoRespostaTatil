using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FeedbackManager : MonoBehaviour
{   
    public AudioSource alarmeParede;
    public XRBaseController controller;
    private List<string> eventList;

    // Start is called before the first frame update
    void Start()
    {
        eventList = new List<string>();
    }


    public void NotifyEvent(string evt)
    {
        eventList.Add(evt);
    }

    public void RemoveEvent(string evt)
    {
        eventList.Remove(evt);
    }

    // Update is called once per frame
    void Update()
    { /*
        if (eventList.Count != 0)
        {   /
            controller.SendHapticImpulse(0.9f, 1); 
            foreach(string evt in eventList)
            {   
                if(evt=="parede" && eventList.Contains(evt))
                {
                    alarmeParede.Play();  
                }else{
                    alarmeParede.Stop();
                }
                   
            }
        }else{
            controller.SendHapticImpulse(0, 0);
        }
    */
    }
}
