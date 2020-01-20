using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour {

	public float speed = 6.0f;
	public float gravity = -9.8f;

	private CharacterController _charController; // переменная для ссылки на компонент
	// Use this for initialization
	void Start () {
		_charController = GetComponent<CharacterController>(); // доступ к другим компонентам присоединенным к этому же объекту
	}
	
	// Update is called once per frame
	void Update () {
		float deltaX = Input.GetAxis("Horizontal") * speed;
		float deltaZ = Input.GetAxis("Vertical") * speed;
		Vector3 movement = new Vector3(deltaX, 0, deltaZ);
		movement = Vector3.ClampMagnitude(movement, speed); // ограничим движение по диагонали той же скоростью, что и движение параллельно осям
		movement.y = gravity;
		movement *= Time.deltaTime;
		movement = transform.TransformDirection(movement); // преобразуем вектор движения от локальных к глобальным координатам
		_charController.Move(movement);

		//transform.Translate(deltaX * Time.deltaTime, 0, deltaZ * Time.deltaTime);
	}
}
