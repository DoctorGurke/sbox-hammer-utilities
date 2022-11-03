

namespace HammerUtilities.Widgets;

public class TransformDialog : Dialog
{
	private Action<Vector3>? OnSuccess;

	public Button Okay { get; private set; }
	public Button Cancel { get; private set; }

	public Label LabelX { get; private set; }
	public LineEdit LineEditX { get; private set; }

	public Label LabelY { get; private set; }
	public LineEdit LineEditY { get; private set; }

	public Label LabelZ { get; private set; }
	public LineEdit LineEditZ { get; private set; }


	public static void AskTranslate( Action<Vector3> onSuccess, string okay = "Okay", string cancel = "Cancel" )
	{
		// init window
		var dialog = new TransformDialog();
		dialog.Window.SetWindowIcon( "open_with" );
		dialog.Window.Title = "Translate Selection";
		dialog.Window.Size = new Vector2( 200f, 100f );

		dialog.OnSuccess = onSuccess;
		dialog.Okay.Text = okay;
		dialog.Cancel.Text = cancel;

		// default text for input
		dialog.LabelX.Text = "X";
		dialog.LineEditX.PlaceholderText = "0";
		dialog.LabelY.Text = "Y";
		dialog.LineEditY.PlaceholderText = "0";
		dialog.LabelZ.Text = "Z";
		dialog.LineEditZ.PlaceholderText = "0";

		// focus and select first input field
		dialog.LineEditX.Focus();
		dialog.LineEditX.SelectAll();

		dialog.Show();
	}

	public static void AskRotate( Action<Vector3> onSuccess, string okay = "Okay", string cancel = "Cancel" )
	{
		// init window
		var dialog = new TransformDialog();
		dialog.Window.SetWindowIcon( "rotate_left" );
		dialog.Window.Title = "Rotate Selection";
		dialog.Window.Size = new Vector2( 200f, 100f );

		dialog.OnSuccess = onSuccess;
		dialog.Okay.Text = okay;
		dialog.Cancel.Text = cancel;

		// default text for input
		dialog.LabelX.Text = "Pitch";
		dialog.LineEditX.PlaceholderText = "0";
		dialog.LabelY.Text = "Yaw";
		dialog.LineEditY.PlaceholderText = "0";
		dialog.LabelZ.Text = "Roll";
		dialog.LineEditZ.PlaceholderText = "0";

		// focus and select first input field
		dialog.LineEditX.Focus();
		dialog.LineEditX.SelectAll();

		dialog.Show();
	}

	public static void AskScale( Action<Vector3> onSuccess, string okay = "Okay", string cancel = "Cancel" )
	{
		// init window
		var dialog = new TransformDialog();
		dialog.Window.SetWindowIcon( "open_in_full" );
		dialog.Window.Title = "Scale Selection";
		dialog.Window.Size = new Vector2( 200f, 100f );

		dialog.OnSuccess = onSuccess;
		dialog.Okay.Text = okay;
		dialog.Cancel.Text = cancel;

		// default text for input
		dialog.LabelX.Text = "X";
		dialog.LineEditX.PlaceholderText = "1";
		dialog.LabelY.Text = "Y";
		dialog.LineEditY.PlaceholderText = "1";
		dialog.LabelZ.Text = "Z";
		dialog.LineEditZ.PlaceholderText = "1";

		// focus and select first input field
		dialog.LineEditX.Focus();
		dialog.LineEditX.SelectAll();

		dialog.Show();
	}

	private float X => LineEditX.Text.TryParseFloat();
	private float Y => LineEditY.Text.TryParseFloat();
	private float Z => LineEditZ.Text.TryParseFloat();

	private void Finish()
	{
		string text = LineEditX.Text;
		Close();
		OnSuccess?.Invoke( new Vector3( X, Y, Z ) );
	}

	private TransformDialog()
	{
		Window.SetModal( on: true, application: true );

		CreateElements();

		SetLayout( LayoutMode.TopToBottom );

		CreateLayout();
	}

	private void CreateElements()
	{
		// create inputs
		LabelX = new Label( this );
		LineEditX = new LineEdit( this );
		LabelY = new Label( this );
		LineEditY = new LineEdit( this );
		LabelZ = new Label( this );
		LineEditZ = new LineEdit( this );

		// allow Finish on enter for last input
		LineEditZ.ReturnPressed += Finish;

		Okay = new Button( "Okay", this );
		Okay.SetProperty( "type", "primary" );
		Okay.Clicked = Finish;
		Cancel = new Button( "Cancel", this );
		Cancel.Clicked = Close;
	}

	private void CreateLayout()
	{
		var spacing = 16f;

		// main input layout
		Layout layout = Layout.Add( LayoutMode.TopToBottom );
		layout.Margin = spacing;

		// x input
		var inputLayoutX = layout.Add( LayoutMode.LeftToRight );
		inputLayoutX.Add( LabelX );
		inputLayoutX.AddStretchCell();
		inputLayoutX.Add( LineEditX );

		layout.AddSpacingCell( spacing );

		// y input
		var inputLayoutY = layout.Add( LayoutMode.LeftToRight );
		inputLayoutY.Add( LabelY );
		inputLayoutY.AddStretchCell();
		inputLayoutY.Add( LineEditY );

		layout.AddSpacingCell( spacing );

		// z input
		var inputLayoutZ = layout.Add( LayoutMode.LeftToRight );
		inputLayoutZ.Add( LabelZ );
		inputLayoutZ.AddStretchCell();
		inputLayoutZ.Add( LineEditZ );

		// add bottom okay and cancel buttons
		Layout buttons = Layout.Add( LayoutMode.LeftToRight );
		buttons.Margin = spacing;
		buttons.Spacing = 8f;
		buttons.AddStretchCell();
		buttons.Add( Okay );
		buttons.Add( Cancel );
	}
}
