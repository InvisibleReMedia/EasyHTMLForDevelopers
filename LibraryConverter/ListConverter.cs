using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryConverter
{
    public class ListConverter : Marshalling.MarshallingList
    {

        public ListConverter()
            : base("List")
        {

        }

        public static ListConverter ProjectFiles(DirectoryInfo di)
        {
            ListConverter listConverter = new ListConverter();
            listConverter.Add(() =>
            {
                List<HashConverter> list = new List<HashConverter>();
                uint index = 0;
                foreach (FileInfo fi in di.GetFiles("*.bin"))
                {
                    Marshalling.PersistentDataObject obj;
                    if (Library.Project.Load(fi, out obj))
                    {
                        Library.Project proj = obj as Library.Project;
                        if (proj != null)
                        {
                            HashConverter h = HashConverter.ProjectItems(proj);
                            h.Set("name", index.ToString());
                            h.Set("project", proj);
                            list.Add(h);
                            ++index;
                        }
                    }
                }
                return list;
            });
            return listConverter;
        }

        public static UXFramework.UXTable MakeUXProjectFiles(DirectoryInfo di) {

            ListConverter list = ProjectFiles(di);
            List<UXFramework.UXRow> rows = new List<UXFramework.UXRow>();

            UXFramework.UXRow header = HashConverter.MakeUXHeaderProjectItems(list[0] as HashConverter);
            rows.Add(header);
            uint index = 0;
            foreach(HashConverter hash in list) {
                UXFramework.UXRow row = HashConverter.MakeUXProjectItems(hash);
                row.Add( () => {
                    return new Dictionary<string, dynamic>() {
                        { "Id", index.ToString() }
                    };
                });
                rows.Add(row);
                ++index;
            }
            return UXFramework.Creation.CreateTable("table", Convert.ToUInt32(list[0].Values.Count()), Convert.ToUInt32(list.Count), null, rows.ToArray());

        }


        public static ListConverter MakeUXhierarchyProject(Library.Project p, Library.Node<string, Library.Accessor> node)
        {
            ListConverter listConverter = new ListConverter();
            listConverter.Add(() =>
            {
                List<HashConverter> list = new List<HashConverter>();
                list.Add(HashConverter.HierarchyItems(p, node));
                return list;
            });
            return listConverter;
        }

    }
}
