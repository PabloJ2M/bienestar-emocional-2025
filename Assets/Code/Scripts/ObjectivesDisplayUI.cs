using TMPro;
using UnityEngine;
using UnityEngine.Animations;

public class ObjectivesDisplayUI : MonoBehaviour
{
    [SerializeField] private TweenCanvasGroup fade;
    [SerializeField] private TextMeshProUGUI text;

    public void SetValue(float value)
    {
        text.SetText($"{100 * value}% ambiente limpio");
        fade.FadeIn();
    }
}