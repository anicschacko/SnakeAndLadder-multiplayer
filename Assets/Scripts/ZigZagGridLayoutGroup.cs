using UnityEngine;
using UnityEngine.UI;

public class ZigZagGridLayoutGroup : GridLayoutGroup
{
	public bool zigzag = true; // Toggle to enable or disable zigzag pattern

	protected override void OnEnable()
	{
		base.OnEnable();
		CalculateLayoutInputHorizontal();
	}

	public override void CalculateLayoutInputHorizontal()
	{
		base.CalculateLayoutInputHorizontal();

		if (!zigzag)
			return;

		int itemCount = transform.childCount;
		if (itemCount == 0)
			return;

		int constraintCount = 1;
		constraintCount = constraint == Constraint.FixedRowCount ? constraintCount : itemCount / constraintCount + Mathf.Min(1, itemCount % constraintCount);
		int rowSize = constraint == Constraint.FixedRowCount ? constraintCount : itemCount / constraintCount;

		float width = rectTransform.rect.width;
		float height = rectTransform.rect.height;

		float cellWidth = (width - (padding.left + padding.right + (spacing.x * (constraintCount - 1)))) / constraintCount;
		float cellHeight = (height - (padding.top + padding.bottom + (spacing.y * (rowSize - 1)))) / rowSize;

		for (int i = 0; i < itemCount; i++)
		{
			int row = i / constraintCount;
			int column = i % constraintCount;

			if (zigzag && row % 2 != 0)
				column = constraintCount - 1 - column;

			float xPos = padding.left + (cellWidth + spacing.x) * column;
			float yPos = padding.top + (cellHeight + spacing.y) * row;

			SetChildAlongAxis(transform.GetChild(i) as RectTransform, 0, xPos, cellWidth);
			SetChildAlongAxis(transform.GetChild(i) as RectTransform, 1, yPos, cellHeight);
		}
	}

	public override void CalculateLayoutInputVertical()
	{
		// Do nothing, as we only care about horizontal layout for the zigzag pattern.
	}
}