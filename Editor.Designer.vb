<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Editor

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        '
        ' === GroupBox1: Information ===
        '
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblTag = New System.Windows.Forms.TextBox()
        Me.chkActive = New System.Windows.Forms.CheckBox()
        Me.lblConnectedTo = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        '
        ' === GroupBox2: Connections ===
        '
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        '
        ' === GroupBox5: Property Package ===
        '
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.btnConfigurePP = New System.Windows.Forms.Button()
        Me.cbPropPack = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        '
        ' === GroupBoxBedGeometry: Bed Geometry ===
        '
        Me.GroupBoxBedGeometry = New System.Windows.Forms.GroupBox()
        Me.lblBedDiameter = New System.Windows.Forms.Label()
        Me.txtBedDiameter = New System.Windows.Forms.TextBox()
        Me.lblBedDiameterUnit = New System.Windows.Forms.Label()
        Me.lblBedHeight = New System.Windows.Forms.Label()
        Me.txtBedHeight = New System.Windows.Forms.TextBox()
        Me.lblBedHeightUnit = New System.Windows.Forms.Label()
        Me.lblParticleDiameter = New System.Windows.Forms.Label()
        Me.txtParticleDiameter = New System.Windows.Forms.TextBox()
        Me.lblParticleDiameterUnit = New System.Windows.Forms.Label()
        Me.lblVoidFraction = New System.Windows.Forms.Label()
        Me.txtVoidFraction = New System.Windows.Forms.TextBox()
        Me.lblVoidFractionUnit = New System.Windows.Forms.Label()
        Me.lblSphericity = New System.Windows.Forms.Label()
        Me.txtSphericity = New System.Windows.Forms.TextBox()
        Me.lblSphericityUnit = New System.Windows.Forms.Label()
        Me.lblSolidDensity = New System.Windows.Forms.Label()
        Me.txtSolidDensity = New System.Windows.Forms.TextBox()
        Me.lblSolidDensityUnit = New System.Windows.Forms.Label()
        '
        ' === GroupBoxCalcOptions: Calculation Options ===
        '
        Me.GroupBoxCalcOptions = New System.Windows.Forms.GroupBox()
        Me.lblEquation = New System.Windows.Forms.Label()
        Me.cbEquation = New System.Windows.Forms.ComboBox()
        Me.lblFluidPhase = New System.Windows.Forms.Label()
        Me.cbFluidPhase = New System.Windows.Forms.ComboBox()
        Me.chkWallCorrection = New System.Windows.Forms.CheckBox()
        '
        ' === GroupBox4: Results ===
        '
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.gridOutput = New System.Windows.Forms.DataGridView()
        Me.colResultName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colResultValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colResultUnit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        '
        Me.ToolTipChangeTag = New System.Windows.Forms.ToolTip(Me.components)
        '
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBoxBedGeometry.SuspendLayout()
        Me.GroupBoxCalcOptions.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.gridOutput, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        ' =============================================
        ' GroupBox1 - Information
        ' =============================================
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.lblTag)
        Me.GroupBox1.Controls.Add(Me.chkActive)
        Me.GroupBox1.Controls.Add(Me.lblConnectedTo)
        Me.GroupBox1.Controls.Add(Me.lblStatus)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(391, 110)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Information"
        '
        'lblTag
        '
        Me.lblTag.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTag.Location = New System.Drawing.Point(140, 24)
        Me.lblTag.Name = "lblTag"
        Me.lblTag.Size = New System.Drawing.Size(236, 20)
        Me.lblTag.TabIndex = 31
        '
        'chkActive
        '
        Me.chkActive.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkActive.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkActive.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.chkActive.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.chkActive.Location = New System.Drawing.Point(355, 48)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(21, 21)
        Me.chkActive.TabIndex = 30
        Me.chkActive.UseVisualStyleBackColor = True
        '
        'lblConnectedTo
        '
        Me.lblConnectedTo.AutoSize = True
        Me.lblConnectedTo.Location = New System.Drawing.Point(139, 77)
        Me.lblConnectedTo.Name = "lblConnectedTo"
        Me.lblConnectedTo.Size = New System.Drawing.Size(38, 13)
        Me.lblConnectedTo.TabIndex = 29
        Me.lblConnectedTo.Text = ""
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(139, 52)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(38, 13)
        Me.lblStatus.TabIndex = 28
        Me.lblStatus.Text = ""
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(16, 77)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(51, 13)
        Me.Label13.TabIndex = 27
        Me.Label13.Text = "Linked to"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(16, 52)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(37, 13)
        Me.Label12.TabIndex = 26
        Me.Label12.Text = "Status"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(16, 27)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(38, 13)
        Me.Label11.TabIndex = 25
        Me.Label11.Text = "Object"
        '
        ' =============================================
        ' GroupBox2 - Connections
        ' =============================================
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.Panel1)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 120)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(391, 100)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Connections"
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 16)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(385, 81)
        Me.Panel1.TabIndex = 0
        '
        ' =============================================
        ' GroupBox5 - Property Package
        ' =============================================
        '
        Me.GroupBox5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox5.Controls.Add(Me.btnConfigurePP)
        Me.GroupBox5.Controls.Add(Me.cbPropPack)
        Me.GroupBox5.Controls.Add(Me.Label9)
        Me.GroupBox5.Location = New System.Drawing.Point(6, 226)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(391, 56)
        Me.GroupBox5.TabIndex = 2
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Property Package"
        '
        'btnConfigurePP
        '
        Me.btnConfigurePP.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnConfigurePP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnConfigurePP.Location = New System.Drawing.Point(355, 24)
        Me.btnConfigurePP.Name = "btnConfigurePP"
        Me.btnConfigurePP.Size = New System.Drawing.Size(21, 21)
        Me.btnConfigurePP.TabIndex = 23
        Me.btnConfigurePP.Text = "..."
        Me.btnConfigurePP.UseVisualStyleBackColor = True
        '
        'cbPropPack
        '
        Me.cbPropPack.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbPropPack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPropPack.FormattingEnabled = True
        Me.cbPropPack.Location = New System.Drawing.Point(128, 24)
        Me.cbPropPack.Name = "cbPropPack"
        Me.cbPropPack.Size = New System.Drawing.Size(221, 21)
        Me.cbPropPack.TabIndex = 22
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(16, 27)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(92, 13)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "Property Package"
        '
        ' =============================================
        ' GroupBoxBedGeometry - Bed Geometry & Particles
        ' =============================================
        ' Layout: Label(X=16) | TextBox(X=140, W=170, fixed) | UnitLabel(X=318)
        '
        Me.GroupBoxBedGeometry.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxBedGeometry.Controls.Add(Me.lblBedDiameter)
        Me.GroupBoxBedGeometry.Controls.Add(Me.txtBedDiameter)
        Me.GroupBoxBedGeometry.Controls.Add(Me.lblBedDiameterUnit)
        Me.GroupBoxBedGeometry.Controls.Add(Me.lblBedHeight)
        Me.GroupBoxBedGeometry.Controls.Add(Me.txtBedHeight)
        Me.GroupBoxBedGeometry.Controls.Add(Me.lblBedHeightUnit)
        Me.GroupBoxBedGeometry.Controls.Add(Me.lblParticleDiameter)
        Me.GroupBoxBedGeometry.Controls.Add(Me.txtParticleDiameter)
        Me.GroupBoxBedGeometry.Controls.Add(Me.lblParticleDiameterUnit)
        Me.GroupBoxBedGeometry.Controls.Add(Me.lblVoidFraction)
        Me.GroupBoxBedGeometry.Controls.Add(Me.txtVoidFraction)
        Me.GroupBoxBedGeometry.Controls.Add(Me.lblVoidFractionUnit)
        Me.GroupBoxBedGeometry.Controls.Add(Me.lblSphericity)
        Me.GroupBoxBedGeometry.Controls.Add(Me.txtSphericity)
        Me.GroupBoxBedGeometry.Controls.Add(Me.lblSphericityUnit)
        Me.GroupBoxBedGeometry.Controls.Add(Me.lblSolidDensity)
        Me.GroupBoxBedGeometry.Controls.Add(Me.txtSolidDensity)
        Me.GroupBoxBedGeometry.Controls.Add(Me.lblSolidDensityUnit)
        Me.GroupBoxBedGeometry.Location = New System.Drawing.Point(6, 288)
        Me.GroupBoxBedGeometry.Name = "GroupBoxBedGeometry"
        Me.GroupBoxBedGeometry.Size = New System.Drawing.Size(391, 190)
        Me.GroupBoxBedGeometry.TabIndex = 3
        Me.GroupBoxBedGeometry.TabStop = False
        Me.GroupBoxBedGeometry.Text = "Bed Geometry && Particles"
        '
        ' --- Row 1: Bed Diameter ---
        '
        Me.lblBedDiameter.AutoSize = True
        Me.lblBedDiameter.Location = New System.Drawing.Point(16, 24)
        Me.lblBedDiameter.Name = "lblBedDiameter"
        Me.lblBedDiameter.Size = New System.Drawing.Size(80, 13)
        Me.lblBedDiameter.Text = "Bed Diameter"
        '
        Me.txtBedDiameter.Location = New System.Drawing.Point(140, 21)
        Me.txtBedDiameter.Name = "txtBedDiameter"
        Me.txtBedDiameter.Size = New System.Drawing.Size(170, 20)
        Me.txtBedDiameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        Me.lblBedDiameterUnit.AutoSize = True
        Me.lblBedDiameterUnit.Location = New System.Drawing.Point(318, 24)
        Me.lblBedDiameterUnit.Name = "lblBedDiameterUnit"
        Me.lblBedDiameterUnit.Size = New System.Drawing.Size(15, 13)
        Me.lblBedDiameterUnit.Text = "m"
        Me.lblBedDiameterUnit.ForeColor = System.Drawing.Color.Gray
        '
        ' --- Row 2: Bed Height ---
        '
        Me.lblBedHeight.AutoSize = True
        Me.lblBedHeight.Location = New System.Drawing.Point(16, 51)
        Me.lblBedHeight.Name = "lblBedHeight"
        Me.lblBedHeight.Size = New System.Drawing.Size(80, 13)
        Me.lblBedHeight.Text = "Bed Height"
        '
        Me.txtBedHeight.Location = New System.Drawing.Point(140, 48)
        Me.txtBedHeight.Name = "txtBedHeight"
        Me.txtBedHeight.Size = New System.Drawing.Size(170, 20)
        Me.txtBedHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        Me.lblBedHeightUnit.AutoSize = True
        Me.lblBedHeightUnit.Location = New System.Drawing.Point(318, 51)
        Me.lblBedHeightUnit.Name = "lblBedHeightUnit"
        Me.lblBedHeightUnit.Size = New System.Drawing.Size(15, 13)
        Me.lblBedHeightUnit.Text = "m"
        Me.lblBedHeightUnit.ForeColor = System.Drawing.Color.Gray
        '
        ' --- Row 3: Particle Diameter ---
        '
        Me.lblParticleDiameter.AutoSize = True
        Me.lblParticleDiameter.Location = New System.Drawing.Point(16, 78)
        Me.lblParticleDiameter.Name = "lblParticleDiameter"
        Me.lblParticleDiameter.Size = New System.Drawing.Size(100, 13)
        Me.lblParticleDiameter.Text = "Particle Diameter"
        '
        Me.txtParticleDiameter.Location = New System.Drawing.Point(140, 75)
        Me.txtParticleDiameter.Name = "txtParticleDiameter"
        Me.txtParticleDiameter.Size = New System.Drawing.Size(170, 20)
        Me.txtParticleDiameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        Me.lblParticleDiameterUnit.AutoSize = True
        Me.lblParticleDiameterUnit.Location = New System.Drawing.Point(318, 78)
        Me.lblParticleDiameterUnit.Name = "lblParticleDiameterUnit"
        Me.lblParticleDiameterUnit.Size = New System.Drawing.Size(15, 13)
        Me.lblParticleDiameterUnit.Text = "m"
        Me.lblParticleDiameterUnit.ForeColor = System.Drawing.Color.Gray
        '
        ' --- Row 4: Void Fraction ---
        '
        Me.lblVoidFraction.AutoSize = True
        Me.lblVoidFraction.Location = New System.Drawing.Point(16, 105)
        Me.lblVoidFraction.Name = "lblVoidFraction"
        Me.lblVoidFraction.Size = New System.Drawing.Size(80, 13)
        Me.lblVoidFraction.Text = "Void Fraction"
        '
        Me.txtVoidFraction.Location = New System.Drawing.Point(140, 102)
        Me.txtVoidFraction.Name = "txtVoidFraction"
        Me.txtVoidFraction.Size = New System.Drawing.Size(170, 20)
        Me.txtVoidFraction.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        Me.lblVoidFractionUnit.AutoSize = True
        Me.lblVoidFractionUnit.Location = New System.Drawing.Point(318, 105)
        Me.lblVoidFractionUnit.Name = "lblVoidFractionUnit"
        Me.lblVoidFractionUnit.Size = New System.Drawing.Size(15, 13)
        Me.lblVoidFractionUnit.Text = "[-]"
        Me.lblVoidFractionUnit.ForeColor = System.Drawing.Color.Gray
        '
        ' --- Row 5: Sphericity ---
        '
        Me.lblSphericity.AutoSize = True
        Me.lblSphericity.Location = New System.Drawing.Point(16, 132)
        Me.lblSphericity.Name = "lblSphericity"
        Me.lblSphericity.Size = New System.Drawing.Size(80, 13)
        Me.lblSphericity.Text = "Sphericity"
        '
        Me.txtSphericity.Location = New System.Drawing.Point(140, 129)
        Me.txtSphericity.Name = "txtSphericity"
        Me.txtSphericity.Size = New System.Drawing.Size(170, 20)
        Me.txtSphericity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        Me.lblSphericityUnit.AutoSize = True
        Me.lblSphericityUnit.Location = New System.Drawing.Point(318, 132)
        Me.lblSphericityUnit.Name = "lblSphericityUnit"
        Me.lblSphericityUnit.Size = New System.Drawing.Size(15, 13)
        Me.lblSphericityUnit.Text = "[-]"
        Me.lblSphericityUnit.ForeColor = System.Drawing.Color.Gray
        '
        ' --- Row 6: Solid Density ---
        '
        Me.lblSolidDensity.AutoSize = True
        Me.lblSolidDensity.Location = New System.Drawing.Point(16, 159)
        Me.lblSolidDensity.Name = "lblSolidDensity"
        Me.lblSolidDensity.Size = New System.Drawing.Size(80, 13)
        Me.lblSolidDensity.Text = "Solid Density"
        '
        Me.txtSolidDensity.Location = New System.Drawing.Point(140, 156)
        Me.txtSolidDensity.Name = "txtSolidDensity"
        Me.txtSolidDensity.Size = New System.Drawing.Size(170, 20)
        Me.txtSolidDensity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        Me.lblSolidDensityUnit.AutoSize = True
        Me.lblSolidDensityUnit.Location = New System.Drawing.Point(318, 159)
        Me.lblSolidDensityUnit.Name = "lblSolidDensityUnit"
        Me.lblSolidDensityUnit.Size = New System.Drawing.Size(40, 13)
        Me.lblSolidDensityUnit.Text = "kg/m³"
        Me.lblSolidDensityUnit.ForeColor = System.Drawing.Color.Gray
        '
        ' =============================================
        ' GroupBoxCalcOptions - Calculation Options
        ' =============================================
        '
        Me.GroupBoxCalcOptions.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxCalcOptions.Controls.Add(Me.lblEquation)
        Me.GroupBoxCalcOptions.Controls.Add(Me.cbEquation)
        Me.GroupBoxCalcOptions.Controls.Add(Me.lblFluidPhase)
        Me.GroupBoxCalcOptions.Controls.Add(Me.cbFluidPhase)
        Me.GroupBoxCalcOptions.Controls.Add(Me.chkWallCorrection)
        Me.GroupBoxCalcOptions.Location = New System.Drawing.Point(6, 484)
        Me.GroupBoxCalcOptions.Name = "GroupBoxCalcOptions"
        Me.GroupBoxCalcOptions.Size = New System.Drawing.Size(391, 105)
        Me.GroupBoxCalcOptions.TabIndex = 4
        Me.GroupBoxCalcOptions.TabStop = False
        Me.GroupBoxCalcOptions.Text = "Calculation Options"
        '
        ' --- Correlation ---
        '
        Me.lblEquation.AutoSize = True
        Me.lblEquation.Location = New System.Drawing.Point(16, 24)
        Me.lblEquation.Name = "lblEquation"
        Me.lblEquation.Size = New System.Drawing.Size(60, 13)
        Me.lblEquation.Text = "Correlation"
        '
        Me.cbEquation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbEquation.FormattingEnabled = True
        Me.cbEquation.Location = New System.Drawing.Point(140, 21)
        Me.cbEquation.Name = "cbEquation"
        Me.cbEquation.Size = New System.Drawing.Size(236, 21)
        Me.cbEquation.TabIndex = 0
        '
        ' --- Fluid Phase ---
        '
        Me.lblFluidPhase.AutoSize = True
        Me.lblFluidPhase.Location = New System.Drawing.Point(16, 51)
        Me.lblFluidPhase.Name = "lblFluidPhase"
        Me.lblFluidPhase.Size = New System.Drawing.Size(70, 13)
        Me.lblFluidPhase.Text = "Fluid Phase"
        '
        Me.cbFluidPhase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFluidPhase.FormattingEnabled = True
        Me.cbFluidPhase.Location = New System.Drawing.Point(140, 48)
        Me.cbFluidPhase.Name = "cbFluidPhase"
        Me.cbFluidPhase.Size = New System.Drawing.Size(236, 21)
        Me.cbFluidPhase.TabIndex = 1
        '
        ' --- Wall Correction ---
        '
        Me.chkWallCorrection.AutoSize = True
        Me.chkWallCorrection.Location = New System.Drawing.Point(140, 78)
        Me.chkWallCorrection.Name = "chkWallCorrection"
        Me.chkWallCorrection.Size = New System.Drawing.Size(220, 17)
        Me.chkWallCorrection.Text = "Wall Effect Correction (Eisfeld && Schnitzlein)"
        Me.chkWallCorrection.TabIndex = 2
        Me.chkWallCorrection.UseVisualStyleBackColor = True
        '
        ' =============================================
        ' GroupBox4 - Results
        ' =============================================
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.gridOutput)
        Me.GroupBox4.Location = New System.Drawing.Point(6, 595)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(391, 310)
        Me.GroupBox4.TabIndex = 5
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Results"
        '
        'gridOutput
        '
        Me.gridOutput.AllowUserToAddRows = False
        Me.gridOutput.AllowUserToDeleteRows = False
        Me.gridOutput.AllowUserToResizeRows = False
        Me.gridOutput.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.gridOutput.BackgroundColor = System.Drawing.SystemColors.Window
        Me.gridOutput.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.gridOutput.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.gridOutput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gridOutput.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colResultName, Me.colResultValue, Me.colResultUnit})
        Me.gridOutput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridOutput.EnableHeadersVisualStyles = False
        Me.gridOutput.GridColor = System.Drawing.Color.FromArgb(CType(230, Byte), CType(230, Byte), CType(230, Byte))
        Me.gridOutput.Location = New System.Drawing.Point(3, 16)
        Me.gridOutput.Margin = New System.Windows.Forms.Padding(0)
        Me.gridOutput.Name = "gridOutput"
        Me.gridOutput.ReadOnly = True
        Me.gridOutput.RowHeadersVisible = False
        Me.gridOutput.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.gridOutput.Size = New System.Drawing.Size(385, 291)
        Me.gridOutput.TabIndex = 0
        '
        'colResultName
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(245, Byte), CType(245, Byte), CType(245, Byte))
        Me.colResultName.DefaultCellStyle = DataGridViewCellStyle1
        Me.colResultName.FillWeight = 55.0!
        Me.colResultName.HeaderText = "Parameter"
        Me.colResultName.Name = "colResultName"
        Me.colResultName.ReadOnly = True
        '
        'colResultValue
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colResultValue.DefaultCellStyle = DataGridViewCellStyle2
        Me.colResultValue.FillWeight = 30.0!
        Me.colResultValue.HeaderText = "Value"
        Me.colResultValue.Name = "colResultValue"
        Me.colResultValue.ReadOnly = True
        '
        'colResultUnit
        '
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Gray
        Me.colResultUnit.DefaultCellStyle = DataGridViewCellStyle3
        Me.colResultUnit.FillWeight = 15.0!
        Me.colResultUnit.HeaderText = "Unit"
        Me.colResultUnit.Name = "colResultUnit"
        Me.colResultUnit.ReadOnly = True
        '
        'ToolTipChangeTag
        '
        Me.ToolTipChangeTag.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.ToolTipChangeTag.ToolTipTitle = "Info"
        '
        ' =============================================
        ' Editor Form
        ' =============================================
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(402, 920)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBoxCalcOptions)
        Me.Controls.Add(Me.GroupBoxBedGeometry)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "Editor"
        Me.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockLeft
        Me.Text = "Packed Bed Pressure Drop"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBoxBedGeometry.ResumeLayout(False)
        Me.GroupBoxBedGeometry.PerformLayout()
        Me.GroupBoxCalcOptions.ResumeLayout(False)
        Me.GroupBoxCalcOptions.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.gridOutput, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As Windows.Forms.GroupBox
    Friend WithEvents GroupBox5 As Windows.Forms.GroupBox
    Friend WithEvents GroupBoxBedGeometry As Windows.Forms.GroupBox
    Friend WithEvents GroupBoxCalcOptions As Windows.Forms.GroupBox
    Friend WithEvents Panel1 As Windows.Forms.Panel
    Public WithEvents lblTag As Windows.Forms.TextBox
    Public WithEvents chkActive As Windows.Forms.CheckBox
    Public WithEvents lblConnectedTo As Windows.Forms.Label
    Public WithEvents lblStatus As Windows.Forms.Label
    Public WithEvents Label13 As Windows.Forms.Label
    Public WithEvents Label12 As Windows.Forms.Label
    Public WithEvents Label11 As Windows.Forms.Label
    Public WithEvents btnConfigurePP As Windows.Forms.Button
    Public WithEvents cbPropPack As Windows.Forms.ComboBox
    Public WithEvents Label9 As Windows.Forms.Label
    Friend WithEvents ToolTipChangeTag As Windows.Forms.ToolTip
    ' Bed Geometry fields
    Friend WithEvents lblBedDiameter As Windows.Forms.Label
    Friend WithEvents txtBedDiameter As Windows.Forms.TextBox
    Friend WithEvents lblBedDiameterUnit As Windows.Forms.Label
    Friend WithEvents lblBedHeight As Windows.Forms.Label
    Friend WithEvents txtBedHeight As Windows.Forms.TextBox
    Friend WithEvents lblBedHeightUnit As Windows.Forms.Label
    Friend WithEvents lblParticleDiameter As Windows.Forms.Label
    Friend WithEvents txtParticleDiameter As Windows.Forms.TextBox
    Friend WithEvents lblParticleDiameterUnit As Windows.Forms.Label
    Friend WithEvents lblVoidFraction As Windows.Forms.Label
    Friend WithEvents txtVoidFraction As Windows.Forms.TextBox
    Friend WithEvents lblVoidFractionUnit As Windows.Forms.Label
    Friend WithEvents lblSphericity As Windows.Forms.Label
    Friend WithEvents txtSphericity As Windows.Forms.TextBox
    Friend WithEvents lblSphericityUnit As Windows.Forms.Label
    Friend WithEvents lblSolidDensity As Windows.Forms.Label
    Friend WithEvents txtSolidDensity As Windows.Forms.TextBox
    Friend WithEvents lblSolidDensityUnit As Windows.Forms.Label
    ' Calculation Options
    Friend WithEvents lblEquation As Windows.Forms.Label
    Friend WithEvents cbEquation As Windows.Forms.ComboBox
    Friend WithEvents lblFluidPhase As Windows.Forms.Label
    Friend WithEvents cbFluidPhase As Windows.Forms.ComboBox
    Friend WithEvents chkWallCorrection As Windows.Forms.CheckBox
    ' Results grid
    Public WithEvents gridOutput As Windows.Forms.DataGridView
    Friend WithEvents colResultName As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colResultValue As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colResultUnit As Windows.Forms.DataGridViewTextBoxColumn
End Class
