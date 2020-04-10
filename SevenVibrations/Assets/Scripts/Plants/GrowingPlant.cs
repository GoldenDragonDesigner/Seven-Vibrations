using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingPlant : MonoBehaviour
{
    //[Tooltip("This is just for reference")]
    private float countDownTime;

    [Tooltip("This is just the time counting down")]
    public float timeCountingDown;

    //[Tooltip("This is the bool as to whether or not the plant can grow")]
    private bool timeToGrow;

    [Tooltip("This is the bool for when the plant is done growing")]
    public bool doneGrowing;

    [Tooltip("Enter the min amount of time the plant has to wait to grow")]
    public float minGrowthRange;

    [Tooltip("Enter the max amount of time the plant has to wait to grow")]
    public float maxGrowthRange;

    //[Tooltip("What state is the plant in now")]
    private GlobalVariables.PlantStates plantStates;

    [Tooltip("Add the Game object parts here")]
    public GameObject[] growingPlant;


    public int plantPartsNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        timeToGrow = false;

        doneGrowing = false;

        timeCountingDown = countDownTime = Random.Range(maxGrowthRange, maxGrowthRange);

        plantStates = GlobalVariables.PlantStates.Growing;

        //plantPartsNum = 0;

        foreach(GameObject part in growingPlant)
        {
            part.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlantState();
    }

    void PlantState()
    {
        switch (plantStates)
        {
            case GlobalVariables.PlantStates.Growing:
                PlantGrowing();
                Debug.Log("Plant is growing");
                break;
            case GlobalVariables.PlantStates.DoneGrowing:
                DoneGrowing();
                Debug.Log("Done growing");
                break;
        }
    }

    void PlantGrowing()
    {
        if(countDownTime <= 0)
        {
            timeToGrow = true;
            if(timeToGrow == true)
            {
                Growing();
                countDownTime = countDownTime = Random.Range(maxGrowthRange, maxGrowthRange);
            }
        }
        else
        {
            timeToGrow = false;
            countDownTime -= Time.deltaTime;
        }
    }

    public bool Growing()
    {
        //Debug.Log("The number of Plants are: " + growingPlant.Length);
        //int i = plantPartsNum;

        plantPartsNum = 0;
        
        foreach(GameObject plant in growingPlant)
        {
            //Debug.Log("Plant count: " + i);
            //plantPartsNum++;
            plantPartsNum++;
            if (!plant.activeSelf)
            {
                plant.SetActive(true);
                timeToGrow = false;
                return false;
            }
        }
        doneGrowing = true;

        plantStates = GlobalVariables.PlantStates.DoneGrowing;
        return true;
    }

    void DoneGrowing()
    {
        if (doneGrowing)
        {
            timeCountingDown = 0;
        }
    }
}
