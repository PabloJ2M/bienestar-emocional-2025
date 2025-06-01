using System.Collections;
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
    [SerializeField] float endDelay = 1;

    
    
    void Start()
    {
#if UNITY_EDITOR
        PlayerPrefs.DeleteAll();
#endif
        felicidad = PlayerPrefs.GetFloat("Felicidad", 20);
        alimento = PlayerPrefs.GetFloat("Alimento", 20);
        limpieza = PlayerPrefs.GetFloat("Limpieza", 20);
        salud = PlayerPrefs.GetFloat("Salud", 20);

        startValue = PlayerPrefs.GetFloat(stat, 20);
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

        if(felicidad >= startValue + maxValueAdded)
        {
            for (int i = 3; i >= 0; i--)
            {
                if (felicidad >= sceneLimits[i] && alimento >= sceneLimits[i] && limpieza >= sceneLimits[i] && salud >= sceneLimits[i])
                {

                    StartCoroutine(Finish(scenes[i]));
                }

            }
        }
    }

    private IEnumerator Finish(SceneLoader s)
    {
        yield return new WaitForSeconds(endDelay);

        s.SwipeScene();

    }
}
