using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HammerUtilities.Extensions;

public static class StringX
{
	public static float TryParseFloat( this string text, float fallback = 0.0f )
	{
		if ( float.TryParse( text, out float value ) )
		{
			return value;
		}

		return fallback;
	}
}
