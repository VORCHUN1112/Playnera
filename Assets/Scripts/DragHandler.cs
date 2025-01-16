using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Класс, реализующий логику перетаскивания объекта.
/// Имеет статические поля для проброса зависимости. Прототип :D.
/// </summary>
public class DragHandler : MonoBehaviour
{
	// Стат. ссылка на текущий перетаскиваемый объект. Рабочий колхоз.
	private static Transform _currentlyDrag;
	/// <summary>
	/// Стат. ссылка на компонент Scaleable текущего перетаскиваемого объекта.
	/// Наколхозил дабы не вызывать GetComponent ежекадрово для реализации логики "скейла".
	/// </summary>
	private static Scaleable _scaleable;
	
	// Отвечает за "задержку" следования перетаскиваемого объекта за курсором. 
	[SerializeField] private float _smooth = 1f;
	private Transform _transform;
	// Направление от объекта до курсора мыши в пикселях. Без нормализации.
	private Vector3 directionToMouseOnScreen;

	// Стат. свойства для чтения означенных полей.
	public static Transform CurrentlyDrag => _currentlyDrag;
	public static Scaleable Scaleable => _scaleable;
	
	// Свойство-шорткат на первый инстанс камеры с тегом "Main".
	private Camera mainCamera => Camera.main;
	// Свойство-шорткат на координаты курсора в пикселях".
	private Vector3 MouseScreenPosition => Input.mousePosition;
	// Свойство-шорткат на координаты первого касания экрана в пикселях. Для мобилок.
	private Vector3 FirstTouchPosition => Input.GetTouch(0).position;
	// Свойство-шорткат на позицию объекта в игровом окружении.
	private Vector3 currentWorldPosition => _transform.position;
	
	//Инициализация
	private void Awake()
	{
		_transform = GetComponent<Transform>();
	}

	/// <summary>
	/// Отвечает за логику начала перетаскивания объекта.
	/// По сути, реинициализируем определенные поля.
	/// </summary>
	public void StartDrag(PointerEventData data)
	{
		if (CurrentlyDrag) return;
		
		Vector3 screenClickPosition;
		#if UNITY_ANDROID || UNITY_IOS
		{
			screenClickPosition = FirstTouchPosition;
		}
		#else
		{
			screenClickPosition = MouseScreenPosition;
		}
		#endif
		
		Vector3 objectScreenPosition = GetObjectScreenPosition();
		directionToMouseOnScreen = screenClickPosition - objectScreenPosition;

		_currentlyDrag = transform;
		_scaleable = GetComponent<Scaleable>();
	}

	/// <summary>
	/// Реализует логику перетаскивания (привязал через события к Update).
	/// Считает координаты курсора в игр. окружении, объекта (там же) и двигает объект к курсору.
	/// </summary>
	public void Drag(PointerEventData data)
	{
		if (!CurrentlyDrag) return;
		
		float smooth = (1 / _smooth) * Time.deltaTime;
		Vector3 screenClickPosition;

		#if UNITY_ANDROID || UNITY_IOS
		{
			screenClickPosition = FirstTouchPosition;
		}
		#else
		{
			screenClickPosition = MouseScreenPosition;
		}
		#endif
		
		Vector3 newScreenPosition = screenClickPosition - directionToMouseOnScreen;
		Vector3 newWorldPosition = mainCamera.ScreenToWorldPoint(newScreenPosition);
		
		CurrentlyDrag.position = Vector3.Lerp(currentWorldPosition, newWorldPosition, t: smooth);
	}

	/// <summary>
	/// Окончание перетаскивания. Нет тела (ссылок) - нет дела (перетаскивания).
	/// </summary>
	public void EndDrag(PointerEventData data)
	{
		if (!CurrentlyDrag) return;
		
		_currentlyDrag = null;
		_scaleable = null;
	}

	/// <summary>
	/// Через камеру преобразует пиксельные координаты в координаты в игровом окружении.
	/// </summary>
	private Vector3 GetObjectScreenPosition()
	{
		Vector3 objectScreenPoint = mainCamera.WorldToScreenPoint(currentWorldPosition);
		return objectScreenPoint;
	}
}
