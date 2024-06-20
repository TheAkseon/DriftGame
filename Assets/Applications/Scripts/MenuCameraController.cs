using UnityEngine;

public class MenuCameraController : MonoBehaviour
{
    [SerializeField] private Transform _target; // Цель, вокруг которой вращается камера (например, машина)
    [SerializeField] private float _distance = 1.0f; // Расстояние до цели
    [SerializeField] private float _xSpeed = 1200.0f; // Скорость вращения по оси X
    [SerializeField] private float _ySpeed = 1200.0f; // Скорость вращения по оси Y
    [SerializeField] private float _smoothSpeed = 0.05f; // Плавность движения
    [SerializeField] private float _damping = 3.0f; // Плавность отскока после отпускания касания

    private float _x = 0.0f;
    private float _y = 0.0f;

    private Vector2 _currentTouch;
    private Vector2 _previousTouch;

    private Vector3 _targetPosition;
    private Quaternion _targetRotation;


    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        _x = angles.y;
        _y = angles.x;

        _targetPosition = transform.position;
        _targetRotation = transform.rotation;
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(1) || Input.touchCount == 1 || Input.GetMouseButton(0)) // ПК (правая кнопка мыши) или мобильное устройство (касание)
        {
            if (Input.GetMouseButton(1) || Input.GetMouseButton(0)) // Управление мышью
            {
                _x += Input.GetAxis("Mouse X") * _xSpeed * Time.deltaTime;
                _y -= Input.GetAxis("Mouse Y") * _ySpeed * Time.deltaTime;
            }
            else if (Input.touchCount == 1) // Управление касанием
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    _x += touch.deltaPosition.x * _xSpeed * 0.02f;
                    _y -= touch.deltaPosition.y * _ySpeed * 0.02f;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    // Сохраняем скорость касания для плавного движения
                    _currentTouch = touch.deltaPosition;
                    _previousTouch = touch.deltaPosition;
                }
            }

            // Ограничение углов поворота по оси Y
            _y = Mathf.Clamp(_y, 0, 20);

            // Обновление позиции и вращения камеры
            _targetRotation = Quaternion.Euler(_y, _x, 0);
            _targetPosition = _targetRotation * new Vector3(0.0f, 0.0f, -_distance) + _target.position;
        }
        else
        {
            // Плавное продолжение движения камеры после отпускания касания
            if (_currentTouch != Vector2.zero)
            {
                Vector3 velocity = (_previousTouch - _currentTouch) * _damping * Time.deltaTime;
                _targetRotation *= Quaternion.Euler(velocity.y * Time.deltaTime, velocity.x * Time.deltaTime, 0);
                _currentTouch = Vector2.Lerp(_currentTouch, Vector2.zero, _smoothSpeed);
            }
        }

        // Применяем новое положение и вращение камеры
        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, _smoothSpeed);
        transform.position = Vector3.Lerp(transform.position, _targetPosition, _smoothSpeed);
    }
}
