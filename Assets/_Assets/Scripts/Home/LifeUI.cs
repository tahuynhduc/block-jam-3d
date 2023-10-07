using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{
    [SerializeField] Text _lifeText;
    private void Awake()
    {
        _lifeText = GetComponent<Text>();
    }
    public void ShowLifeText(int value)
    {
        _lifeText.text = value.ToString();
    }
}
