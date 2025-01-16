using UnityEngine;

/// <summary>
/// Абстрактный класс-скелет для логики изменения размера.
/// </summary>
public abstract class ScaleZone : MonoBehaviour
{
	// Контракты на свойства для чтения парты трасформ - мод. размера.
	protected abstract ScalePoint FirstPoint { get; }
	protected abstract ScalePoint SecondPoint { get; }
	// Переопределяемое свойство - шорткат на перетаскиваемый объект. Колхозно, но и ладно.
	protected virtual Transform Dragging => DragHandler.CurrentlyDrag;

	// Переопределяемая логика "при попадании" объекта в зону. Не особо пригодилась.
	public virtual void OnEnterZone()
	{
		if (!Dragging) return;
	}

	// Переопределяемая логика "при нахождении" объекта в зоне. Сама суть.
	public virtual void OnStayInZone()
	{
		if (!Dragging) return;
		
		float currentScaleFactor = CalculateScaleFactor();
		Scaleable scaleable = DragHandler.Scaleable;
		scaleable.SetScaleModifier(currentScaleFactor);
	}

	// Переопределяемая логика "при выходе" объекта из зоны. Не особо пригодилась.
	public virtual void OnExitZone()
	{
		if (!Dragging) return;
	}

	// Рассчитывает мод. размера на основе точек.
	protected virtual float CalculateScaleFactor()
	{
		float zoneHeight = GetZoneSize();
		float itemSizeInZone = GetItemSizeInZone(Dragging);

		float itemSizePercentage = itemSizeInZone / zoneHeight;
		float currentScaleFactor = Mathf.Lerp(SecondPoint.ScaleFactor,
		FirstPoint.ScaleFactor, itemSizePercentage);
		return currentScaleFactor;
	}

	// Контракт на реализацию логики рассчета размера зоны на основе точек.
	protected abstract float GetZoneSize();
	// Контракт на реализацию логики рассчета размера объекта в зоне.
	protected abstract float GetItemSizeInZone(Transform item);
}
