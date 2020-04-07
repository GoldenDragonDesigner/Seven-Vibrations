using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationMeterScript : MonoBehaviour
{
    public bool useMicrophone;
    string microphone;
    float vibration;
    AudioSource adsource;
    float[] samples;


    void Start()
    {
        adsource = GetComponent<AudioSource>();

        if (Microphone.devices.Length > 0)
        {
            microphone = Microphone.devices[0].ToString();
        }
        else useMicrophone = false;
    }

    
    void Update()
    {
        Vibrate();
    }

    void Vibrate()
    {
        if (useMicrophone)
        {
            Debug.Log("recording");
           adsource.clip = Microphone.Start(microphone, true, 5, 44100);
            adsource.loop = true;
            while (!(Microphone.GetPosition(null) > 0))
            {
                adsource.Play();
                AudioListener.GetOutputData(samples, 0);
                Debug.Log(samples.ToString());
            }
        }
        else Microphone.End(microphone);

        foreach(float sample in samples)
        {
            if(sample > 0)
            {
                vibration += Time.deltaTime * 2;
                vibration = Mathf.Clamp(vibration, 0, 100);
                Debug.Log("Vibration is at " + vibration.ToString());
            }
        }
    }
}
