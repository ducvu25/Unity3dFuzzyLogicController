using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] float distanceMap = 75;
    [SerializeField] int indexCheck = 1;

    [SerializeField] Transform transPlayer;
    // Start is called before the first frame update
    void Start()
    {
        InitMap(0);
        InitMap(1);
        InitMap(2);
    }
    void InitMap(int i)
    {
        Transform trans = transform.GetChild(i).GetChild(1);
        int n = trans.childCount;
        for (int j = 0; j < n; j++)
        {
            int n2 = trans.GetChild(j).childCount;
            for (int k = 0; k < n2; k++)
            {
                trans.GetChild(j).GetChild(k).gameObject.SetActive(false);
            }
            if(Random.value < 0.4f)
            {
                trans.GetChild(j).GetChild(Random.Range(0, n2)).gameObject.SetActive(true);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(transPlayer.position.z > transform.GetChild(indexCheck).position.z)
        {
            int indexPre = (indexCheck + 2)%3;
            indexCheck = (indexCheck + 1)%3;
            //Debug.Log(indexPre + " " + indexCheck);
            transform.GetChild(indexPre).position = transform.GetChild(indexCheck).position + new Vector3(0, 0, distanceMap);
            InitMap(indexPre);
        }
    }
}
