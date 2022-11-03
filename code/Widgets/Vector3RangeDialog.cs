using Tools;

namespace HammerUtilities.Widgets;

public class Vector3RangeDialog : Dialog
{
	private Action<Vector3Range>? OnSuccess;

	public Button Okay { get; private set; }
	public Button Cancel { get; private set; }

	public Label LabelX { get; private set; }
	public LineEdit LineEditFromX { get; private set; }
	public LineEdit LineEditToX { get; private set; }

	public Label LabelY { get; private set; }
	public LineEdit LineEditFromY { get; private set; }
	public LineEdit LineEditToY { get; private set; }

	public Label LabelZ { get; private set; }
	public LineEdit LineEditFromZ { get; private set; }
	public LineEdit LineEditToZ { get; private set; }

	private bool IsRotation;

	public static void AskRange( Action<Vector3Range> onSuccess, string okay = "Okay", string cancel = "Cancel" )
	{
		// init window
		var dialog = new Vector3RangeDialog();
		dialog.IsRotation = false;
		dialog.Window.SetWindowIcon( "rotate_left" );
		dialog.Window.Title = "Random Range";
		dialog.Window.Size = new Vector2( 350f, 100f );

		dialog.OnSuccess = onSuccess;
		dialog.Okay.Text = okay;
		dialog.Cancel.Text = cancel;

		// default text for input
		dialog.LabelX.Text = "X";
		dialog.LineEditFromX.PlaceholderText = "0";
		dialog.LineEditToX.PlaceholderText = "1";

		dialog.LabelY.Text = "Y";
		dialog.LineEditFromY.PlaceholderText = "0";
		dialog.LineEditToY.PlaceholderText = "1";

		dialog.LabelZ.Text = "Z";
		dialog.LineEditFromZ.PlaceholderText = "0";
		dialog.LineEditToZ.PlaceholderText = "1";

		// focus and select first input field
		dialog.LineEditFromX.Focus();
		dialog.LineEditFromX.SelectAll();

		dialog.Show();
	}

	public static void AskOffsetRange( Action<Vector3Range> onSuccess, string okay = "Okay", string cancel = "Cancel" )
	{
		// init window
		var dialog = new Vector3RangeDialog();
		dialog.IsRotation = true;
		dialog.Window.SetWindowIcon( "open_with" );
		dialog.Window.Title = "Offset Range";
		dialog.Window.Size = new Vector2( 350f, 100f );

		dialog.OnSuccess = onSuccess;
		dialog.Okay.Text = okay;
		dialog.Cancel.Text = cancel;

		// default text for input
		dialog.LabelX.Text = "X";
		dialog.LineEditFromX.PlaceholderText = "0";
		dialog.LineEditToX.PlaceholderText = "0";

		dialog.LabelY.Text = "Y";
		dialog.LineEditFromY.PlaceholderText = "0";
		dialog.LineEditToY.PlaceholderText = "0";

		dialog.LabelZ.Text = "Z";
		dialog.LineEditFromZ.PlaceholderText = "0";
		dialog.LineEditToZ.PlaceholderText = "0";

		// focus and select first input field
		dialog.LineEditFromX.Focus();
		dialog.LineEditFromX.SelectAll();

		dialog.Show();
	}

	public static void AskRotationRange( Action<Vector3Range> onSuccess, string okay = "Okay", string cancel = "Cancel" )
	{
		// init window
		var dialog = new Vector3RangeDialog();
		dialog.IsRotation = true;
		dialog.Window.SetWindowIcon( "rotate_left" );
		dialog.Window.Title = "Random Range";
		dialog.Window.Size = new Vector2( 350f, 100f );

		dialog.OnSuccess = onSuccess;
		dialog.Okay.Text = okay;
		dialog.Cancel.Text = cancel;

		// default text for input
		dialog.LabelX.Text = "Pitch";
		dialog.LineEditFromX.PlaceholderText = "0";
		dialog.LineEditToX.PlaceholderText = "0";

		dialog.LabelY.Text = "Yaw";
		dialog.LineEditFromY.PlaceholderText = "0";
		dialog.LineEditToY.PlaceholderText = "0";

		dialog.LabelZ.Text = "Roll";
		dialog.LineEditFromZ.PlaceholderText = "0";
		dialog.LineEditToZ.PlaceholderText = "0";

		// focus and select first input field
		dialog.LineEditFromX.Focus();
		dialog.LineEditFromX.SelectAll();

		dialog.Show();
	}

