using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {

	public enum RotationAxes {
		MouseXAndY = 0,
		MouseX = 1,
		MouseY = 2
	}

	public RotationAxes axes = RotationAxes.MouseXAndY;

	public float sensitivityHor = 9.0f;
	public float sensitivityVert = 9.0f;

	public float minimumVert = -45.0f;
	public float maximumVert = 45.0f;

	private float _rotationX = 0;

	// Use this for initialization
	void Start () {
		Rigidbody body = GetComponent<Rigidbody>();
		if (body != null)
			body.freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (axes == RotationAxes.MouseX)
		{
			// поворот по горизонтали
			transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0); // увеличивает угол поворота
		}
		else if (axes == RotationAxes.MouseY)
		{
			// поворот по вертикали
			_rotationX -= Input.GetAxis("Mouse Y") *  sensitivityVert; // увеличиваем угол поворота
			_rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert); // фиксируем угол в заданном диапазоне
			float rotationY = transform.localEulerAngles.y; // сохраняем одинаковый угол поворота вокруг оси Y - вращение в горизонтальной плоскости отсутствует
			transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0); // задаем угол поворота в явном виде
		}
		else
		{
			// комбинированный поворот
			_rotationX -= Input.GetAxis("Mouse Y") *  sensitivityVert; // увеличиваем угол поворота
			_rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert); // фиксируем угол в заданном диапазоне
			float delta = Input.GetAxis("Mouse X") * sensitivityHor;
			float rotationY = transform.localEulerAngles.y + delta;
			transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0); // задаем угол поворота в явном виде
		}
	}
}
