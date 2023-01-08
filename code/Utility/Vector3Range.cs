using static Editor.Label;

namespace HammerUtilities.Utility;

public struct Vector3Range
{
	public float FromX;
	public float FromY;
	public float FromZ;

	public float ToX;
	public float ToY;
	public float ToZ;

	public Vector3Range( float fromAll, float toAll )
	{
		FromX = fromAll;
		FromY = fromAll;
		FromZ = fromAll;

		ToX = toAll;
		ToY = toAll;
		ToZ = toAll;
	}

	public Vector3Range( float fromX, float toX, float fromY, float toY, float fromZ, float toZ )
	{
		FromX = fromX;
		FromY = fromY;
		FromZ = fromZ;

		ToX = toX;
		ToY = toY;
		ToZ = toZ;
	}

	public Vector3 RandomVector => new Vector3( Random.Shared.Float( FromX, ToX ), Random.Shared.Float( FromY, ToY ), Random.Shared.Float( FromZ, ToZ ) );
}