	private float FromX => TryParseFloat( LineEditFromX.Text );
	private float ToX => TryParseFloat( LineEditToX.Text, IsRotation ? 0.0f : 1.0f );
	private float FromY => TryParseFloat( LineEditFromY.Text );
	private float ToY => TryParseFloat( LineEditToY.Text, IsRotation ? 0.0f : 1.0f );
	private float FromZ => TryParseFloat( LineEditFromZ.Text );
	private float ToZ => TryParseFloat( LineEditToZ.Text, IsRotation ? 0.0f : 1.0f );

	private float TryParseFloat( string text, float fallback = 0.0f )
	{
		if ( float.TryParse( text, out float value ) )
		{
			return value;
		}

		return fallback;
	}

	private void Finish()
	{
		Close();
		OnSuccess?.Invoke( new Vector3Range( FromX, ToX, FromY, ToY, FromZ, ToZ ) );
	}

	private Vector3RangeDialog()
	{
		Window.SetModal( on: true, application: true );

		CreateElements();

		SetLayout( LayoutMode.TopToBottom );

		CreateLayout();
	}

	private void CreateElements()
	{
		// create inputs

		// X
		LabelX = new Label( this );
		// From
		LineEditFromX = new LineEdit( this );
		LineEditFromX.ReturnPressed += Finish;
		// To
		LineEditToX = new LineEdit( this );
		LineEditToX.ReturnPressed += Finish;

		// Y
		LabelY = new Label( this );
		// From
		LineEditFromY = new LineEdit( this );
		LineEditFromY.ReturnPressed += Finish;
		// To
		LineEditToY = new LineEdit( this );
		LineEditToY.ReturnPressed += Finish;

		// Z
		LabelZ = new Label( this );
		// From
		LineEditFromZ = new LineEdit( this );
		LineEditFromZ.ReturnPressed += Finish;
		// To
		LineEditToZ = new LineEdit( this );
		LineEditToZ.ReturnPressed += Finish;

		Okay = new Button( "Okay", this );
		Okay.SetProperty( "type", "primary" );
		Okay.Clicked = Finish;
		Cancel = new Button( "Cancel", this );
		Cancel.Clicked = Close;
	}

	private void CreateLayout()
	{
		// main input layout
		Layout layout = Layout.Add( LayoutMode.TopToBottom );
		layout.Margin = 16f;

		var rangeSpacing = 4f;
		var rowSpacing = 16f;

		// x input
		var inputLayoutX = layout.Add( LayoutMode.LeftToRight );
		inputLayoutX.Add( LabelX );
		inputLayoutX.AddStretchCell();
		inputLayoutX.Add( LineEditFromX );
		inputLayoutX.AddSpacingCell( rangeSpacing );
		inputLayoutX.Add( LineEditToX );

		layout.AddSpacingCell( rowSpacing );

		// x input
		var inputLayoutY = layout.Add( LayoutMode.LeftToRight );
		inputLayoutY.Add( LabelY );
		inputLayoutY.AddStretchCell();
		inputLayoutY.Add( LineEditFromY );
		inputLayoutY.AddSpacingCell( rangeSpacing );
		inputLayoutY.Add( LineEditToY );

		layout.AddSpacingCell( rowSpacing );

		// x input
		var inputLayoutZ = layout.Add( LayoutMode.LeftToRight );
		inputLayoutZ.Add( LabelZ );
		inputLayoutZ.AddStretchCell();
		inputLayoutZ.Add( LineEditFromZ );
		inputLayoutZ.AddSpacingCell( rangeSpacing );
		inputLayoutZ.Add( LineEditToZ );

		// add bottom okay and cancel buttons
		Layout buttons = Layout.Add( LayoutMode.LeftToRight );
		buttons.Margin = rowSpacing;
		buttons.Spacing = 8f;
		buttons.AddStretchCell();
		buttons.Add( Okay );
		buttons.Add( Cancel );
	}
}
