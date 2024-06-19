using System;
using System.Collections.Generic;
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

    [SerializeField] private List<bool> _isBuy = new List<bool> { true, false, false, false, false };

    [SerializeField] private List<int> _prices;

    private void Start()
    {
        _shopUIView.LeftNextCarButton.onClick.AddListener(() => ChangeCar(Direction.Left));
        _shopUIView.RightNextCarButton.onClick.AddListener(() => ChangeCar(Direction.Right));
        _shopUIView.BuyButton.onClick.AddListener(Buy);
        LoadCars();
    }

    private void Buy()
    {
        _isBuy[_loadIndex] = true;
        LoadCars();
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
