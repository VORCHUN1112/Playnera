using UnityEngine;
using System;

/// <summary>
/// По сути класс-хранилище для пары значений трасформ - модификатор размера.
/// Удобства ради. Сериализуется.
/// </summary>
[Serializable]
public class ScalePoint
{
	[SerializeField] private Transform _transform;
	[SerializeField] private float _scaleFactor;

	public Transform Transform => _transform; 
	public float ScaleFactor => _scaleFactor; 
}