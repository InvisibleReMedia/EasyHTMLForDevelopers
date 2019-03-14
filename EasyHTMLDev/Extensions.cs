using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyHTMLDev
{
    public static class ExtensionForms
    {
        public static void RegisterControls(this System.Windows.Forms.Control control, ref int id, params object[] pars)
        {
            id = Localization.Strings.RelativeWindow(control, pars);
        }

        public static void UnregisterControls(this System.Windows.Forms.Control control, ref int id)
        {
            Localization.Strings.FreeWindow(ref id);
        }

        public static void UpdateText(this System.Windows.Forms.Control control, int id, string name, params object[] pars)
        {
            Localization.Strings.Instances[id].OneReBindControl(control, name, pars);
        }

        /// <summary>
        /// TreeNode collection to descendants
        /// </summary>
        /// <param name="c">tree node collection</param>
        /// <returns>elements in a list</returns>
        internal static IEnumerable<TreeNode> Descendants(this TreeNodeCollection c)
        {
            foreach (var node in c.OfType<TreeNode>())
            {
                yield return node;

                foreach (var child in node.Nodes.Descendants())
                {
                    yield return child;
                }
            }
        }
    }
}
