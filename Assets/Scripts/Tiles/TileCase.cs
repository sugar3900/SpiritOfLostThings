namespace GGJ
{
	public enum TileCase : byte
	{
		Empty = 0,
		Corner = 1,
		Corner90 = 2,
		Corner180 = 4,
		Corner270 = 8,
		Side = 3,
		Side90 = 6,
		Side180 = 12,
		Side270 = 9,
		Diagonal = 5,
		Diagonal90 = 10,
		InnerCorner = 7,
		InnerCorner90 = 14,
		InnerCorner180 = 13,
		InnerCorner270 = 11,
		Solid = 15
	}

	public static class TileCaseExtensions
	{
		public static float GetRotationZ(this TileCase tileCase)
		{
			switch(tileCase)
			{
				default:
					return 0f;
				case TileCase.Corner90:
				case TileCase.Side90:
				case TileCase.Diagonal90:
				case TileCase.InnerCorner90:
					return 90f;
				case TileCase.Corner180:
				case TileCase.Side180:
				case TileCase.InnerCorner180:
					return 180f;
				case TileCase.Corner270:
				case TileCase.Side270:
				case TileCase.InnerCorner270:
					return 270f;
			}
		}
	}
}
