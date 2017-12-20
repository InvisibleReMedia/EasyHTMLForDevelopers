using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    interface IGenerateDesignDIV
    {
        OutputHTML GenerateDesignDIV();
        OutputHTML GenerateDesignDIV(Page refPage);
        OutputHTML GenerateDesignDIV(Page refPage, List<MasterObject> objects, ParentConstraint parentConstraint);
        OutputHTML GenerateDesignDIV(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint);
        OutputHTML GenerateDesignDIV(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint);
    }

    interface IGenerateProductionDIV
    {
        OutputHTML GenerateProductionDIV(Page refPage);
        OutputHTML GenerateProductionDIV(Page refPage, MasterPage masterRefPage, ParentConstraint parentConstraint);
        OutputHTML GenerateProductionDIV(Page refPage, MasterPage masterRefPage, List<MasterObject> objects, ParentConstraint parentConstraint);
    }
}
