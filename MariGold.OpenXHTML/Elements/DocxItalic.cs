﻿namespace MariGold.OpenXHTML
{
    using System;
    using MariGold.HtmlParser;
    using DocumentFormat.OpenXml;
    using DocumentFormat.OpenXml.Wordprocessing;

    internal sealed class DocxItalic : DocxElement, ITextElement
    {
        private void SetStyle(DocxNode node)
        {
            string value = node.ExtractStyleValue(DocxFontStyle.italic);

            if (string.IsNullOrEmpty(value))
            {
                node.SetExtentedStyle(DocxFontStyle.fontStyle, DocxFontStyle.italic);
            }
        }

        internal DocxItalic(IOpenXmlContext context)
            : base(context)
        {
        }

        internal override bool CanConvert(DocxNode node)
        {
            return string.Compare(node.Tag, "i", StringComparison.InvariantCultureIgnoreCase) == 0 ||
            string.Compare(node.Tag, "em", StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        internal override void Process(DocxNode node, ref Paragraph paragraph)
        {
            if (node.IsNull() || IsHidden(node))
            {
                return;
            }

            SetStyle(node);

            ProcessElement(node, ref paragraph);
        }

        bool ITextElement.CanConvert(DocxNode node)
        {
            return CanConvert(node);
        }

        void ITextElement.Process(DocxNode node)
        {
            if (IsHidden(node))
            {
                return;
            }

            SetStyle(node);
            ProcessTextChild(node);
        }
    }
}
