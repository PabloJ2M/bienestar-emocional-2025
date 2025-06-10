using UnityEngine;

public class GameConfig : MonoBehaviour
{
    [SerializeField] private bool _centerPlayer;
    [SerializeField] private bool _unFocusCamera;

    private GameController _controller;

    private void Awake() => _controller = GameController.Instance;

    private void Start()
    {
        if (_centerPlayer) _controller?.GoToCenter(true);
        if (_unFocusCamera) _controller?.CameraFocusStatus(true);
    }
    private void OnDisable()
    {
        _controller?.GoToCenter(false);
        _controller?.CameraFocusStatus(false);
    }
}