using UnityEngine;

public class StatSender : MonoBehaviour
{
    [SerializeField] private StatType _types;
    [SerializeField, Tooltip("scale of 100")] private float _amount;

    private GameController _controller;

    private void Awake() => _controller = GameController.Instance;
    public void AddValue()
    {
        _controller?.AddAmount(_types, _amount / 100f);

        int nivelActual = PlayerPrefs.GetInt("NivelActual", 0);

        if (_types.HasFlag(StatType.Alimento))
            IncrementarObjetivo("Comer", nivelActual);

        if (_types.HasFlag(StatType.Limpieza))
            IncrementarObjetivo("Limpiar", nivelActual);

        if (_types.HasFlag(StatType.Felicidad))
            IncrementarObjetivo("Jugar", nivelActual);

        if (_types.HasFlag(StatType.Salud))
            IncrementarObjetivo("Salud", nivelActual);

        PlayerPrefs.Save();

        ObjectivesController controller = FindFirstObjectByType<ObjectivesController>();
        if (controller != null)
        {
            controller.ForzarActualizarTextos();
        }
    }
    private void IncrementarObjetivo(string nombreObjetivo, int nivel)
    {
        string key = $"Obj_{nivel}_{nombreObjetivo}";
        int actual = PlayerPrefs.GetInt(key, 0);
        PlayerPrefs.SetInt(key, actual + 1);
    }
}