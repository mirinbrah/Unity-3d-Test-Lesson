using UnityEngine;
using TMPro;
using System.Collections; // Обязательно для использования корутин (IEnumerator)

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;   // Сюда перетащим наш текст для счета
    public TextMeshProUGUI victoryText; // А сюда - новый текст победы

    private int score = 0;
    private bool victoryAchieved = false; // Флаг, чтобы победа сработала только один раз

    void Start()
    {
        // При старте сцены убедимся, что текст победы выключен
        if (victoryText != null)
        {
            victoryText.gameObject.SetActive(false);
        }
        UpdateScoreText();
    }

    void OnEnable()
    {
        Collectible.OnItemCollected += HandleItemCollected;
    }

    void OnDisable()
    {
        Collectible.OnItemCollected -= HandleItemCollected;
    }

    void HandleItemCollected()
    {
        // Если победа уже достигнута, ничего не делаем
        if (victoryAchieved) return;

        score++;
        UpdateScoreText();

        // ПРОВЕРКА УСЛОВИЯ ПОБЕДЫ
        if (score >= 3)
        {
            victoryAchieved = true; // Устанавливаем флаг
            StartCoroutine(ShowVictoryAndFadeOut()); // Запускаем корутину
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Собрано: " + score;
    }

    // Корутина для отображения и затухания текста
    private IEnumerator ShowVictoryAndFadeOut()
    {
        // 1. Включаем объект и делаем текст полностью видимым (альфа = 1)
        victoryText.gameObject.SetActive(true);
        victoryText.color = new Color(victoryText.color.r, victoryText.color.g, victoryText.color.b, 1);

        // 2. Ждем 2 секунды перед началом затухания
        // Если хочешь, чтобы он сразу начал затухать, можно убрать эту строку
        yield return new WaitForSeconds(2.0f);

        // 3. Плавно убираем текст в течение 2 секунд (эффект затухания)
        float fadeDuration = 2.0f;
        float elapsedTime = 0.0f;
        Color startColor = victoryText.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            victoryText.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
            yield return null; // Ждем следующего кадра
        }

        // 4. Выключаем объект, когда он стал полностью прозрачным
        victoryText.gameObject.SetActive(false);
    }
}