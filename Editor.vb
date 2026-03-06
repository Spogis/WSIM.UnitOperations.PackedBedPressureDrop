Imports System.Windows.Forms
Imports DWSIM.ExtensionMethods
Imports DWSIM
Imports DWSIM.UnitOperations

Public Class Editor

    Inherits WeifenLuo.WinFormsUI.Docking.DockContent

    Public SimObject As PackedBedPressureDrop

    Public Loaded As Boolean = False

    Dim units As SharedClasses.SystemsOfUnits.Units
    Dim nf As String

    Private ceditor As DWSIM.SharedClassesCSharp.ConnectionsEditor.ConnectionsEditor

    Private Sub Editor_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' Connections editor
        ceditor = New DWSIM.SharedClassesCSharp.ConnectionsEditor.ConnectionsEditor()
        ceditor.SimObject = SimObject
        ceditor.Dock = Windows.Forms.DockStyle.Fill
        Panel1.Controls.Add(ceditor)

        ' Populate equation dropdown
        cbEquation.Items.Clear()
        cbEquation.Items.AddRange({"Ergun", "McDonald et al.", "Gamane & Lannoy", "Tallmadge", "Kozeny-Carman"})

        ' Populate fluid phase dropdown
        cbFluidPhase.Items.Clear()
        cbFluidPhase.Items.AddRange({"Auto (detect)", "Vapor", "Liquid", "Overall"})

        UpdateInfo()

        ChangeDefaultFont()

    End Sub

    Public Sub UpdateInfo()

        units = SimObject.FlowSheet.FlowsheetOptions.SelectedUnitSystem
        nf = SimObject.FlowSheet.FlowsheetOptions.NumberFormat

        Loaded = False

        ceditor.UpdateInfo()

        With SimObject

            ' === Information section ===

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

            ' === Property Package ===

            Dim proppacks As String() = .GetFlowsheet().PropertyPackages.Values.Select(Function(m) m.Tag).ToArray
            cbPropPack.Items.Clear()
            cbPropPack.Items.AddRange(proppacks)
            cbPropPack.SelectedItem = .PropertyPackage?.Tag

            ' === Bed Geometry fields (read from InputParameters) ===

            txtBedDiameter.Text = GetInputValue("Bed Diameter")
            txtBedHeight.Text = GetInputValue("Bed Height")
            txtParticleDiameter.Text = GetInputValue("Particle Diameter")
            txtVoidFraction.Text = GetInputValue("Void Fraction")
            txtSphericity.Text = GetInputValue("Sphericity")
            txtSolidDensity.Text = GetInputValue("Solid Density")

            ' === Calculation Options ===

            Dim eqIdx As Integer = CInt(GetInputValueRaw("Equation"))
            If eqIdx >= 0 AndAlso eqIdx < cbEquation.Items.Count Then
                cbEquation.SelectedIndex = eqIdx
            Else
                cbEquation.SelectedIndex = 0
            End If

            Dim phIdx As Integer = CInt(GetInputValueRaw("Fluid Phase"))
            If phIdx >= 0 AndAlso phIdx < cbFluidPhase.Items.Count Then
                cbFluidPhase.SelectedIndex = phIdx
            Else
                cbFluidPhase.SelectedIndex = 0
            End If

            chkWallCorrection.Checked = (GetInputValueRaw("Wall Correction") > 0.5)

            ' === Results grid ===

            gridOutput.Rows.Clear()

            If .Calculated Then
                AddResultRow("Pressure Drop", "Pressure Drop", "Pa")
                AddResultRow("Reynolds Number", "Reynolds Number", "")
                AddResultRow("Superficial Velocity", "Superficial Velocity", "m/s")
                AddResultRow("Friction Factor", "Friction Factor", "")
                AddResultRow("Bed Cross-Section Area", "Bed Cross-Section Area", "m²")
                AddResultRow("Archimedes Number", "Archimedes Number", "")
                AddResultRow("Re at Min. Fluidization", "Re at Min. Fluidization", "")
                AddResultRow("Min. Fluidization Velocity", "Min. Fluidization Velocity", "m/s")
                AddResultRow("Fluid Density", "Fluid Density", "kg/m³")
                AddResultRow("Fluid Viscosity", "Fluid Viscosity", "Pa.s")
                AddResultRow("Inlet Pressure", "Inlet Pressure", "Pa")
                AddResultRow("Outlet Pressure", "Outlet Pressure", "Pa")

                ' Phase used (special: show name instead of number)
                Dim phaseVal As Double = GetOutputValueRaw("Phase Used")
                Dim phaseName As String = "N/A"
                Select Case CInt(phaseVal)
                    Case 1 : phaseName = "Vapor"
                    Case 2 : phaseName = "Liquid"
                    Case 3 : phaseName = "Overall Liquid"
                    Case 4 : phaseName = "Overall"
                End Select
                gridOutput.Rows.Add(New Object() {"Phase Used", phaseName, ""})
            End If

        End With

        Loaded = True

    End Sub

