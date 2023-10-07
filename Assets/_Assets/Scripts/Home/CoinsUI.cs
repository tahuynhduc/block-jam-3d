using UnityEngine;
using UnityEngine.UI;

public class CoinsUI : UIHomeController
{
    [SerializeField] Text _coinsText;
    private void Awake()
    {
        _coinsText = GetComponent<Text>();
    }
    public void ShowCoins()
    {
        ShowCoins(_coinsText);
    }
}
