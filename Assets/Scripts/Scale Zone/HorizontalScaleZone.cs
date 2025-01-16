using UnityEngine;

/// <summary>
/// Зона изменение размера объекта на основе X координат.
/// Не наследуем. Наследуется от ScaleZone.
/// </summary>
public sealed class HorizontalScaleZone : ScaleZone
{
	// Собственно - сами точки, на знице расстояния которых считается мод. размера объекта.
	[SerializeField] private ScalePoint _leftScalePoint;
	[SerializeField] private ScalePoint _rightScalePoint;

	// Исполение котракта на наследуемые свойства для чтения точек внутри наследников.
	protected override ScalePoint FirstPoint => _leftScalePoint;
	protected override ScalePoint SecondPoint => _rightScalePoint;

	/// <summary>
	/// Исполнение контрактана реализацию логики рассчета размера объекта в зоне по X значениям.
	/// </summary>
	/// <param name="item"></param>
	/// <returns></returns>
	protected override float GetItemSizeInZone(Transform item)
	{
		float GetItemWidthInZone()
		{
			float itemX = item.transform.position.x;
			float bottomPointX = SecondPoint.Transform.position.x;
			float itemWidthInZone = itemX - bottomPointX;
			return itemWidthInZone;
		}

		float itemWidth = GetItemWidthInZone();
		return itemWidth;
	}

	/// <summary>
	/// Исполнение контракта на реализацию логики рассчета размера зоны на основе точек по X значениям.
	/// </summary>
	/// <returns></returns>
	protected override float GetZoneSize()
	{
		float GetZoneWidth()
		{
			float topPointX = FirstPoint.Transform.position.x;
			float bottomPointX = SecondPoint.Transform.position.x;
			float zoneWidth = topPointX - bottomPointX;
			return zoneWidth;
		}

		float zoneWidth = GetZoneWidth();
		return zoneWidth;
	}
}