#Region "Helper Methods"

    ''' <summary>Gets a formatted string value from InputParameters</summary>
    Private Function GetInputValue(key As String) As String
        If SimObject.InputParameters.ContainsKey(key) Then
            Return CDbl(SimObject.InputParameters(key).Value).ToString(nf)
        End If
        Return "0"
    End Function

    ''' <summary>Gets raw Double value from InputParameters</summary>
    Private Function GetInputValueRaw(key As String) As Double
        If SimObject.InputParameters.ContainsKey(key) Then
            Return CDbl(SimObject.InputParameters(key).Value)
        End If
        Return 0.0
    End Function

    ''' <summary>Gets raw Double value from OutputParameters</summary>
    Private Function GetOutputValueRaw(key As String) As Double
        If SimObject.OutputParameters.ContainsKey(key) Then
            Return CDbl(SimObject.OutputParameters(key).Value)
        End If
        Return 0.0
    End Function

    ''' <summary>Adds a row to the results grid from OutputParameters</summary>
    Private Sub AddResultRow(paramKey As String, displayName As String, unit As String)
        If SimObject.OutputParameters.ContainsKey(paramKey) Then
            Dim val As Double = CDbl(SimObject.OutputParameters(paramKey).Value)
            gridOutput.Rows.Add(New Object() {displayName, val.ToString(nf), unit})
        End If
    End Sub

    ''' <summary>Safely writes a value to InputParameters from a TextBox</summary>
    Private Sub SetInputFromTextBox(key As String, txt As TextBox)
        If SimObject.InputParameters.ContainsKey(key) Then
            Dim val As Double = 0.0
            If Double.TryParse(txt.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, val) Then
                SimObject.InputParameters(key).Value = val
            ElseIf Double.TryParse(txt.Text, Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, val) Then
                SimObject.InputParameters(key).Value = val
            End If
        End If
    End Sub

#End Region

#Region "Event Handlers - Information"

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

#End Region

#Region "Event Handlers - Bed Geometry TextBoxes"

    Private Sub txtBedDiameter_Leave(sender As Object, e As EventArgs) Handles txtBedDiameter.Leave
        If Loaded Then
            SimObject.FlowSheet.RegisterSnapshot(Interfaces.Enums.SnapshotType.ObjectData, SimObject)
            SetInputFromTextBox("Bed Diameter", txtBedDiameter)
        End If
    End Sub

    Private Sub txtBedHeight_Leave(sender As Object, e As EventArgs) Handles txtBedHeight.Leave
        If Loaded Then
            SimObject.FlowSheet.RegisterSnapshot(Interfaces.Enums.SnapshotType.ObjectData, SimObject)
            SetInputFromTextBox("Bed Height", txtBedHeight)
        End If
    End Sub

    Private Sub txtParticleDiameter_Leave(sender As Object, e As EventArgs) Handles txtParticleDiameter.Leave
        If Loaded Then
            SimObject.FlowSheet.RegisterSnapshot(Interfaces.Enums.SnapshotType.ObjectData, SimObject)
            SetInputFromTextBox("Particle Diameter", txtParticleDiameter)
        End If
    End Sub

    Private Sub txtVoidFraction_Leave(sender As Object, e As EventArgs) Handles txtVoidFraction.Leave
        If Loaded Then
            SimObject.FlowSheet.RegisterSnapshot(Interfaces.Enums.SnapshotType.ObjectData, SimObject)
            SetInputFromTextBox("Void Fraction", txtVoidFraction)
        End If
    End Sub

    Private Sub txtSphericity_Leave(sender As Object, e As EventArgs) Handles txtSphericity.Leave
        If Loaded Then
            SimObject.FlowSheet.RegisterSnapshot(Interfaces.Enums.SnapshotType.ObjectData, SimObject)
            SetInputFromTextBox("Sphericity", txtSphericity)
        End If
    End Sub

    Private Sub txtSolidDensity_Leave(sender As Object, e As EventArgs) Handles txtSolidDensity.Leave
        If Loaded Then
            SimObject.FlowSheet.RegisterSnapshot(Interfaces.Enums.SnapshotType.ObjectData, SimObject)
            SetInputFromTextBox("Solid Density", txtSolidDensity)
        End If
    End Sub

#End Region

#Region "Event Handlers - Calculation Options"

    Private Sub cbEquation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbEquation.SelectedIndexChanged
        If Loaded Then
            SimObject.FlowSheet.RegisterSnapshot(Interfaces.Enums.SnapshotType.ObjectData, SimObject)
            If SimObject.InputParameters.ContainsKey("Equation") Then
                SimObject.InputParameters("Equation").Value = CDbl(cbEquation.SelectedIndex)
            End If
        End If
    End Sub

    Private Sub cbFluidPhase_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFluidPhase.SelectedIndexChanged
        If Loaded Then
            SimObject.FlowSheet.RegisterSnapshot(Interfaces.Enums.SnapshotType.ObjectData, SimObject)
            If SimObject.InputParameters.ContainsKey("Fluid Phase") Then
                SimObject.InputParameters("Fluid Phase").Value = CDbl(cbFluidPhase.SelectedIndex)
            End If
        End If
    End Sub

    Private Sub chkWallCorrection_CheckedChanged(sender As Object, e As EventArgs) Handles chkWallCorrection.CheckedChanged
        If Loaded Then
            SimObject.FlowSheet.RegisterSnapshot(Interfaces.Enums.SnapshotType.ObjectData, SimObject)
            If SimObject.InputParameters.ContainsKey("Wall Correction") Then
                SimObject.InputParameters("Wall Correction").Value = If(chkWallCorrection.Checked, 1.0, 0.0)
            End If
        End If
    End Sub

#End Region

End Class