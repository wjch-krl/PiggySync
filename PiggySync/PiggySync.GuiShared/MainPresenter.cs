﻿using System;

namespace PiggySync.GuiShared
{
	public class MainPresenter
	{
		IMainView view;

		public MainPresenter (IMainView view)
		{
			this.view = view;
		}
	}
}

