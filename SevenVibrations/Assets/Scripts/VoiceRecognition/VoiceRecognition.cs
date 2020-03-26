using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;

public class VoiceRecognition : MonoBehaviour
{
    [Tooltip("Add a specific Game Object Here that is going to be manipulated by voice")]
    public GameObject objectToManipulate;
    public GameObject illumination;
    [Tooltip("This is the starting color you want it to start at")]
    public Material startingColor;
    [Tooltip("This is the ending color you want after the funtion has been activated")]
    public Material endingColor;

    public float startingLerpColorTime;
    public float maxStartingColorTime;
    public float colorToLerp;

    [Tooltip("A random number that is counting down for the player to find the object")]
    public float countDown;
    [Tooltip("This is the smallest amount of time for the player to find the object")]
    public float minRange;
    [Tooltip("This is the largetst amount of time for the player to find the object")]
    public float maxRange;

    public string phrase;

    public GameObject text;

    public GameObject field;

    private KeywordRecognizer keywordRecognizer;

    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    private void Start()
    {
        text.SetActive(false);

        actions.Add(phrase, Glow);
        //actions.Add("Nam Myoho Renge Kyo", NMRK);
        //actions.Add("南無妙法蓮華教", NMRK);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += WordSaid;

        illumination.SetActive(false);
        field.SetActive(true);
    }

    private void WordSaid(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void Glow()
    {
        Debug.Log(phrase);
        objectToManipulate.gameObject.GetComponent<Renderer>().material = endingColor;
        illumination.SetActive(true);
        field.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            text.SetActive(true);
            Debug.Log("Keyword Recognizer has started");
            keywordRecognizer.Start();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        text.SetActive(false);
        Debug.Log("Exiting The trigger");
        keywordRecognizer.Stop();
    }

}
