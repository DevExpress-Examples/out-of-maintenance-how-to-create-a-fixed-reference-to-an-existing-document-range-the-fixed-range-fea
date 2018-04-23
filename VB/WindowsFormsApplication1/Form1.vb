Imports Microsoft.VisualBasic
#Region "#usings"
Imports System
Imports System.Drawing
Imports DevExpress.XtraRichEdit.API.Native
#End Region ' #usings

Namespace WindowsFormsApplication1
	Partial Public Class Form1
		Inherits DevExpress.XtraBars.Ribbon.RibbonForm
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			richEditControl1.CreateNewDocument()
		End Sub

		Private Sub barButtonItem1_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles barButtonItem1.ItemClick
'			#Region "#FixedRangeUse"
			Dim document As Document = richEditControl1.Document
			document.AppendText("First text block without formatting. ")
			Dim formattedRangeBold = document.AppendText("Second text block with bold font. ").GetFixedRange()
			document.AppendText("Third text block without formatting. ")
			Dim formattedRangeUnderlined = document.AppendText("Fourth text block with underlined font. ").GetFixedRange()

			Dim charsBold = document.BeginUpdateCharacters(formattedRangeBold)
			charsBold.Bold = True
			document.EndUpdateCharacters(charsBold)

			Dim charsUnderline = document.BeginUpdateCharacters(formattedRangeUnderlined)
			charsUnderline.Underline = UnderlineType.Single
			charsUnderline.UnderlineColor = Color.Brown
			document.EndUpdateCharacters(charsUnderline)
'			#End Region ' #FixedRangeUse
		End Sub
	End Class
#Region "#FixedRangeExtension"
	Public Module FixedRangeExtension
        Sub New()
        End Sub
        <System.Runtime.CompilerServices.Extension> _
        Public Function GetFixedRange(ByVal range As DocumentRange) As FixedRange
            Return New FixedRange(range)
        End Function
        <System.Runtime.CompilerServices.Extension> _
        Public Function BeginUpdateCharacters(ByVal document As Document, ByVal range As FixedRange) As CharacterProperties
            Return document.BeginUpdateCharacters(range.CreateRange(document))
        End Function
	End Module
	Public Class FixedRange
		Private start As Integer
		Private length As Integer
		Public Sub New(ByVal range As DocumentRange)
			Me.start = range.Start.ToInt()
			Me.length = range.Length
		End Sub
		Public Function CreateRange(ByVal document As Document) As DocumentRange
			Return document.CreateRange(start, length)
		End Function
	End Class
#End Region ' #FixedRangeExtension
End Namespace
