using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save : MonoBehaviour
{
    //Things we wanna save
    [SerializeField] public List<List<float>> playerPosition = new List<List<float>>();

    [SerializeField] public float health;

    public List<float> SavePosition(Vector3 position)
    {
        List<float> positionList = new List<float>();
        positionList.Add(position.x);
        positionList.Add(position.y);
        positionList.Add(position.z);

        return positionList;
    }

    public float SavePlayerHeath()
    {
        return health = GetComponent<PlayerHealth>().CalculatingHealth();
    }

    public Vector3 LoadPosition(List<float> floats)
    {
        Vector3 position = new Vector3(floats[0], floats[1], floats[2]);
        return position;
    }
}
