using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    [SerializeField] private ShopUIView _shopUIView;
    [SerializeField] private MainMenuView _mainMenuView;

    [SerializeField] private List<GameObject> _cars;

    [Header("Applied")]
    [SerializeField] private int _loadIndex;

    [SerializeField] private List<bool> _isBuy;

    [SerializeField] private List<int> _prices;

    [SerializeField] private TextMeshProUGUI _coinsText;

    public int LoadIndex => _loadIndex;

    private void Start()
    {
        UpdateCoins();
        _isBuy = SaveData.Instance.Data.IsBuyShop;

        _shopUIView.LeftNextCarButton.onClick.AddListener(() => ChangeCar(Direction.Left));
        _shopUIView.RightNextCarButton.onClick.AddListener(() => ChangeCar(Direction.Right));
        _shopUIView.BuyButton.onClick.AddListener(Buy);
        LoadCars();
    }

    private void Buy()
    {
        if(SaveData.Instance.Data.Coins > _prices[_loadIndex])
        {
            _isBuy[_loadIndex] = true;

            SaveData.Instance.Data.Coins = SaveData.Instance.Data.Coins - _prices[_loadIndex];
            SaveData.Instance.Data.IsBuyShop[_loadIndex] = true;
            _isBuy = SaveData.Instance.Data.IsBuyShop;
            SaveData.Instance.SaveYandex();

            UpdateCoins();
            LoadCars();
        }
    }

    private void LoadCars()
    {
        foreach (GameObject car in _cars)
        {
            car.SetActive(false);
        }
        _cars[_loadIndex].SetActive(true);

        if (_isBuy[_loadIndex])
        {
            _shopUIView.BuyButton.gameObject.SetActive(false);
            _shopUIView.PriceText.gameObject.SetActive(false);
            _mainMenuView.StartButton.gameObject.SetActive(true);
        }
        else
        {
            _shopUIView.PriceText.text = _prices[_loadIndex].ToString();
            _shopUIView.PriceText.gameObject.SetActive(true);
            _shopUIView.BuyButton.gameObject.SetActive(true);
            _mainMenuView.StartButton.gameObject.SetActive(false);
        }
    }

    private void UpdateCoins()
    {
        _coinsText.text = SaveData.Instance.Data.Coins.ToString();
    }

    public void ChangeCar(Direction direction)
    {
        if (direction == Direction.Left)
        {
            _loadIndex = (_loadIndex - 1 + _cars.Count) % _cars.Count;
        }
        else if (direction == Direction.Right)
        {
            _loadIndex = (_loadIndex + 1) % _cars.Count;
        }
        LoadCars();
    }

    public enum Direction
    {
        Left,
        Right
    }
}
