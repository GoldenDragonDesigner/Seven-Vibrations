using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;

public class VoiceRecognition : MonoBehaviour
{
    [Tooltip("The float for filling the vibration meter")]
    public float curVibration;

    public float maxVibration;
    
    public string firstPhrase;

    //public string secondPhrase;

    //public string thirdPhrase;

    //public string fourthPhrase;

    public bool useMic;

    public Slider vibrationMeter;

    private KeywordRecognizer keywordRecognizer;

    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    private void Awake()
    {
        vibrationMeter = GameObject.Find("VibrationMeter").GetComponent<Slider>();
    }

    private void Start()
    {
        Debug.Log("Setting the mic to false");
        useMic = false;

        vibrationMeter.value = CalculatingVibration();

        actions.Add(firstPhrase, Nam);
        //actions.Add(secondPhrase, Myoho);
        //actions.Add(thirdPhrase, Renge);
        //actions.Add(fourthPhrase, Kyo);

        //actions.Add("Nam Myoho Renge Kyo", NMRK);
        //actions.Add("南無妙法蓮華教", NMRK);
        //南無妙法蓮華經

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += WordSaid;
    }

    private void Update()
    {
        vibrationMeter.value = CalculatingVibration();
    }

    private void WordSaid(PhraseRecognizedEventArgs speech)
    {
        //Neal holding ago Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void Nam()
    {
        if(useMic == true)
        curVibration += 1;
        Debug.Log(firstPhrase + " " + curVibration);
    }

    //private void Myoho()
    //{
    //    if (useMic == true)
    //        curVibration += 1;
    //    Debug.Log(secondPhrase + " " + curVibration);
    //}

    //private void Renge()
    //{
    //    if (useMic == true)
    //        curVibration += 1;
    //    Debug.Log(thirdPhrase + " " + curVibration);
    //}

    //private void Kyo()
    //{
    //    if (useMic == true)
    //        curVibration += 1;
    //    Debug.Log(fourthPhrase + " " + curVibration);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Setting the mic to true");
            useMic = true;
            Debug.Log("Keyword Recognizer has started");
            keywordRecognizer.Start();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Setting the mic back to false");
            useMic = false;
            Debug.Log("Exiting The trigger");
            keywordRecognizer.Stop();
        }
    }

    public float CalculatingVibration()
    {
        return (curVibration / maxVibration);
    }
}
