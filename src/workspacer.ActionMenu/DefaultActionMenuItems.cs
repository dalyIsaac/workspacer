using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workspacer.ActionMenu
{
    public static class DefaultActionMenuItems
    {
        public static ActionMenuItemBuilder CreateSwitchToWindowMenu(ActionMenuPlugin actionMenu,IConfigContext context)
        {
            var builder = actionMenu.Create();
            var workspaces = context.WorkspaceContainer.GetAllWorkspaces();
            foreach (var workspace in workspaces)
            {
                foreach (var window in workspace.ManagedWindows)
                {
                    var text = $"[{workspace.Name}] {window.Title}";
                    builder.Add(text, () => context.Workspaces.SwitchToWindow(window));
                }
            }
            return builder;
        }

        public static ActionMenuItemBuilder CreateSwitchToLayoutMenu(ActionMenuPlugin actionMenu,IConfigContext context)
        {
            var builder = actionMenu.Create();
            var focusedWorkspace = context.Workspaces.FocusedWorkspace;

            Func<int, Action> createChildMenu = (index) => () =>
            {
                focusedWorkspace.SwitchLayoutEngineToIndex(index);
            };

            var layoutNames = focusedWorkspace.LayoutEngineNames;
            int idx = 0;
            foreach (var name in layoutNames)
            {
                builder.Add(name, createChildMenu(idx));
                idx++;
            }

            return builder;
        }

    }
}
