using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// "Класс-сообщатор" о коллайдерных ивентах курсора (тапа) для перетаскивания объекта.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Collider2DMouseEvents : MonoBehaviour
{
	private Collider2D _collider;

	[Header("Events")]
	public UnityEvent<Collider2D> OnMouseEntered;
	public UnityEvent<Collider2D> OnMouseStayedOver;
	public UnityEvent<Collider2D> OnMouseExited;
	public UnityEvent<Collider2D> OnMousePressedDown;
	public UnityEvent<Collider2D> OnMouseDragged;
	public UnityEvent<Collider2D> OnMousePressedUp;

	public Collider2D Collider => _collider;

	private void Awake()
	{
		_collider = GetComponent<Collider2D>();
	}

	private void OnMouseEnter()
	{
		OnMouseEntered?.Invoke(Collider);
	}

	private void OnMouseOver()
	{
		OnMouseStayedOver?.Invoke(Collider);
	}

	private void OnMouseExit()
	{
		OnMouseExited.Invoke(Collider);
	}

	private void OnMouseDown()
	{
		OnMousePressedDown.Invoke(Collider);
	}

	private void OnMouseDrag()
	{
		OnMouseDragged.Invoke(Collider);
	}

	private void OnMouseUp()
	{
		OnMousePressedUp.Invoke(Collider);
	}
}
