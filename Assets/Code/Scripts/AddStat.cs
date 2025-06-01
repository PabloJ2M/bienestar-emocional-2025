using UnityEngine;
using UnityEngine.SceneManagement;

public class AddStat : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float felicidad, alimento, limpieza, salud;
    [SerializeField] string stat;
    [SerializeField] float maxValueAdded;
    float valueAdded;
    float startValue;
    [SerializeField] SceneLoader[] scenes;
    [SerializeField] float[] sceneLimits;

    
    
    void Start()
    {
        felicidad = PlayerPrefs.GetFloat("Felicidad", 100);
        alimento = PlayerPrefs.GetFloat("Alimento", 100);
        limpieza = PlayerPrefs.GetFloat("Limpieza", 100);
        salud = PlayerPrefs.GetFloat("Salud", 100);

        startValue = PlayerPrefs.GetFloat(stat);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StatChange(float value)
    {
        switch (stat)
        {
            case "Felicidad":
                felicidad += value; break;
            case "Alimento":
                alimento += value; break;
            case "Limpieza":
                limpieza += value; break;
            case "Salud":
                    salud += value; break;
            default: Debug.Log("Error, stat no existe"); break;
        }
        valueAdded += value;

        //if(valueAdded >= maxValueAdded)
        //{
        //    ReturnHome();
        //}

        ReturnHome();
    }

    public void ReturnHome()
    {
        PlayerPrefs.SetFloat("Felicidad", felicidad);
        PlayerPrefs.SetFloat("Alimento", alimento);
        PlayerPrefs.SetFloat("Limpieza", limpieza);
        PlayerPrefs.SetFloat("Salud", salud);

        if(felicidad >= startValue)
        {
            for (int i = 0; i < 4; i++)
            {
                if (felicidad >= sceneLimits[i] && alimento >= sceneLimits[i] && limpieza >= sceneLimits[i] && salud >= sceneLimits[i])
                {
                    scenes[i].SwipeScene();
                }

            }
        }
    }
}
