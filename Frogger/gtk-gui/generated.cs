
// This file has been generated by the GUI designer. Do not modify.
namespace ChrisJones.Frogger
{
	internal class Gui
	{
		private static bool initialized;

		internal static void Initialize (Gtk.Widget iconRenderer)
		{
			if ((Gui.initialized == false)) {
				Gui.initialized = true;
			}
		}
	}

	internal class ActionGroups
	{
		public static Gtk.ActionGroup GetActionGroup (System.Type type)
		{
			return ActionGroups.GetActionGroup (type.FullName);
		}

		public static Gtk.ActionGroup GetActionGroup (string name)
		{
			return null;
		}
	}
}
