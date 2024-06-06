using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _cars;

    [Header("Buttons")]
    [SerializeField] private Button _left;
    [SerializeField] private Button _right;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _appedingButton;

    [Header("Price")]
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private List<int> _prices;

    [Header("Applied")]
    [SerializeField] GameObject _checkMark;
    [SerializeField] private int _isLoadIndex;
    [SerializeField] private int _isAppliedIndex;

    [SerializeField] private List<bool> _isBuy = new List<bool> {true, false, false, false, false};

    private void Start()
    {
        _left.onClick.AddListener(() => ButtonClick(_button: "left"));
        _right.onClick.AddListener(() => ButtonClick(_button: "right"));

        _buyButton.onClick.AddListener(Buy);

        _appedingButton.onClick.AddListener(() => Appliending());

        LoadCars();
    }

    private void Buy()
    {
        _isBuy[_isLoadIndex] = true;
        LoadCars();
    }

    private void LoadCars()
    {
        foreach(GameObject _cars in _cars)
        {
            _cars.SetActive(false);
        }
        _cars[_isLoadIndex].SetActive(true);

        if (_isLoadIndex == _isAppliedIndex)
            _checkMark.SetActive(true);
        else
            _checkMark.SetActive(false);

        if (_isBuy[_isLoadIndex])
        {
            _buyButton.gameObject.SetActive(false);
            _priceText.gameObject.SetActive(false);
        }
        else
        {
            _priceText.text = _prices[_isLoadIndex].ToString();
            _priceText.gameObject.SetActive(true);
            _buyButton.gameObject.SetActive(true);
        }
    }

    private void Appliending()
    {
        _isAppliedIndex = _isLoadIndex;
        LoadCars();
    }

    private void ButtonClick(string _button)
    {
        if(_button == "left")
        {
            _isLoadIndex--;
        }
        else
        {
            _isLoadIndex++;
        }
        if (_isLoadIndex > 4) 
            _isLoadIndex = 0;
        if (_isLoadIndex < 0) 
            _isLoadIndex = 2;
        
        LoadCars();
    }
} 
