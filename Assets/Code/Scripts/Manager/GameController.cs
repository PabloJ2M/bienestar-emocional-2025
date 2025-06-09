using System;

[Flags]
public enum StatType { None = 0, Felicidad = 1, Alimento = 2, Limpieza = 4, Salud = 8 }

public class GameController : SingletonBasic<GameController>
{
    public event Action<StatType, float> onValueChanged;

    public void AddAmount(StatType type, float amount) => onValueChanged?.Invoke(type, amount);
}