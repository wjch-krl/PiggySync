using System;

namespace PiggySync.GtkGui
{
    internal class ActionGroups
    {
        public static Gtk.ActionGroup GetActionGroup(Type type)
        {
            return GetActionGroup(type.FullName);
        }

        public static Gtk.ActionGroup GetActionGroup(string name)
        {
            return null;
        }
    }
}