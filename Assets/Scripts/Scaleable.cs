using UnityEngine;

/// <summary>
/// Класс, отвечающий за изменение размера объекта на основе передаваемого модификатора размера.
/// Размер меняется с целью создания видимой перспетивы для 2D объекта.
/// </summary>
public class Scaleable : MonoBehaviour
{
	// Сам основополагающий модификатор размера. 
	[SerializeField] private float _scaleModifier;
	// Сохраненный исходный размер объекта, мофицируемыый модификатором.
	private Vector3 _startScale;
	// Ссылка на компонент Transform.
	private Transform _transform;
	
	//Публичные свойства для чтения означенных полей.
	public float ScaleModifier => _scaleModifier;
	public Vector3 StartScale => _startScale;
	public Transform Transform => _transform;

	// Инициализация ссылки на компонент
	private void Awake()
	{
		_transform = GetComponent<Transform>();
	}

	// Сохранение стартового размера и стартовая модификация размера.
	private void Start()
	{
		_startScale = Transform.localScale;
		ModifyLocalScale();
	}
	
	//Метод, благодаря которому модификатор размера попадает в объект класса. Вызывает метод изменения размера.
	public void SetScaleModifier(float scaleModifier)
	{
		_scaleModifier = scaleModifier;
		ModifyLocalScale();
	}
	
	//Метод, изменяющий размер объекта.
	private void ModifyLocalScale()
	{
		Transform.localScale = StartScale * ScaleModifier;
	}
}