using UnityEngine;

public class CubeTransformMover : MonoBehaviour
{
    // Скорость движения, настраивается в инспекторе
    [SerializeField] private float moveSpeed = 8f;

    void Update()
    {
        // 1. Получаем ввод с клавиатуры (W, A, S, D или стрелки)
        float moveHorizontal = Input.GetAxis("Horizontal"); // Ось X
        float moveVertical = Input.GetAxis("Vertical");     // Ось Z

        // 2. Создаем вектор направления движения
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Нормализуем вектор, чтобы движение по диагонали не было быстрее
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }

        // 3. Двигаем объект, напрямую меняя его transform.position
        // Умножаем на Time.deltaTime, чтобы скорость не зависела от частоты кадров
        transform.position += movement * moveSpeed * Time.deltaTime;

        // 4. (Опционально) Поворачиваем куб в сторону движения для красоты
        if (movement != Vector3.zero)
        {
            // Создаем "поворот", который смотрит в направлении движения
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            // Плавно поворачиваем объект к цели
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }
    }
}