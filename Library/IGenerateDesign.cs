using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    interface IGenerateDesign
    {
        OutputHTML GenerateThumbnail();
        OutputHTML GenerateDesign();
        OutputHTML GenerateDesign(Page refPage);
        OutputHTML GenerateDesign(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint);
        OutputHTML GenerateDesign(Page refPage, List<MasterObject> objects, ParentConstraint parentConstraint);
        OutputHTML GenerateDesign(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint);
    }

    interface IGenerateProduction
    {
        OutputHTML GenerateProduction();
        OutputHTML GenerateProduction(Page refPage);
        OutputHTML GenerateProduction(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint);
        OutputHTML GenerateProduction(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint);
    }
}
