using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// "Класс-сообщатор" о Graphic Raycast перетаскивани объекта.
/// Доп-но реализует оповещения в игроциклах FixedUpdate, Update, LateUpdate. Удобно.
/// </summary>
[RequireComponent(typeof(Image))]
public class ImageDragEvents : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	private PointerEventData _eventData;
	
	[Header("Events")]
	public UnityEvent<PointerEventData> OnDragStarted;
	public UnityEvent<PointerEventData> OnDragPerformed;
	public UnityEvent<PointerEventData> OnDragUpdated;
	public UnityEvent<PointerEventData> OnDragLateUpdated;
	public UnityEvent<PointerEventData> OnDragFixedUpdated;
	public UnityEvent<PointerEventData> OnDragFinished;

	private bool IsDragging => _eventData != null;
	public PointerEventData EventData => _eventData;

	private void Update()
	{
		if (!IsDragging) return;
		OnDragUpdated?.Invoke(EventData);
	}
	
	private void LateUpdate()
	{
		if (!IsDragging) return;
		OnDragLateUpdated.Invoke(EventData);
	}
	
	private void FixedUpdate()
	{
		if (!IsDragging) return;
		OnDragFixedUpdated.Invoke(EventData);
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (IsDragging) return;
		
		_eventData = eventData;
		OnDragStarted?.Invoke(eventData);
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (!IsDragging) return;
		
		_eventData = eventData;
		OnDragPerformed.Invoke(eventData);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (!IsDragging) return;
		
		_eventData = eventData;
		OnDragFinished.Invoke(eventData);
	
		_eventData = null;
	}
}