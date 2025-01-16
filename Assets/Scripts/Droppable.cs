using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Класс, предназначенный для реализации желаемого поведения "падающего" объета.
/// Процесс "падения" начинается извне, оканчивается контактом с коллайдером, имеющем на Gameobject'е затычку-тип DropZone.
/// В целом, представляет собой ивентовую оповещалку для задания этого самого поведения.
/// </summary>
public class Droppable : MonoBehaviour
{
	// Флаг, не позволящий еще не "падающим" объектам реализовывать основную логику.
	private bool _isDropped = false;

	//Собственно, события.
	public UnityEvent OnDropStarted;
	public UnityEvent OnDropFinished;
	
	// Свойтво для чтения флага.
	public bool IsDropped => _isDropped; 

	// С первого фрейма игры запускаем процесс падения объектов. Мало ли кто в воздухе "стоит".
	private void Start()
	{
		Drop();
	}
	
	// Метод, отвечающий за начало "падения". Переключает флаг, вызывает событие.
	public void Drop()
	{
		if (IsDropped) return;
		
		_isDropped = true;
		OnDropStarted.Invoke();
	}

	/// <summary>
	// Метод, реализующий конец "падения". 
	// Переключает флаг, вызывает событие. 
	// Способ внешнего прерывания "падения".
	/// </summary>
	public void FinishDrop()
	{
		if (!IsDropped) return;
		
		_isDropped = false;
		OnDropFinished?.Invoke();
	}

	// Внутренний способ прерываения "падения" - столкновение с объектом, имеющим компонент типа DropZone.
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!IsDropped) return;
		
		if (other.TryGetComponent<DropZone>(out DropZone dropZone))
		{
			FinishDrop();
		}
	}
}