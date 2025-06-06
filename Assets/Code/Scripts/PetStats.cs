
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
                stats[i].fillImage.fillAmount = 1 - (float) (stats[i].value / 100f);
                PlayerPrefs.SetFloat(stat, stats[i].value);

                if(stats[i].value > 100)
                {
                    stats[i].value = 100;
                    PlayerPrefs.SetFloat(stat, 100);
                }
                if (stats[i].value <0)
                {
                    stats[i].value = 0;
                    PlayerPrefs.SetFloat(stat, 0);
                }

            }
        }
    }

    public void FelicidadBoton(float value)
    {
        ChangeStat("Felicidad", value);
    }

    private void Start()
    {

        for (int i = 0; i < stats.Count; i++)
        {
            stats[i].value = PlayerPrefs.GetFloat(stats[i].name, 20);
            stats[i].fillImage.fillAmount = (float)stats[i].value / 100;

        }
    }
}

[Serializable]
public class PetStat
{
    public string name;
    public float value;
    public Image fillImage;
}
