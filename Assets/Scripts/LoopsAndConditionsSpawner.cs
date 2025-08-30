using UnityEngine;
using System.Collections; // Обязательно для использования корутин (IEnumerator)

public class LoopsAndConditionsSpawner : MonoBehaviour
{
    // Префаб (шаблон) объекта, который мы будем создавать
    public GameObject cubePrefab;
    public int gridWidth = 10;
    public int gridHeight = 10;
    // Тут будет оффсет вместо "магического числа"

    // Тут будет Заголовок с цветами вместо "магического цвета"

    // Задержка между появлением кубов в секундах.

    [Tooltip("Задержка между появлением кубов в секундах")]
    public float spawnDelay = 1.0f;

    void Start()
    {
        // Вместо того чтобы выполнять циклы здесь, мы ЗАПУСКАЕМ корутину.
        // Сама корутина будет работать "в фоновом режиме", не замораживая игру.
        StartCoroutine(SpawnGridWithDelay());

        // Пример с циклом WHILE останется здесь и выполнится мгновенно при старте.
        RunWhileLoopExample();
    }

    // Это и есть наша корутина. Обрати внимание на тип возвращаемого значения: IEnumerator.
    private IEnumerator SpawnGridWithDelay()
    {
        Debug.Log("--- Начинаем корутину для создания сетки ---");

        // Трансформ, к которому будут прикрепляться кубы (для порядка в иерархии)
        Transform cubeHolder = new GameObject("Cube Holder").transform;

        // Цикл FOR 
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                // Создаем позицию для нового куба
                Vector3 spawnPosition = new Vector3(x * 1.5f, 0, y * 1.5f);

                // Создаем экземпляр префаба в указанной позиции
                GameObject newCube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);

                // Делаем куб дочерним объектом для порядка
                newCube.transform.parent = cubeHolder;

                // Условие IF-ELSE IF-ELSE для раскраски
                if ((x + y) % 2 == 0)
                {
                    newCube.GetComponent<Renderer>().material.color = Color.blue;
                }
                else
                {
                    newCube.GetComponent<Renderer>().material.color = Color.red;
                }

                if (x == y)
                {
                    newCube.GetComponent<Renderer>().material.color = Color.green;
                    newCube.transform.localScale = new Vector3(1, 2, 1);
                }

                // *** ВОТ САМАЯ ГЛАВНАЯ ЧАСТЬ ***
                // "yield return" приостанавливает выполнение корутины.
                // new WaitForSeconds() - это инструкция "подождать указанное количество секунд".
                // После ожидания код продолжится со следующей итерации цикла.
                yield return new WaitForSeconds(spawnDelay);
            }
        }

        Debug.Log("--- Создание сетки завершено ---");
    }


    // Логика с циклом WHILE
    void RunWhileLoopExample()
    {
        Debug.Log("--- Начинаем цикл WHILE ---");
        int counter = 5;
        while (counter > 0)
        {
            Debug.Log("Обратный отсчет: " + counter);
            counter--;
        }
        Debug.Log("Пуск!");
    }
}