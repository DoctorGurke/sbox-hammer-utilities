namespace HammerUtilities.Widgets;

public class RangeDialog : Dialog
{
	private Action<Vector2>? OnSuccess;

	public Button Okay { get; private set; }
	public Button Cancel { get; private set; }

	public Label Label { get; private set; }
	public LineEdit LineEditFrom { get; private set; }
	public LineEdit LineEditTo { get; private set; }


	public static void AskRange( Action<Vector2> onSuccess, string okay = "Okay", string cancel = "Cancel" )
	{
		// init window
		var dialog = new RangeDialog();
		dialog.Window.SetWindowIcon( "code" );
		dialog.Window.Title = "Enter Range";
		dialog.Window.Size = new Vector2( 350f, 100f );

		dialog.OnSuccess = onSuccess;
		dialog.Okay.Text = okay;
		dialog.Cancel.Text = cancel;

		// default text for input
		dialog.Label.Text = "Range";
		dialog.LineEditFrom.PlaceholderText = "0";
		dialog.LineEditTo.PlaceholderText = "1";

		// focus and select first input field
		dialog.LineEditFrom.Focus();
		dialog.LineEditFrom.SelectAll();

		dialog.Show();
	}

	private float From => LineEditFrom.Text.TryParseFloat();
	private float To => LineEditTo.Text.TryParseFloat( 1.0f );

	private void Finish()
	{
		Close();
		OnSuccess?.Invoke( new Vector2( From, To ) );
	}

	private RangeDialog()
	{
		Window.SetModal( on: true, application: true );

		CreateElements();

		SetLayout( LayoutMode.TopToBottom );

		CreateLayout();
	}

	private void CreateElements()
	{
		// create inputs
		Label = new Label( this );
		LineEditFrom = new LineEdit( this );
		LineEditFrom.ReturnPressed += Finish;
		LineEditTo = new LineEdit( this );
		LineEditTo.ReturnPressed += Finish;

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

		// input
		var input = layout.Add( LayoutMode.LeftToRight );
		input.Add( Label );
		input.AddStretchCell();
		input.Add( LineEditFrom );
		input.AddSpacingCell( 4f );
		input.Add( LineEditTo );

		// add bottom okay and cancel buttons
		Layout buttons = Layout.Add( LayoutMode.LeftToRight );
		buttons.Margin = spacing;
		buttons.Spacing = 8f;
		buttons.AddStretchCell();
		buttons.Add( Okay );
		buttons.Add( Cancel );
	}
}
