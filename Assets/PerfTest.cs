using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class PerfTest : MonoBehaviour
{
    public int PointsCount;
    private int OldPointsCount;
    private Vector3[] data;

    public bool DoFullSort;
    public Text PointsCountDisplay;
    public Text FramerateDisplay;
    public Toggle FullSortToggle;
    public Slider PointsSlider;

    void Start()
    {
        data = CreateRandomData();    
    }

    private Vector3[] CreateRandomData()
    {
        Vector3[] ret = new Vector3[PointsCount];
        for (int i = 0; i < PointsCount; i++)
        {
            ret[i] = new Vector3(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        }
        return ret;
    }

    void Update()
    {
        PointsCount = (int) Mathf.Pow(10, PointsSlider.value);
        UpdateDataSet();
        DoFullSort = FullSortToggle.isOn;
        DoSort();
        UpdateDisplay();
    }

    private void UpdateDataSet()
    {
        if(OldPointsCount != PointsCount)
        {
            data = CreateRandomData();
        }
        OldPointsCount = PointsCount;
    }

    private void UpdateDisplay()
    {
        PointsCountDisplay.text = "Evaluating " + PointsCount + " Points";
        FramerateDisplay.text = "Framerate: " + (int)(1 / Time.unscaledDeltaTime);
    }

    private void DoSort()
    {
        if(DoFullSort)
        {
            Vector3[] fullSort = data.OrderBy(item => item.magnitude).ToArray();
        }
        else
        {
            Vector3 leastMag = GetLeastMag();
        }
    }

    private Vector3 GetLeastMag()
    {
        Vector3 ret = Vector3.zero;
        float minMag = Mathf.Infinity;
        foreach (Vector3 datum in data)
        {
            if(minMag > datum.sqrMagnitude)
            {
                ret = datum;
                minMag = datum.sqrMagnitude;
            }
        }
        return ret;
    }
}
