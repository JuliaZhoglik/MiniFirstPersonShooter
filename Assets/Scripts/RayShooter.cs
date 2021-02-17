using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayShooter : MonoBehaviour {

	private Camera _camera;
	// Use this for initialization
	void Start () {
		_camera = GetComponent<Camera>();

	}

	void OnGUI()
	{
		int size = 12;
		float posX = _camera.pixelWidth/2 - size/4;
		float posY = _camera.pixelHeight/2 - size/2;
		GUI.Label(new Rect(posX, posY, size, size), "*");
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetMouseButtonDown(0)) // по нажатию мыши
        && !EventSystem.current.IsPointerOverGameObject()) // проверяем, что GIU не используется
		{
			Vector3 point = new Vector3(_camera.pixelWidth/2, _camera.pixelHeight/2, 0); // середина экрана
			Ray ray = _camera.ScreenPointToRay(point); // создаем луч
			RaycastHit hit; // информация о пересечении луча
			
			if (Physics.Raycast(ray, out hit)) // испущенный луч заполняет информацией переменную, на которую имеется ссылка
			{
				GameObject hitObject = hit.transform.gameObject; // объект в который попал луч
				ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
				if (target != null)
				{
					//Debug.Log("Target hit");
					target.ReactToHit();
                    Messenger.Broadcast(GameEvent.ENEMY_HIT);
				}
				else
				{
					// выполняется при пересечении луча с препятствием
					//Debug.Log("Hit " + hit.point);
					StartCoroutine(SphereIndicator(hit.point)); // StartCoroutine - запуск сопрограммы
				}
			}
		}
	}

	private IEnumerator SphereIndicator(Vector3 pos)
	{
		GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.transform.position = pos;
		
		yield return new WaitForSeconds(1);
		
		Destroy(sphere);
	}
}
