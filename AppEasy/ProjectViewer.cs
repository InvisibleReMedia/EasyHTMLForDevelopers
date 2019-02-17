
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UXFramework;

namespace AppEasy
{
    public static class ProjectViewer
    {

        public static UXTable CreateProjectTableHeaders()
        {

            List<string> headers = new List<string>() {
                "Title", "CreationDate", "ModificationDate", "MasterPages count", "MasterObjects count", "Pages count", "Tools count", "Revision"
            };

            List<UXCell> cells = new List<UXCell>();
            for(int index = 0; index < 8; ++index) {
                UXReadOnlyText ux = Creation.NewUXReadOnlyText("text" + index.ToString(), headers[index], new CommonProperties());
                cells.Add(Creation.CreateCell("splash." + index.ToString(), 0, index + 1, new CommonProperties(), ux));
            }

            UXRow r = Creation.CreateRow("splash.0", 0, 10, new CommonProperties(), cells);
            List<UXRow> rows = new List<UXRow>();
            rows.Add(r);
            return Creation.NewUXTable("project", 10, 8, new CommonProperties(), rows);

        }
            
    }
}
