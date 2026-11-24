using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [Header("UI References")]
    // Suggestion: _scoreText, _speedText y _lastDisplayedSpeed no se usan; son restos de otro proyecto junto con el bloque comentado de las lineas 32 a 48
    // (referencia a un GameManager que no existe en esta entrega). Borra el codigo muerto.
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _speedText;
    [SerializeField] private Slider _healthSlider;

    private int _lastDisplayedSpeed = -1;

    private void OnEnable()
    {
        // PlayerHealth.OnHealthChanged += UpdateHealthBar;
    }

    private void OnDisable()
    {
        
        // PlayerHealth.OnHealthChanged -= UpdateHealthBar;
    }

    // Suggestion: Update, OnEnable y OnDisable estan vacios (solo tienen codigo comentado). Unity igual los invoca; borralos si no se usan.
    private void Update()
    {
       
        // UpdateScoreDisplay();
        // UpdateSpeedDisplay();
    }

    /*
    private void UpdateScoreDisplay()
    {
        _scoreText.text = GameManager.Instance.CurrentScore.ToString("0");
    }

    private void UpdateSpeedDisplay()
    {
        int speedInt = Mathf.FloorToInt(GameManager.Instance.CurrentSpeed);

        if (speedInt != _lastDisplayedSpeed)
        {
            _lastDisplayedSpeed = speedInt;
            _speedText.text = $"speed = {speedInt}x";
        }
    }
    */

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    { 
        _healthSlider.maxValue = maxHealth;
        _healthSlider.value = currentHealth;
    }
}