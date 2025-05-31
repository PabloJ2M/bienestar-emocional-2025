
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetStats : MonoBehaviour
{
    [Header("Principales")]


    [SerializeField] List<PetStat> stats = new List<PetStat>();

    public void ChangeStat(string stat, float amount)
    {
        for (int i = 0; i < stats.Count; i++)
        {
            if (stats[i].name == stat)
            {
                stats[i].value += amount;
                stats[i].fillImage.fillAmount = (float) stats[i].value / 100;

            }
        }
    }

    private void Start()
    {
        ChangeStat("Felicidad", -50);
    }
}

[Serializable]
public class PetStat
{
    public string name;
    public float value;
    public Image fillImage;
}
