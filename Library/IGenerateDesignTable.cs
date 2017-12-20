using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    interface IGenerateDesignTable
    {
        OutputHTML GenerateDesignTable(Page refPage);
        OutputHTML GenerateDesignTable(Page refPage, List<MasterObject> objects, ParentConstraint parentConstraint);
        OutputHTML GenerateDesignTable(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint);
        OutputHTML GenerateDesignTable(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint);
    }

    interface IGenerateProductionTable
    {
        OutputHTML GenerateProductionTable(Page refPage);
        OutputHTML GenerateProductionTable(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint);
        OutputHTML GenerateProductionTable(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint);
    }
}
