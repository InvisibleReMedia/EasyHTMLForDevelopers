using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public interface IContainer
    {
        EnumConstraint ConstraintWidth { get; set; }
        EnumConstraint ConstraintHeight { get; set; }
        uint Width { get; set; }
        uint Height { get; set; }
        string Name { get; set; }
        bool SearchContainer(List<IContainer> containers, List<IContent> objects, string searchName, out IContainer found);
    }
}
