using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UXFramework
{
    public class UXTree : UXControl
    {
        #region Fields


        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="id">id</param>
        public UXTree(string id)
        {
            this.Name = id;
        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        /// <summary>
        /// Bind sequence
        /// </summary>
        /// <param name="a">specific bind function</param>
        public virtual void Bind(Marshalling.IMarshalling m)
        {
            if (m is Marshalling.MarshallingList)
            {
                Marshalling.MarshallingList list = m as Marshalling.MarshallingList;
                // construction de la table
                uint count = Convert.ToUInt32(list.Count);
                if (count > 0)
                {
                    for (int index = 0; index < count; ++index)
                    {
                        string title = (list[index].Value as List<Marshalling.IMarshalling>).ElementAt(0).Value;
                        dynamic sub = (list[index].Value as List<Marshalling.IMarshalling>).ElementAt(1);
                        UXTreeItem r = new UXTreeItem(title, sub);
                        this.Add(r);
                    }
                }
            }
        }

        #endregion
    }
}
