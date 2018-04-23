#region #usings
using System;
using System.Drawing;
using DevExpress.XtraRichEdit.API.Native;
#endregion #usings

namespace WindowsFormsApplication1 {
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            richEditControl1.CreateNewDocument();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            #region #FixedRangeUse
            Document document = richEditControl1.Document;
            document.AppendText("First text block without formatting. ");
            var formattedRangeBold = document.AppendText("Second text block with bold font. ").GetFixedRange();
            document.AppendText("Third text block without formatting. ");
            var formattedRangeUnderlined = document.AppendText("Fourth text block with underlined font. ").GetFixedRange();

            var charsBold = document.BeginUpdateCharacters(formattedRangeBold);
            charsBold.Bold = true;
            document.EndUpdateCharacters(charsBold);

            var charsUnderline = document.BeginUpdateCharacters(formattedRangeUnderlined);
            charsUnderline.Underline = UnderlineType.Single;
            charsUnderline.UnderlineColor = Color.Brown;
            document.EndUpdateCharacters(charsUnderline);
            #endregion #FixedRangeUse
        }
    }
#region #FixedRangeExtension
    public static class FixedRangeExtension {
        public static FixedRange GetFixedRange(this DocumentRange range) {
            return new FixedRange(range);
        }
        public static CharacterProperties BeginUpdateCharacters
            (this Document document, FixedRange range) {
            return document.BeginUpdateCharacters(range.CreateRange(document));
        }
    }
    public class FixedRange {
        int start;
        int length;
        public FixedRange(DocumentRange range) {
            this.start = range.Start.ToInt();
            this.length = range.Length;
        }
        public DocumentRange CreateRange(Document document)
        {
            return document.CreateRange(start, length);
        }
    }
#endregion #FixedRangeExtension
}
