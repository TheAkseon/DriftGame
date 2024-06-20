using UnityEngine;
using UnityEngine.UI;

public class LevelCanvasView : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private MainMenuView _mainMenuView;
    [SerializeField] private ShopUIView _shopUIView;

    [Header("Buttons")]
    [SerializeField] private Button _dayFreeRideButton;
    [SerializeField] private Button _nightFreeRideButton;
    [SerializeField] private Button _backButton;

    [Header("Panels")]
    [SerializeField] private GameObject _loadingScreen;

    public Button DayFreeRideButton => _dayFreeRideButton;
    public Button NightFreeRideButton => _nightFreeRideButton;
    public GameObject LoadingScreen => _loadingScreen;

    private void Start()
    {
        _backButton.onClick.AddListener(DisactivateLevelCanvas);
    }

    private void DisactivateLevelCanvas()
    {
        _mainMenuView.gameObject.SetActive(true);
        _shopUIView.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
