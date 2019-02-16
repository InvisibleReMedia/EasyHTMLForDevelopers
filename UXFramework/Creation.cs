using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UXFramework
{
    public static class Creation
    {

        /// <summary>
        /// Create common graphics properties
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="width">width</param>
        /// <param name="height">height</param>
        /// <param name="backColor">back color</param>
        /// <param name="foreColor">fore color</param>
        /// <param name="border">border</param>
        /// <param name="margin">margin</param>
        /// <param name="padding">padding</param>
        /// <returns>hash</returns>
        public static Marshalling.MarshallingHash NewCommonUXProps(string id, int width, int height, string backColor, string foreColor,
                                                                string border, string margin, string padding)
        {

            return Marshalling.MarshallingHash.CreateMarshalling(id, () =>
            {
                return new List<KeyValuePair<string, dynamic>>() {
                    new KeyValuePair<string, dynamic>("Width", width),
                    new KeyValuePair<string, dynamic>("Height", height),
                    new KeyValuePair<string, dynamic>("BackColor", backColor),
                    new KeyValuePair<string, dynamic>("ForeColor", foreColor),
                    new KeyValuePair<string, dynamic>("Border", border),
                    new KeyValuePair<string, dynamic>("Margin", margin),
                    new KeyValuePair<string, dynamic>("Padding", padding)
                }.AsEnumerable();
            });
        }

        /// <summary>
        /// Create text graphics properties
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="width">width</param>
        /// <param name="height">height</param>
        /// <param name="backColor">back color</param>
        /// <param name="foreColor">fore color</param>
        /// <param name="border">border</param>
        /// <param name="margin">margin</param>
        /// <param name="padding">padding</param>
        /// <param name="rollBackColor">roll back color</param>
        /// <param name="rollColor">roll color</param>
        /// <param name="clickBorderColor">click border color</param>
        /// <returns></returns>
        public static Marshalling.MarshallingHash NewTextUXProps(string id, int width, int height, string backColor, string foreColor,
                                                                 string border, string margin, string padding, string rollBackColor,
                                                                 string rollColor, string clickBorderColor)
        {

            return Marshalling.MarshallingHash.CreateMarshalling(id, () =>
            {
                return new List<KeyValuePair<string, dynamic>>() {
                    new KeyValuePair<string, dynamic>("Width", width),
                    new KeyValuePair<string, dynamic>("Height", height),
                    new KeyValuePair<string, dynamic>("BackColor", backColor),
                    new KeyValuePair<string, dynamic>("ForeColor", foreColor),
                    new KeyValuePair<string, dynamic>("Border", border),
                    new KeyValuePair<string, dynamic>("Margin", margin),
                    new KeyValuePair<string, dynamic>("Padding", padding),
                    new KeyValuePair<string, dynamic>("RollBackColor", rollBackColor),
                    new KeyValuePair<string, dynamic>("RollColor", rollColor),
                    new KeyValuePair<string, dynamic>("ClickBorderColor", clickBorderColor)

                }.AsEnumerable();
            });
        }

        /// <summary>
        /// Create UXReadOnlyText
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="text">text</param>
        /// <param name="uiProps">ui properties</param>
        /// <returns>new UX</returns>
        public static UXReadOnlyText NewUXReadOnlyText(string id, string text, Dictionary<string, dynamic> uiProps)
        {
            Marshalling.IMarshalling data = Marshalling.MarshallingHash.CreateMarshalling(id, () =>
            {
                return new List<KeyValuePair<string, dynamic>>() {
                    new KeyValuePair<string, dynamic>("Id", id),
                    new KeyValuePair<string, dynamic>("Text", text)
                }.AsEnumerable();
            });

            Marshalling.IMarshalling ui = NewCommonUXProps(id, uiProps["Width"], uiProps["Height"], uiProps["BackColor"],
                                                           uiProps["ForeColor"], uiProps["Border"], uiProps["Margin"], uiProps["Padding"]);

            return UXReadOnlyText.CreateUXReadOnlyText(data as Marshalling.MarshallingHash, ui as Marshalling.MarshallingHash);
        }

        /// <summary>
        /// Create UXClickableText
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="text">text</param>
        /// <param name="rollText">roll text</param>
        /// <param name="clickText">click text</param>
        /// <param name="uiProps">ui properties</param>
        /// <param name="parent">parent UX</param>
        /// <returns>new UX</returns>
        public static UXClickableText NewUXClickableText(string id, string text, string rollText, string clickText, Dictionary<string, dynamic> uiProps)
        {
            Marshalling.IMarshalling data = Marshalling.MarshallingHash.CreateMarshalling(id, () =>
            {
                return new List<KeyValuePair<string, dynamic>>() {
                    new KeyValuePair<string, dynamic>("Id", id),
                    new KeyValuePair<string, dynamic>("Text", text),
                    new KeyValuePair<string, dynamic>("ClickText", clickText),
                    new KeyValuePair<string, dynamic>("RollText", rollText)
                }.AsEnumerable();
            });

            Marshalling.IMarshalling ui = NewTextUXProps(id, uiProps["Width"], uiProps["Height"],
                                                         uiProps["BackColor"], uiProps["ForeColor"],
                                                         uiProps["Border"], uiProps["Margin"], uiProps["Padding"],
                                                         uiProps["RollBackColor"], uiProps["RollColor"], uiProps["ClickBorderColor"]);

            return UXClickableText.CreateUXClickableText(data as Marshalling.MarshallingHash, ui as Marshalling.MarshallingHash);
        }

        /// <summary>
        /// Create UXEditableText
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="text">text</param>
        /// <param name="rollText">roll text</param>
        /// <param name="clickText">click text</param>
        /// <param name="uiProps">ui properties</param>
        /// <param name="parent">parent UX</param>
        /// <returns>new UX</returns>
        public static UXEditableText NewUXEditableText(string id, string text, string rollText, string clickText, Dictionary<string, dynamic> uiProps)
        {
            Marshalling.IMarshalling data = Marshalling.MarshallingHash.CreateMarshalling(id, () =>
            {
                return new List<KeyValuePair<string, dynamic>>() {
                    new KeyValuePair<string, dynamic>("Id", id),
                    new KeyValuePair<string, dynamic>("Text", text),
                    new KeyValuePair<string, dynamic>("ClickText", clickText),
                    new KeyValuePair<string, dynamic>("RollText", rollText)
                }.AsEnumerable();
            });

            Marshalling.IMarshalling ui = NewTextUXProps(id, uiProps["Width"], uiProps["Height"],
                                                         uiProps["BackColor"], uiProps["ForeColor"],
                                                         uiProps["Border"], uiProps["Margin"], uiProps["Padding"],
                                                         uiProps["RollBackColor"], uiProps["RollColor"], uiProps["ClickBorderColor"]);

            return UXEditableText.CreateUXEditableText(data as Marshalling.MarshallingHash, ui as Marshalling.MarshallingHash);
        }

        /// <summary>
        /// Create UXSelectableText
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="text">text</param>
        /// <param name="rollText">roll text</param>
        /// <param name="clickText">click text</param>
        /// <param name="uiProps">ui properties</param>
        /// <param name="parent">parent UX</param>
        /// <returns>new UX</returns>
        public static UXEditableText NewUXSelectableText(string id, string text, uint refIndex, Dictionary<string, dynamic> uiProps)
        {
            Marshalling.IMarshalling data = Marshalling.MarshallingHash.CreateMarshalling(id, () =>
            {
                return new List<KeyValuePair<string, dynamic>>() {
                    new KeyValuePair<string, dynamic>("Id", id),
                    new KeyValuePair<string, dynamic>("Text", text),
                    new KeyValuePair<string, dynamic>("RefIndex", refIndex)
                }.AsEnumerable();
            });

            Marshalling.IMarshalling ui = NewTextUXProps(id, uiProps["Width"], uiProps["Height"],
                                                         uiProps["BackColor"], uiProps["ForeColor"],
                                                         uiProps["Border"], uiProps["Margin"], uiProps["Padding"],
                                                         uiProps["RollBackColor"], uiProps["RollColor"], uiProps["ClickBorderColor"]);

            return UXEditableText.CreateUXEditableText(data as Marshalling.MarshallingHash, ui as Marshalling.MarshallingHash);
        }

        public static UXTable NewUXTable(string id, uint lineCount, uint columnCount, Dictionary<string, dynamic> uiProps, IEnumerable<IUXObject> obj)
        {
            Marshalling.MarshallingHash hash = Marshalling.MarshallingHash.CreateMarshalling(id, () =>
            {
                return new List<KeyValuePair<string, dynamic>>() {
                    new KeyValuePair<string, dynamic>("Id", id),
                    new KeyValuePair<string, dynamic>("LineCount", lineCount),
                    new KeyValuePair<string, dynamic>("ColumnCount", columnCount),
                    new KeyValuePair<string, dynamic>("Childs", obj)
                }.AsEnumerable();
            });

            Marshalling.MarshallingHash ui = NewCommonUXProps(id, uiProps["Width"], uiProps["Height"], uiProps["BackColor"],
                                                           uiProps["ForeColor"], uiProps["Border"], uiProps["Margin"], uiProps["Padding"]);

            return UXTable.CreateUXTable(hash, ui);

        }

        public static UXRow CreateRow(string id, uint rowIndex, uint columnCount, Dictionary<string, dynamic> uiProps, IEnumerable<IUXObject> obj)
        {
            Marshalling.MarshallingHash hash = Marshalling.MarshallingHash.CreateMarshalling(id, () =>
            {
                return new List<KeyValuePair<string, dynamic>>() {
                    new KeyValuePair<string, dynamic>("Id", id),
                    new KeyValuePair<string, dynamic>("RowIndex", rowIndex),
                    new KeyValuePair<string, dynamic>("ColumnCount", columnCount),
                    new KeyValuePair<string, dynamic>("Childs", obj)
                }.AsEnumerable();
            });

            Marshalling.MarshallingHash ui = NewCommonUXProps(id, uiProps["Width"], uiProps["Height"], uiProps["BackColor"],
                                                           uiProps["ForeColor"], uiProps["Border"], uiProps["Margin"], uiProps["Padding"]);

            return UXRow.CreateUXRow(hash, ui);
        }

        public static UXCell CreateCell(string id, uint rowIndex, uint colIndex, Dictionary<string, dynamic> uiProps, IUXObject obj)
        {
            Marshalling.MarshallingHash hash = Marshalling.MarshallingHash.CreateMarshalling(id, () =>
            {
                return new List<KeyValuePair<string, dynamic>>() {
                    new KeyValuePair<string, dynamic>("Id", id),
                    new KeyValuePair<string, dynamic>("RowIndex", rowIndex),
                    new KeyValuePair<string, dynamic>("ColIndex", colIndex),
                    new KeyValuePair<string, dynamic>("Content", obj)
                }.AsEnumerable();
            });

            Marshalling.MarshallingHash ui = NewCommonUXProps(id, uiProps["Width"], uiProps["Height"], uiProps["BackColor"],
                                                           uiProps["ForeColor"], uiProps["Border"], uiProps["Margin"], uiProps["Padding"]);

            return UXCell.CreateUXCell(hash, ui);

        }

        public static UXWindow NewUXWindow(string id, string title, Dictionary<string, dynamic> uiProps, IUXObject obj) {

            Marshalling.MarshallingHash hash = Marshalling.MarshallingHash.CreateMarshalling(id, () =>
            {
                return new List<KeyValuePair<string, dynamic>>() {
                    new KeyValuePair<string, dynamic>("Id", id)
                }.AsEnumerable();
            });

            Marshalling.MarshallingHash ui = NewCommonUXProps(id, uiProps["Width"], uiProps["Height"], uiProps["BackColor"],
                                                              uiProps["ForeColor"], uiProps["Border"], uiProps["Margin"], uiProps["Padding"]);

            UXWindow win = UXWindow.CreateUXWindow(id, hash, ui);
            win.Add(obj);
            return win;

        }
    
    }
}
