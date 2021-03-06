﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UXFramework
{
    /// <summary>
    /// Interface to render with a specific implementation
    /// It exists just one implementation : WebRenderer generates HTML Pages
    /// </summary>
    public interface IUXRenderer
    {
        /// <summary>
        /// Render a window
        /// </summary>
        /// <param name="window">window to render</param>
        void RenderControl(UXWindow window);
        /// <summary>
        /// Render a box
        /// </summary>
        /// <param name="box">box to render</param>
        void RenderControl(UXBox box);
        /// <summary>
        /// Render a button
        /// </summary>
        /// <param name="button">button to render</param>
        void RenderControl(UXButton button);
        /// <summary>
        /// Render a checkbox
        /// </summary>
        /// <param name="check">checkbox to render</param>
        void RenderControl(UXCheck check);
        /// <summary>
        /// Render a clickable image
        /// </summary>
        /// <param name="image">clickable image</param>
        void RenderControl(UXClickableImage image);
        /// <summary>
        /// Render a clickable text
        /// </summary>
        /// <param name="image">clickable text</param>
        void RenderControl(UXClickableText image);
        /// <summary>
        /// Render a drop-down list
        /// </summary>
        /// <param name="combo">drop-down list</param>
        void RenderControl(UXCombo combo);
        /// <summary>
        /// Render an editable text
        /// </summary>
        /// <param name="editText">text to render</param>
        void RenderControl(UXEditableText editText);
        /// <summary>
        /// Render a frame
        /// </summary>
        /// <param name="frame">frame to render</param>
        void RenderControl(UXFrame frame);
        /// <summary>
        /// Render an image
        /// </summary>
        /// <param name="frame">image to render</param>
        void RenderControl(UXImage image);
        /// <summary>
        /// Render a label
        /// </summary>
        /// <param name="text">text to render</param>
        void RenderControl(UXReadOnlyText text);
        /// <summary>
        /// Render a table
        /// </summary>
        /// <param name="table">table to render</param>
        void RenderControl(UXTable table);
        /// <summary>
        /// Render a data table view
        /// </summary>
        /// <param name="view">view to render</param>
        void RenderControl(UXViewDataTable view);
        /// <summary>
        /// Render a selectable data table view
        /// </summary>
        /// <param name="view">view to render</param>
        void RenderControl(UXViewSelectableDataTable view);
        /// <summary>
        /// Render object
        /// </summary>
        /// <param name="obj">object</param>
        void RenderControl(IUXObject obj);

    }
}
