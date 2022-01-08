using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatingRowGridLayout : RatingGridLayout
{
	[SerializeField] private int _availableWidthDenom;
	[SerializeField] private int _itemsInRowCount;
	protected sealed override Vector2 CalculateCellSize()
	{
		return new Vector2(_parentRect.rect.width / (_availableWidthDenom * _itemsInRowCount), _parentRect.rect.height);
	}
}
