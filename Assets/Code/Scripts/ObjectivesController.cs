using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ObjectivesController : MonoBehaviour
{
    [Header("Configuración de Niveles")]
    [SerializeField] private NivelObjetivo[] niveles;
    [SerializeField] private string endScene = "endScene";

    [Header("Eventos")]
    [SerializeField] private UnityEvent alCompletarNivel;  // Evento general
    [SerializeField] private UnityEvent alCompletarJuego;
    [SerializeField] GameObject[] rooms;

    private int[] progresos;
    private int nivelActual = 0;

    private void Start()
    {
        nivelActual = PlayerPrefs.GetInt("NivelActual", 0);
        ConfigurarNivel();
    }

    public void AgregarProgreso(string nombreObjetivo)
    {
        NivelObjetivo nivel = niveles[nivelActual];

        for (int i = 0; i < nivel.objetivos.Length; i++)
        {
            if (nivel.objetivos[i].nombre == nombreObjetivo)
            {
                progresos[i]++;
                PlayerPrefs.SetInt($"Obj_{nivelActual}_{nivel.objetivos[i].nombre}", progresos[i]);
                PlayerPrefs.Save();

                ActualizarTextos();
                VerificarCompleto();
                return;
            }
        }
    }

    private void ConfigurarNivel()
    {

        if (nivelActual >= niveles.Length)
        {
            SceneManager.LoadScene(endScene);
            return;
        }

        for (int i = 0; i < rooms.Length; i++)
        {
            rooms[i].SetActive(i == nivelActual);
        }



        NivelObjetivo nivel = niveles[nivelActual];
        progresos = new int[nivel.objetivos.Length];

        for (int i = 0; i < nivel.objetivos.Length; i++)
        {
            string key = $"Obj_{nivelActual}_{nivel.objetivos[i].nombre}";
            progresos[i] = PlayerPrefs.GetInt(key, 0);
        }
        VerificarCompleto();
        ActualizarTextos();
    }

    private void ActualizarTextos()
    {
        NivelObjetivo nivel = niveles[nivelActual];

        for (int i = 0; i < nivel.objetivos.Length; i++)
        {
            if (nivel.objetivos[i].textoUI != null)
            {
                nivel.objetivos[i].textoUI.text =
                    $"{nivel.objetivos[i].nombre}: {progresos[i]}/{nivel.objetivos[i].meta}";
            }
        }
    }

    private void VerificarCompleto()
    {
        NivelObjetivo nivel = niveles[nivelActual];

        for (int i = 0; i < nivel.objetivos.Length; i++)
        {
            if (progresos[i] < nivel.objetivos[i].meta)
                return;
        }

        alCompletarNivel?.Invoke(); 

        nivelActual++;
        PlayerPrefs.SetInt("NivelActual", nivelActual);
        PlayerPrefs.Save();

        if (nivelActual >= niveles.Length)
        {
            alCompletarJuego?.Invoke();
            SceneManager.LoadScene(endScene);
        }
        else
        {
            ConfigurarNivel();
        }
    }

    public void ForzarActualizarTextos()
    {
        ConfigurarNivel();
    }

    public void ResetProgresoTotal()
    {
        PlayerPrefs.DeleteKey("NivelActual");

        for (int n = 0; n < niveles.Length; n++)
        {
            NivelObjetivo nivel = niveles[n];
            for (int i = 0; i < nivel.objetivos.Length; i++)
            {
                PlayerPrefs.DeleteKey($"Obj_{n}_{nivel.objetivos[i].nombre}");
            }
        }

        PlayerPrefs.Save();
        nivelActual = 0;
        ConfigurarNivel();
    }
}


[System.Serializable]
public class ObjetivoIndividual
{
    public string nombre; 
    public TextMeshProUGUI textoUI; 
    public int meta;     
}
[System.Serializable]
public class NivelObjetivo
{
    public ObjetivoIndividual[] objetivos;
}

