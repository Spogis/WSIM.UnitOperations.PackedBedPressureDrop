Imports System.Windows.Forms
Imports DWSIM.ExtensionMethods
Imports DWSIM
Imports DWSIM.UnitOperations

Public Class Editor

    Inherits WeifenLuo.WinFormsUI.Docking.DockContent

    Public SimObject As PackedBedPressureDrop

    Public Loaded As Boolean = False

    Dim units As SharedClasses.SystemsOfUnits.Units
    Dim xunits As XFlowsheet.Implementation.DefaultImplementations.UnitsOfMeasure
    Dim nf As String

    Private ceditor As DWSIM.SharedClassesCSharp.ConnectionsEditor.ConnectionsEditor

    Private Sub Editor_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ceditor = New DWSIM.SharedClassesCSharp.ConnectionsEditor.ConnectionsEditor()
        ceditor.SimObject = SimObject
        ceditor.Dock = Windows.Forms.DockStyle.Fill

        Panel1.Controls.Add(ceditor)

        UpdateInfo()

        ChangeDefaultFont()

    End Sub

    Public Sub UpdateInfo()

        units = SimObject.FlowSheet.FlowsheetOptions.SelectedUnitSystem
        nf = SimObject.FlowSheet.FlowsheetOptions.NumberFormat

        Loaded = False

        ceditor.UpdateInfo()

        With SimObject

            'first block

            chkActive.Checked = .GraphicObject.Active

            Me.Text = .GraphicObject.Tag & " (" & .GetDisplayName() & ")"

            lblTag.Text = .GraphicObject.Tag
            If .Calculated Then
                lblStatus.Text = .GetFlowsheet().GetTranslatedString("Calculado") & " (" & .LastUpdated.ToString & ")"
                lblStatus.ForeColor = System.Drawing.Color.Blue
            Else
                If Not .GraphicObject.Active Then
                    lblStatus.Text = .GetFlowsheet().GetTranslatedString("Inativo")
                    lblStatus.ForeColor = System.Drawing.Color.Gray
                ElseIf .ErrorMessage <> "" Then
                    lblStatus.Text = .GetFlowsheet().GetTranslatedString("Erro")
                    lblStatus.ForeColor = System.Drawing.Color.Red
                Else
                    lblStatus.Text = .GetFlowsheet().GetTranslatedString("NoCalculado")
                    lblStatus.ForeColor = System.Drawing.Color.Black
                End If
            End If

            lblConnectedTo.Text = ""

            If .IsSpecAttached Then lblConnectedTo.Text = .GetFlowsheet().SimulationObjects(.AttachedSpecId).GraphicObject.Tag
            If .IsAdjustAttached Then lblConnectedTo.Text = .GetFlowsheet().SimulationObjects(.AttachedAdjustId).GraphicObject.Tag

            'property package

            Dim proppacks As String() = .GetFlowsheet().PropertyPackages.Values.Select(Function(m) m.Tag).ToArray
            cbPropPack.Items.Clear()
            cbPropPack.Items.AddRange(proppacks)
            cbPropPack.SelectedItem = .PropertyPackage?.Tag

            'input parameters

            gridInput.Rows.Clear()
            For Each item In .InputParameters.Values
                gridInput.Rows.Add(New Object() {item.Name, item.Description, item.Value.ToString(nf)})
            Next

            'output parameters

            gridOutput.Rows.Clear()

            If .Calculated Then
                For Each item In .OutputParameters.Values
                    gridOutput.Rows.Add(New Object() {item.Name, item.Description, item.Value.ToString(nf)})
                Next

            End If

        End With

        Loaded = True

    End Sub

    Private Sub cbPropPack_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbPropPack.SelectedIndexChanged

        If Loaded Then
            SimObject.GetFlowsheet().RegisterSnapshot(Interfaces.Enums.SnapshotType.ObjectData, SimObject)
            SimObject.PropertyPackage = SimObject.GetFlowsheet().PropertyPackages.Values.Where(Function(x) x.Tag = cbPropPack.SelectedItem.ToString).SingleOrDefault
        End If

    End Sub

    Private Sub btnConfigurePP_Click(sender As Object, e As EventArgs) Handles btnConfigurePP.Click

        SimObject.GetFlowsheet().PropertyPackages.Values.Where(Function(x) x.Tag = cbPropPack.SelectedItem.ToString).FirstOrDefault()?.DisplayGroupedEditingForm()

    End Sub

    Private Sub chkActive_CheckedChanged(sender As Object, e As EventArgs) Handles chkActive.CheckedChanged

        If Loaded Then
            SimObject.GraphicObject.Active = chkActive.Checked
            SimObject.GetFlowsheet().UpdateInterface()
            UpdateInfo()
        End If

    End Sub

    Private Sub lblTag_TextChanged(sender As Object, e As EventArgs) Handles lblTag.TextChanged

        If Loaded Then ToolTipChangeTag.Show("Press ENTER to commit changes.", lblTag, New System.Drawing.Point(0, lblTag.Height + 3), 3000)

    End Sub

    Private Sub lblTag_KeyUp(sender As Object, e As Windows.Forms.KeyEventArgs) Handles lblTag.KeyUp

        If e.KeyCode = Keys.Enter Then

            If Loaded Then SimObject.GetFlowsheet().RegisterSnapshot(Interfaces.Enums.SnapshotType.ObjectLayout)

            If Loaded Then SimObject.GraphicObject.Tag = lblTag.Text
            If Loaded Then SimObject.GetFlowsheet().UpdateOpenEditForms()
            Me.Text = SimObject.GraphicObject.Tag & " (" & SimObject.GetDisplayName() & ")"
            DirectCast(SimObject.GetFlowsheet(), Interfaces.IFlowsheetGUI).UpdateInterface()

        End If

    End Sub

    Private Sub gridInput_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles gridInput.CellValueChanged

        If Loaded Then

            SimObject.FlowSheet.RegisterSnapshot(Interfaces.Enums.SnapshotType.ObjectData, SimObject)

            Try

                Dim prop = gridInput.Rows(e.RowIndex).Cells(0).Value
                Dim value = gridInput.Rows(e.RowIndex).Cells(2).Value

                SimObject.InputParameters(prop).Value = value

            Catch ex As Exception

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            End Try

        End If

    End Sub

End Class