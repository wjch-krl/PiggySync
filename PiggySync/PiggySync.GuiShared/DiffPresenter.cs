using System;

namespace PiggySync.GuiShared
{
	public class DiffPresenter
	{
		IDiffView view;
		public DiffPresenter (IDiffView view)
		{
			this.view = view;
		}
	}
}

