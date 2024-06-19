using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIView : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _leftNextCarButton;
    [SerializeField] private Button _rightNextCarButton;
    [SerializeField] private Button _buyButton;

    [Header("Price")]
    [SerializeField] private TextMeshProUGUI _priceText;
    

    public Button LeftNextCarButton => _leftNextCarButton;
    public Button RightNextCarButton => _rightNextCarButton;
    public Button BuyButton => _buyButton;
    public TextMeshProUGUI PriceText => _priceText;


    private void Start()
    {

    }

    public void UpdateUI()
    {

    }
}
