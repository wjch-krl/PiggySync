using System;
using Gtk;

public partial class MainWindow: Gtk.Window
{
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
		InititializeComponetnts ();
	}

	void InititializeComponetnts ()
	{
		this.rootSyncPath.Text = System.Environment.CurrentDirectory;
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnForceSyncButtonClicked (object sender, EventArgs e)
	{
		throw new NotImplementedException ();
	}

	protected void OnAutoSyncCheckBoxToggled (object sender, EventArgs e)
	{
		throw new NotImplementedException ();
	}

	protected void OnChooseFolderButtonClicked (object sender, EventArgs e)
	{
		using (var chooser = new FileChooserDialog ("", this, FileChooserAction.SelectFolder, "Cancel", ResponseType.Cancel,
			                     "Open", ResponseType.Accept)) {
			if (chooser.Run() == (int)ResponseType.Accept) 
			{
				this.rootSyncPath.Text = chooser.Filename;
			}
			chooser.Destroy ();
		}
	}
}
