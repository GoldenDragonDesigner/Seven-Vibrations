using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;

public class VibrationMeterScript : MonoBehaviour
{
    public bool useMicrophone;
    public float vibration;
    public string phrase;
    KeywordRecognizer keywordRecognizer;
    float timer = 0;
    public bool keywordOn = true;

    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    public Slider vibrationMeter;


    void Start()
    {
        actions.Add(phrase, Vibrate);
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += WordSaid;
    }

    
    void Update()
    {
        timer -= Time.deltaTime;
        timer = Mathf.Clamp(timer, 0, 5);

        if(useMicrophone && keywordOn && timer == 0)
        {
            keywordOn = false;
            keywordRecognizer.Start();
            Debug.Log("Recognizer on");
        }

        while (timer != 0)
        {
            vibration += .03f;
            timer = Mathf.Clamp(vibration, 0, 100);
            Debug.Log(vibration);
        }
    }

    private void WordSaid(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    void Vibrate()
    {
        timer = 2;
        //keywordOn = true;
        Debug.Log("Vibrating");
    }

    private void OnTriggerEnter(Collider other)
    {
        useMicrophone = true;
        Debug.Log("entered trigger volume");
        keywordRecognizer.Start();
    }

    private void OnTriggerExit(Collider other)
    {
        useMicrophone = false;
        keywordRecognizer.Stop();
        Debug.Log("left trigger volume");
    }
}
