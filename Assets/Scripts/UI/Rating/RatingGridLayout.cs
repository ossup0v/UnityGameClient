using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatingGridLayout : MonoBehaviour
{
	[SerializeField] private int _itemsCount;
	private GridLayoutGroup _gridLayoutGroup;
	protected RectTransform _parentRect;
	protected int _verticalPadding;

	void Start()
	{
		_gridLayoutGroup = GetComponent<GridLayoutGroup>();
		_parentRect = GetComponent<RectTransform>();
		_verticalPadding = _gridLayoutGroup.padding.top;

		_gridLayoutGroup.cellSize = CalculateCellSize();
	}

	void OnRectTransformDimensionsChange()
	{
		if (_gridLayoutGroup != null && _parentRect != null)
		{
			_gridLayoutGroup.cellSize = CalculateCellSize();
		}

	}

	protected virtual Vector2 CalculateCellSize()
	{
		return new Vector2(_parentRect.rect.width - _verticalPadding * 2, (_parentRect.rect.height - _verticalPadding * (_itemsCount + 1)) / _itemsCount);
	}
}
