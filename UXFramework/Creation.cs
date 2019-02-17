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
        public static Marshalling.MarshallingHash NewCommonUXProps(string id, CommonProperties cp)
        {

            return Marshalling.MarshallingHash.CreateMarshalling(id, () =>
            {
                return new List<KeyValuePair<string, dynamic>>() {
                    new KeyValuePair<string, dynamic>("properties", cp)
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
        public static Marshalling.MarshallingHash NewTextUXProps(string id, TextProperties tp)
        {

            return Marshalling.MarshallingHash.CreateMarshalling(id, () =>
            {
                return new List<KeyValuePair<string, dynamic>>() {
                    new KeyValuePair<string, dynamic>("properties", tp)
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
        public static UXReadOnlyText NewUXReadOnlyText(string id, string text, CommonProperties cp)
        {
            Marshalling.MarshallingHash data = Marshalling.MarshallingHash.CreateMarshalling(id, () =>
            {
                return new List<KeyValuePair<string, dynamic>>() {
                    new KeyValuePair<string, dynamic>("Id", id),
                    new KeyValuePair<string, dynamic>("Text", text)
                }.AsEnumerable();
            });

            Marshalling.MarshallingHash ui = NewCommonUXProps(id, cp);

            return UXReadOnlyText.CreateUXReadOnlyText(data, ui);
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
        public static UXClickableText NewUXClickableText(string id, string text, string rollText, string clickText, TextProperties tp)
        {
            tp.InitialText = text;
            tp.RollText = rollText;
            tp.ClickText = clickText;
            Marshalling.MarshallingHash data = Marshalling.MarshallingHash.CreateMarshalling(id, () =>
            {
                return new List<KeyValuePair<string, dynamic>>() {
                    new KeyValuePair<string, dynamic>("Id", id),
                    new KeyValuePair<string, dynamic>("Text", text)
                }.AsEnumerable();
            });

            Marshalling.MarshallingHash ui = NewTextUXProps(id, tp);

            return UXClickableText.CreateUXClickableText(data, ui);
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
        public static UXEditableText NewUXEditableText(string id, string text, string rollText, string clickText, TextProperties tp)
        {
            tp.InitialText = text;
            tp.RollText = rollText;
            tp.ClickText = clickText;
            Marshalling.MarshallingHash data = Marshalling.MarshallingHash.CreateMarshalling(id, () =>
            {
                return new List<KeyValuePair<string, dynamic>>() {
                    new KeyValuePair<string, dynamic>("Id", id),
                    new KeyValuePair<string, dynamic>("Text", text)
                }.AsEnumerable();
            });

            Marshalling.MarshallingHash ui = NewTextUXProps(id, tp);

            return UXEditableText.CreateUXEditableText(data, ui);
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
        public static UXEditableText NewUXSelectableText(string id, string text, uint refIndex, TextProperties tp)
        {
            Marshalling.MarshallingHash data = Marshalling.MarshallingHash.CreateMarshalling(id, () =>
            {
                return new List<KeyValuePair<string, dynamic>>() {
                    new KeyValuePair<string, dynamic>("Id", id),
                    new KeyValuePair<string, dynamic>("Text", text),
                    new KeyValuePair<string, dynamic>("RefIndex", refIndex)
                }.AsEnumerable();
            });

            Marshalling.MarshallingHash ui = NewTextUXProps(id, tp);

            return UXEditableText.CreateUXEditableText(data, ui);
        }

        public static UXTable NewUXTable(string id, uint lineCount, uint columnCount, CommonProperties cp, IEnumerable<IUXObject> obj)
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

            Marshalling.MarshallingHash ui = NewCommonUXProps(id, cp);

            return UXTable.CreateUXTable(hash, ui);

        }

        public static UXRow CreateRow(string id, uint rowIndex, uint columnCount, CommonProperties cp, IEnumerable<IUXObject> obj)
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

            Marshalling.MarshallingHash ui = NewCommonUXProps(id, cp);

            return UXRow.CreateUXRow(hash, ui);
        }

        public static UXCell CreateCell(string id, int rowIndex, int colIndex, CommonProperties cp, IUXObject obj)
        {
            Marshalling.MarshallingHash hash = Marshalling.MarshallingHash.CreateMarshalling(id, () =>
            {
                return new List<KeyValuePair<string, dynamic>>() {
                    new KeyValuePair<string, dynamic>("Id", id),
                    new KeyValuePair<string, dynamic>("RowIndex", rowIndex),
                    new KeyValuePair<string, dynamic>("CellIndex", colIndex),
                    new KeyValuePair<string, dynamic>("Content", obj)
                }.AsEnumerable();
            });

            Marshalling.MarshallingHash ui = NewCommonUXProps(id, cp);

            return UXCell.CreateUXCell(hash, ui);

        }

        public static UXWindow NewUXWindow(string id, string title, CommonProperties cp, IUXObject obj) {

            Marshalling.MarshallingHash hash = Marshalling.MarshallingHash.CreateMarshalling(id, () =>
            {
                return new List<KeyValuePair<string, dynamic>>() {
                    new KeyValuePair<string, dynamic>("Id", id)
                }.AsEnumerable();
            });

            Marshalling.MarshallingHash ui = NewCommonUXProps(id, cp);

            UXWindow win = UXWindow.CreateUXWindow(id, hash, ui);
            win.Add(obj);
            return win;

        }
    
    }
}
