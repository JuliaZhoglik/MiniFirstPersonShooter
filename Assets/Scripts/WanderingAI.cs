using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour {
    [SerializeField] private GameObject fireballPrefab;
    private GameObject _fireball;

	public float speed = 3.0f; // скорость движения
	public float obstacleRange = 5.0f; // расстояние до препятствия
    public const float baseSpeed = 3.0f; // базовая скорость

	private bool _alive;

    void Awake()
    {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    private void OnSpeedChanged(float value)
    {
        speed = baseSpeed * value;
    }

    void Start ()
	{
		_alive = true;
        
	}
	// Update is called once per frame
	void Update () {
		if (_alive)
		{
			transform.Translate(0, 0, speed * Time.deltaTime); // движение вперед

			Ray ray = new Ray(transform.position, transform.forward); // луч находится в том же положении и нацеливается в то же направление что и персонаж
			RaycastHit hit;
			if (Physics.SphereCast(ray, 0.75f, out hit)) // бросаем луч с описанной вокруг него окружностью с радиусом 0.75
			{
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>())
                {
                    if (_fireball == null)
                    {
                        _fireball = Instantiate(fireballPrefab) as GameObject;
                        _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                        _fireball.transform.rotation = transform.rotation;
                    }
                }
				else if (hit.distance < obstacleRange)
				{
					float angle = Random.Range(-110, 110); // поворот наполовину случайным выбором направления
					transform.Rotate(0, angle, 0);
				}
			}
		}
	}

	public void SetAlive(bool alive)
	{
		_alive = alive;
	}
}
