using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _speedText;
    [SerializeField] private Slider _healthSlider;

    private int _lastDisplayedSpeed = -1;

    private void OnEnable()
    {
        // Lo conectaremos cuando creemos el script PlayerHealth
        // PlayerHealth.OnHealthChanged += UpdateHealthBar;
    }

    private void OnDisable()
    {
        // Lo conectaremos cuando creemos el script PlayerHealth
        // PlayerHealth.OnHealthChanged -= UpdateHealthBar;
    }

    private void Update()
    {
        // Lo silenciamos hasta tener un GameManager para este juego
        // UpdateScoreDisplay();
        // UpdateSpeedDisplay();
    }

    /* Guardamos esta lógica para cuando sumemos coleccionables
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

    // Esta función está perfecta, la dejamos tal cual para usarla en breve
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        if (_healthSlider != null)
        {
            _healthSlider.maxValue = maxHealth;
            _healthSlider.value = currentHealth;
        }
    }
}