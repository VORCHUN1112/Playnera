using UnityEngine;

/// <summary>
/// Зона изменение размера объекта на основе Y координат.
/// Не наследуем. Наследуется от ScaleZone.
/// </summary>
public sealed class VerticalScaleZone : ScaleZone
{
	// Собственно - сами точки, на знице расстояния которых считается мод. размера объекта.
	[SerializeField] private ScalePoint _topScalePoint;
	[SerializeField] private ScalePoint _bottomScalePoint;

	// Исполение котракта на наследуемые свойства для чтения точек внутри наследников.
	protected override ScalePoint FirstPoint => _topScalePoint;
	protected override ScalePoint SecondPoint => _bottomScalePoint;

	/// <summary>
	/// Исполнение контрактана реализацию логики рассчета размера объекта в зоне по Y значениям.
	/// </summary>
	/// <param name="item"></param>
	/// <returns></returns>
	protected override float GetItemSizeInZone(Transform item)
	{
		float GetItemHeightInZone()
		{
			float itemY = item.transform.position.y;
			float bottomPointY = SecondPoint.Transform.position.y;
			float itemHeightInZone = itemY - bottomPointY;
			return itemHeightInZone;
		}

		float itemHeight = GetItemHeightInZone();
		return itemHeight;
	}

	/// <summary>
	/// Исполнение контракта на реализацию логики рассчета размера зоны на основе точек по Y значениям.
	/// </summary>
	/// <returns></returns>
	protected override float GetZoneSize()
	{
		float GetZoneHeight()
		{
			float topPointY = FirstPoint.Transform.position.y;
			float bottomPointY = SecondPoint.Transform.position.y;
			float zoneHeight = topPointY - bottomPointY;
			return zoneHeight;
		}

		float zoneHight = GetZoneHeight();
		return zoneHight;
	}
}
