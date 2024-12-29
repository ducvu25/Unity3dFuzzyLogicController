using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiShow : MonoBehaviour
{
    int n = 100;
    public Vector3 pStartDrawKc = new Vector3(0, 0, 0);
    public float doPhongDaiKc = 2;
    public float valueStartKc = -10;
    public float valueEndKc = 10;
    public List<BienNgonNgu> listKc = new List<BienNgonNgu>();

    public Vector3 pStartDrawVt = new Vector3(0, 0, 0);
    public float doPhongDaiVt = 0.1f;
    public float valueStartVt = -100;
    public float valueEndVt = 100;
    public List<BienNgonNgu> listVt = new List<BienNgonNgu>();

    public Vector3 pStartDrawAngle = new Vector3(0, 0, 0);
    public float doPhongDaiAngle = 0.5f;
    public float valueStartAngle = -90;
    public float valueEndAngle = 90;
    public List<BienNgonNgu> listAngle = new List<BienNgonNgu>();

    public bool isSetValue = false;
    public float doPhongDai = 2;

    [Range(-10, 10)]
    public float vDistance = 20;

    [Range(-100, 100)]
    public float vSpeed = 10;


    public CarController transPlayer;
    public CarAI transAI;

    public TextMeshProUGUI txtKc;
    public TextMeshProUGUI txtV;
    public TextMeshProUGUI txtAngle;
    public Slider sliderSpeed;


    public float distancePlayer;
    void Start()
    {
        distancePlayer = transPlayer.transform.position.z - transAI.transform.position.z;
        isSetValue = false;
        listKc.Add(new BienNgonNgu("LTN", GetTriangle(valueStartKc, valueEndKc, -15, -10, -5)));
        listKc.Add(new BienNgonNgu("LT", GetTriangle(valueStartKc, valueEndKc, -9, -5, -1)));
        listKc.Add(new BienNgonNgu("CB", GetTriangle(valueStartKc, valueEndKc, -5, 0, 5)));
        listKc.Add(new BienNgonNgu("LP", GetTriangle(valueStartKc, valueEndKc, 1, 5, 9)));
        listKc.Add(new BienNgonNgu("LPN", GetTriangle(valueStartKc, valueEndKc, 5, 10, 15)));

        listVt.Add(new BienNgonNgu("RatAm", GetTriangle(valueStartVt, valueEndVt, -150, -100, -50)));
        listVt.Add(new BienNgonNgu("Am", GetTriangle(valueStartVt, valueEndVt, -100, -50, 0)));
        listVt.Add(new BienNgonNgu("Khong", GetTriangle(valueStartVt, valueEndVt, -50, 0, 50)));
        listVt.Add(new BienNgonNgu("Duong", GetTriangle(valueStartVt, valueEndVt, 0, 50, 100)));
        listVt.Add(new BienNgonNgu("RatDuong", GetTriangle(valueStartVt, valueEndVt, 50, 100, 150)));

        listAngle.Add(new BienNgonNgu("TraiNhanh", GetTriangle(valueStartAngle, valueEndAngle, -135, -90, -45)));
        listAngle.Add(new BienNgonNgu("Trai", GetTriangle(valueStartAngle, valueEndAngle, -90, -45, 0)));
        listAngle.Add(new BienNgonNgu("CanBang", GetTriangle(valueStartAngle, valueEndAngle, -45, 0, 45)));
        listAngle.Add(new BienNgonNgu("Phai", GetTriangle(valueStartAngle, valueEndAngle, 0, 45, 90)));
        listAngle.Add(new BienNgonNgu("PhaiNhanh", GetTriangle(valueStartAngle, valueEndAngle, 45, 90, 135)));

        isSetValue = true;
    }

    private void Update()
    {
        if (!isSetValue) return;

        // Vẽ đường cho listKc
        for (int i = 0; i < listKc.Count; i++)
        {
            DrawTriangles(listKc[i].listValue, colorLine[i], pStartDrawKc, doPhongDaiKc);
            int index = GetIndexX(listKc[i].listValue, vDistance);
            if (index != -1)
            {
                Vector3 point = pStartDrawKc + new Vector3(listKc[i].listValue[index].x * doPhongDaiKc, listKc[i].listValue[index].nguy * doPhongDai, 0);
                Debug.DrawLine(point + Vector3.up * 0.2f, point - Vector3.up * 0.2f, Color.white); // Vẽ đường thẳng chỉ điểm
            }
        }

        // Vẽ đường cho listVt
        for (int i = 0; i < listVt.Count; i++)
        {
            DrawTriangles(listVt[i].listValue, colorLine[i], pStartDrawVt, doPhongDaiVt);
            int index = GetIndexX(listVt[i].listValue, vSpeed);
            if (index != -1)
            {
                Vector3 point = pStartDrawVt + new Vector3(listVt[i].listValue[index].x * doPhongDaiVt, listVt[i].listValue[index].nguy * doPhongDai, 0);
                Debug.DrawLine(point + Vector3.up * 0.2f, point - Vector3.up * 0.2f, Color.white); // Vẽ đường thẳng chỉ điểm
            }
        }

        for (int i = 0; i < listAngle.Count; i++)
        {
            DrawTriangles(listAngle[i].listValue, colorLine[i], pStartDrawAngle, doPhongDaiAngle);
        }
        Caculater();
    }

    private void DrawTriangles(List<ToHop> triangle, Color color, Vector3 pStart, float k)
    {
        for (int i = 0; i < triangle.Count - 1; i++)
        {
            Vector3 start = pStart + new Vector3(triangle[i].x * k, triangle[i].nguy * doPhongDai, 0);
            Vector3 end = pStart + new Vector3(triangle[i + 1].x * k, triangle[i + 1].nguy * doPhongDai, 0);
            Debug.DrawLine(start, end, color); // Vẽ đường thẳng
        }
    }

    private List<ToHop> GetTriangle(float vS, float vE, float anpha, float beta, float lamda)
    {
        List<ToHop> result = new List<ToHop>();
        float range = vE - vS;
        float dis = range / n;
        for (int i = 0; i < n; i++)
        {
            float x = vS + i * dis;
            float y = 0;
            if (x >= anpha && x < beta)
            {
                y = (x - anpha) / (beta - anpha);
            }
            else if (x >= beta && x < lamda)
            {
                y = (lamda - x) / (lamda - beta);
            }
            result.Add(new ToHop(x, y));
        }
        return result;
    }

    public int GetIndexX(List<ToHop> list, float x)
    {
        int index = -1;
        for (int i = 0; i < n; i++)
        {
            if (index == -1 || (Mathf.Abs(list[i].x - x) < Mathf.Abs(list[index].x - x)))
            {
                index = i;
            }
        }
        return index;
    }

    public int GetIndexY(List<ToHop> list, float y)
    {
        int index = -1;
        for (int i = 0; i < n; i++)
        {
            if (index == -1 || (Mathf.Abs(list[i].nguy - y) < Mathf.Abs(list[index].nguy - y)))
            {
                index = i;
            }
        }
        return index;
    }

    public Color[] colorLine = { Color.red, Color.blue, Color.green, Color.yellow, Color.magenta, Color.black };

    public List<float> nguyKc;
    public List<float> nguyVt;
    void Caculater()
    {
        vDistance = (transAI.transform.position.x - transPlayer.transform.position.x)*2;
        vSpeed = sliderSpeed.value*10;
        transPlayer.speed = sliderSpeed.value;
        transAI.speed = sliderSpeed.value*(transPlayer.transform.position.z - transAI.transform.position.z > distancePlayer ? 1.2f : 1);
        nguyKc = GetNguy(listKc, vDistance);
        nguyVt = GetNguy(listVt, vSpeed);
        //Debug.Log(nguyKc.Count + " " + nguyVt.Count);
        float traiNhanh = Mathf.Max(Mathf.Min(nguyKc[4], nguyVt[1]),
                                    Mathf.Min(nguyKc[4], nguyVt[2]),
                                    Mathf.Min(nguyKc[4], nguyVt[3]));

        float trai = Mathf.Max(Mathf.Min(nguyKc[3], nguyVt[0]),
                                Mathf.Min(nguyKc[3], nguyVt[1]),
                                Mathf.Min(nguyKc[3], nguyVt[2]),
                                Mathf.Min(nguyKc[3], nguyVt[3]),
                                Mathf.Min(nguyKc[3], nguyVt[4]),
                                Mathf.Min(nguyKc[4], nguyVt[0]),
                                Mathf.Min(nguyKc[4], nguyVt[4]));

        float canBang = Mathf.Max(Mathf.Min(nguyKc[2], nguyVt[0]),
                                Mathf.Min(nguyKc[2], nguyVt[1]),
                                Mathf.Min(nguyKc[2], nguyVt[2]),
                                Mathf.Min(nguyKc[2], nguyVt[3]),
                                Mathf.Min(nguyKc[2], nguyVt[4]));

        float phai = Mathf.Max(Mathf.Min(nguyKc[1], nguyVt[0]),
                                Mathf.Min(nguyKc[1], nguyVt[1]),
                                Mathf.Min(nguyKc[1], nguyVt[2]),
                                Mathf.Min(nguyKc[1], nguyVt[3]),
                                Mathf.Min(nguyKc[1], nguyVt[4]),
                                Mathf.Min(nguyKc[0], nguyVt[0]),
                                Mathf.Min(nguyKc[0], nguyVt[4]));

        float phaiNhanh = Mathf.Max(Mathf.Min(nguyKc[0], nguyVt[1]),
                                    Mathf.Min(nguyKc[0], nguyVt[2]),
                                    Mathf.Min(nguyKc[0], nguyVt[3]));
        //Debug.Log(phaiNhanh + " " + phai + " " + canBang + " " + trai + " " + traiNhanh);
        float tuSo = 0;
        float mauSo = 0;
        List<ToHop> result = new List<ToHop>();
        for (int i = 0; i < n; i++)
        {
            float tn = Mathf.Min(listAngle[0].listValue[i].nguy, traiNhanh);
            float t = Mathf.Min(listAngle[1].listValue[i].nguy, trai);
            float cb = Mathf.Min(listAngle[2].listValue[i].nguy, canBang);
            float p = Mathf.Min(listAngle[3].listValue[i].nguy, phai);
            float pn = Mathf.Min(listAngle[4].listValue[i].nguy, phaiNhanh);
            result.Add(new ToHop(listAngle[0].listValue[i].x, Mathf.Max(tn, t, cb, p, pn)));
            tuSo += result[i].nguy * result[i].x;
            mauSo += result[i].nguy;
        }
        float angle = tuSo / mauSo;
        //Debug.Log(tuSo / mauSo);
        transAI.transform.localRotation = Quaternion.Euler(0, angle, 0);
        DrawTriangles(result, colorLine[5], pStartDrawAngle, doPhongDaiAngle);
        txtAngle.text = "Góc xoay: " + angle.ToString("F2") + " độ";
        txtKc.text = "Khoảng cách: " + vDistance.ToString("F2") + " m";
        txtV.text = "Vận tốc: " + (vSpeed).ToString("F2") + " m/s";
    }
    List<float> GetNguy(List<BienNgonNgu> list, float x) {
        List<float> result = new List<float>();
        for (int i = 0; i < list.Count; i++) {
            int indexX = GetIndexX(list[i].listValue, x);   
            float y = list[i].listValue[indexX].nguy;
            result.Add(y);
        }
        return result;
    }
}

[System.Serializable]
public class BienNgonNgu
{
    public string name;
    public List<ToHop> listValue;

    public BienNgonNgu()
    {
        name = "";
        listValue = new List<ToHop>();
    }

    public BienNgonNgu(string name, List<ToHop> listValue)
    {
        this.name = name;
        this.listValue = listValue;
    }
}

[System.Serializable]
public class ToHop
{
    public float x;
    public float nguy;

    public ToHop()
    {
        x = 0;
        nguy = 0;
    }

    public ToHop(float x, float nguy)
    {
        this.x = x;
        this.nguy = nguy;
    }
}