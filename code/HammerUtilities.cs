global using HammerUtilities.Extensions;
global using HammerUtilities.Utility;
global using Sandbox;
global using SandboxEditor;
global using System;
global using System.Linq;
global using Tools;
global using Tools.Editor;
global using Tools.MapDoc;
global using Tools.MapEditor;
using HammerUtilities.Widgets;

namespace HammerUtilities;

public static class HammerUtilities
{
	//[Menu( "Hammer", "Hammer Utils/Transform/Translate", "open_with" )]
	//public static void MenuBarTranslate()
	//{
	//	Log.Info( $"translate" );
	//}

	//[Menu( "Hammer", "Hammer Utils/Transform/Rotate", "rotate_left" )]
	//public static void MenuBarRotate()
	//{
	//	Log.Info( $"rotate" );
	//}

	//[Menu( "Hammer", "Hammer Utils/Transform/Scale", "open_in_full" )]
	//public static void MenuBarScale()
	//{
	//	Log.Info( $"scale" );
	//}

	[Event( "hammer.mapview.contextmenu" )]
	public static void ContextMenuTransform( Menu menu, MapView view )
	{
		// no nodes selected, nothing to transform
		if ( !Selection.All.Any() )
			return;

		menu.AddSeparator();

		// transform menus
		menu.AddTranslateMenu();
		menu.AddRotateMenu();
		menu.AddScaleMenu();

		menu.AddSeparator();
	}

	private static Menu AddTranslateMenu( this Menu menu )
	{
		var translate = menu.AddMenu( "Translate", "open_with" );

		// default translate dialog
		translate.AddOption( "Translate...", "", () => { TransformDialog.AskTranslate( ( translation ) => { Selection.All.ToList().ForEach( node => node.Position += translation ); } ); } );
		translate.AddSeparator();

		// util functions
		translate.AddOption( "Offset Random...", "", () => { Vector3RangeDialog.AskOffsetRange( ( range ) => { Selection.All.ToList().ForEach( x => x.Position += range.RandomVector ); } ); } );

		return translate;
	}

	private static Menu AddRotateMenu( this Menu menu )
	{
		var rotate = menu.AddMenu( "Rotate Selection", "rotate_left" );

		// default rotate dialog
		rotate.AddOption( "Rotate...", "", () => { TransformDialog.AskRotate( ( rotation ) => { Selection.All.ToList().ForEach( node => node.Angles += rotation ); } ); } );
		rotate.AddSeparator();

		// util functions
		rotate.AddOption( "Random Pitch", "", () => Selection.All.ToList().ForEach( x => x.Angles = x.Angles.WithPitch( Rand.Int( 0, 360 ) ) ) );
		rotate.AddOption( "Random Yaw", "", () => Selection.All.ToList().ForEach( x => x.Angles = x.Angles.WithYaw( Rand.Int( 0, 360 ) ) ) );
		rotate.AddOption( "Random Roll", "", () => Selection.All.ToList().ForEach( x => x.Angles = x.Angles.WithRoll( Rand.Int( 0, 360 ) ) ) );
		rotate.AddSeparator();
		rotate.AddOption( "Rotate Random...", "", () => Vector3RangeDialog.AskRotationRange( ( range ) => { Selection.All.ToList().ForEach( x => x.Angles += range.RandomVector ); } ) );

		return rotate;
	}

	private static Menu AddScaleMenu( this Menu menu )
	{
		var scale = menu.AddMenu( "Scale Selection", "open_in_full" );

		// default rotate dialog
		scale.AddOption( "Scale...", "", () => { TransformDialog.AskScale( ( scale ) => { Selection.All.ToList().ForEach( node => node.Scale *= scale ); } ); } );
		scale.AddSeparator();

		// util functions
		scale.AddOption( "Apply Skybox Scale", "", () => { Selection.All.ToList().ForEach( node => node.Scale *= 0.0625f ); } );
		scale.AddOption( "Scale Random 0-1", "", () => Selection.All.ToList().ForEach( x => x.Scale *= Rand.Float() ) );
		scale.AddSeparator();
		scale.AddOption( "Scale Random Uniform...", "", () => RangeDialog.AskRange( ( range ) => { Selection.All.ToList().ForEach( x => x.Scale *= Rand.Float( range.x, range.y ) ); } ) );
		scale.AddOption( "Scale Random...", "", () => Vector3RangeDialog.AskRange( ( range ) => { Selection.All.ToList().ForEach( x => x.Scale *= range.RandomVector ); } ) );

		return scale;
	}
}
