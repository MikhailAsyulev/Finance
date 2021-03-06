﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BooleanTemplate.cs" company="ZigZag">
//   Copyright © ZigZag 2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Common.Grid.RenderTemplates
{
    using System.Reflection;
    using Html;

    /// <summary>
    /// Template for cell with checkbox
    /// </summary>
    public class BooleanTemplate : BaseTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanTemplate"/> class.
        /// </summary>
        /// <param name="property">Entity property</param>
        /// <param name="format">Render format</param>
        public BooleanTemplate(PropertyInfo property, string format)
        {
            this.Property = property;
            this.Format = format;
        }

        /// <summary>
        /// Transfer object in html cell
        /// </summary>
        /// <param name="value">Object to render</param>
        /// <returns>Html cell</returns>
        public override HtmlTableCell Render(object value)
        {
            var isOn = (bool)Property.GetValue(value);
            var result = new HtmlTableCell();
            result.InnerHtml = isOn ? "Да" : "Нет";
            return result;
        }
    }
}

