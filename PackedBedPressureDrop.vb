' ============================================================================
' DWSIM Unit Operation: Packed Bed Pressure Drop
' ============================================================================
' Authors: Nicolas Spogis / AI4Tech
' License: GPL v3 (same as DWSIM)
' ============================================================================

Imports System.Math
Imports DWSIM.Interfaces.Enums
Imports SkiaSharp.Views.Desktop.Extensions
Imports System.Windows.Forms
Imports DWSIM.Thermodynamics.Streams
Imports DWSIM.UnitOperations.Streams
Imports XFlowsheet.Implementation.DefaultImplementations
Imports DWSIM
Imports DWSIM.UnitOperations

Public Class PackedBedPressureDrop

    Inherits UnitOperations.UnitOpBaseClass

    Implements Interfaces.IExternalUnitOperation

#Region "Unit Operation Information"

    ' Use constants to avoid any null issues during deserialization
    Private Const DEFAULT_NAME As String = "Packed Bed Pressure Drop"
    Private Const DEFAULT_DESC As String = "Calculates pressure drop across a packed/fixed bed using Ergun, McDonald, Gamane & Lannoy, Tallmadge, or Kozeny-Carman correlations."
    Private Const DEFAULT_PREFIX As String = "PBDP-"

    Public ReadOnly Property Prefix As String Implements Interfaces.IExternalUnitOperation.Prefix
        Get
            Return DEFAULT_PREFIX
        End Get
    End Property

    Public Overrides Property ObjectClass As SimulationObjectClass = SimulationObjectClass.PressureChangers

    Public Overrides Function GetDisplayName() As String
        Return DEFAULT_NAME
    End Function

    Public Overrides Function GetDisplayDescription() As String
        Return DEFAULT_DESC
    End Function

    Public Overrides ReadOnly Property MobileCompatible As Boolean = False

#End Region

#Region "Enumerations"

    Public Enum PressureDropEquation
        Ergun = 0
        McDonald = 1
        Gamane_Lannoy = 2
        Tallmadge = 3
        KozenyCarman = 4
    End Enum

    Public Enum FluidPhaseOption
        Auto = 0
        Vapor = 1
        Liquid = 2
        Overall = 3
    End Enum

#End Region

#Region "Properties - Bed Geometry"

    Private _bedDiameter As Double = 1.0
    Private _bedHeight As Double = 3.0
    Private _particleDiameter As Double = 0.01
    Private _voidFraction As Double = 0.4
    Private _sphericity As Double = 1.0
    Private _solidDensity As Double = 2500.0

    Public Property BedDiameter As Double
        Get
            Return _bedDiameter
        End Get
        Set(value As Double)
            _bedDiameter = value
        End Set
    End Property

    Public Property BedHeight As Double
        Get
            Return _bedHeight
        End Get
        Set(value As Double)
            _bedHeight = value
        End Set
    End Property

    Public Property ParticleDiameter As Double
        Get
            Return _particleDiameter
        End Get
        Set(value As Double)
            _particleDiameter = value
        End Set
    End Property

    Public Property VoidFraction As Double
        Get
            Return _voidFraction
        End Get
        Set(value As Double)
            _voidFraction = value
        End Set
    End Property

    Public Property Sphericity As Double
        Get
            Return _sphericity
        End Get
        Set(value As Double)
            _sphericity = value
        End Set
    End Property

    Public Property SolidDensity As Double
        Get
            Return _solidDensity
        End Get
        Set(value As Double)
            _solidDensity = value
        End Set
    End Property

#End Region

#Region "Properties - Calculation Options"

    Private _equation As PressureDropEquation = PressureDropEquation.Ergun
    Private _fluidPhase As FluidPhaseOption = FluidPhaseOption.Auto
    Private _useWallCorrection As Boolean = False

    Public Property SelectedEquation As PressureDropEquation
        Get
            Return _equation
        End Get
        Set(value As PressureDropEquation)
            _equation = value
        End Set
    End Property

    Public Property SelectedFluidPhase As FluidPhaseOption
        Get
            Return _fluidPhase
        End Get
        Set(value As FluidPhaseOption)
            _fluidPhase = value
        End Set
    End Property

    Public Property UseWallCorrection As Boolean
        Get
            Return _useWallCorrection
        End Get
        Set(value As Boolean)
            _useWallCorrection = value
        End Set
    End Property

#End Region

#Region "Properties - Calculated Results"

    Private _calcDeltaP As Double = 0.0
    Private _calcReynolds As Double = 0.0
    Private _calcSuperficialVelocity As Double = 0.0
    Private _calcFrictionFactor As Double = 0.0
    Private _calcBedCrossSectionArea As Double = 0.0
    Private _calcMinFluidizationVelocity As Double = 0.0
    Private _calcArchimedesNumber As Double = 0.0
    Private _calcReynoldsMf As Double = 0.0
    Private _calcFluidDensity As Double = 0.0
    Private _calcFluidViscosity As Double = 0.0
    Private _calcInletPressure As Double = 0.0
    Private _calcOutletPressure As Double = 0.0
    Private _calcPhaseUsed As String = ""

#End Region

#Region "Initialization, Serialization and Cloning"

    Public Sub New(ByVal Name As String, ByVal Description As String)
        MyBase.CreateNew()
        Me.ComponentName = Name
        Me.ComponentDescription = Description
        EnsureParametersInitialized()
    End Sub

    Public Sub New()
        MyBase.New()
        EnsureParametersInitialized()
    End Sub

    ''' <summary>
    ''' Override LoadData: wraps base in try-catch because Dictionary(Of String, Parameter) 
    ''' is NOT serialized to DWSIM XML, which can cause NullReferenceException during loading.
    ''' After loading, syncs the deserialized property values into InputParameters.
    ''' </summary>
    Public Overrides Function LoadData(data As System.Collections.Generic.List(Of System.Xml.Linq.XElement)) As Boolean
        Dim result As Boolean = True
        Try
            result = MyBase.LoadData(data)
        Catch ex As Exception
            ' Non-fatal: base class may fail on Dictionary(Of String, Parameter) deserialization
        End Try

        ' Ensure dictionaries exist and have all keys
        EnsureParametersInitialized()

        ' Sync the XML-deserialized property values into InputParameters
        ' (XML restores BedDiameter, BedHeight etc. as class properties,
        '  but InputParameters dictionary is not in the XML)
        SyncPropertiesToParameters()

        Return result
    End Function

    ''' <summary>
    ''' Override SaveData: syncs InputParameters into class properties before saving,
    ''' so the XML always has the latest values from the editor grid.
    ''' </summary>
    Public Overrides Function SaveData() As System.Collections.Generic.List(Of System.Xml.Linq.XElement)
        SyncParametersToProperties()
        Return MyBase.SaveData()
    End Function

    ''' <summary>
    ''' Ensures InputParameters and OutputParameters exist and contain all required keys.
    ''' Safe to call multiple times.
    ''' </summary>
    Private Sub EnsureParametersInitialized()

        If InputParameters Is Nothing Then InputParameters = New Dictionary(Of String, Parameter)()
        If OutputParameters Is Nothing Then OutputParameters = New Dictionary(Of String, Parameter)()

        ' === Input Parameters ===
        AddInputIfMissing("Bed Diameter", "Internal diameter of the packed bed [m]", _bedDiameter)
        AddInputIfMissing("Bed Height", "Height of the packed bed [m]", _bedHeight)
        AddInputIfMissing("Particle Diameter", "Equivalent particle diameter [m]", _particleDiameter)
        AddInputIfMissing("Void Fraction", "Bed porosity, dimensionless (0 to 1)", _voidFraction)
        AddInputIfMissing("Sphericity", "Particle sphericity (0 to 1, 1=sphere)", _sphericity)
        AddInputIfMissing("Solid Density", "Particle/solid material density [kg/m3]", _solidDensity)
        AddInputIfMissing("Equation", "0=Ergun, 1=McDonald, 2=Gamane&Lannoy, 3=Tallmadge, 4=KozenyCarman", CDbl(_equation))
        AddInputIfMissing("Fluid Phase", "0=Auto, 1=Vapor, 2=Liquid, 3=Overall", CDbl(_fluidPhase))
        AddInputIfMissing("Wall Correction", "Eisfeld & Schnitzlein wall correction (0=Off, 1=On)", 0.0)

        ' === Output Parameters ===
        AddOutputIfMissing("Pressure Drop", "Calculated pressure drop [Pa]")
        AddOutputIfMissing("Reynolds Number", "Particle Reynolds number")
        AddOutputIfMissing("Superficial Velocity", "Superficial velocity [m/s]")
        AddOutputIfMissing("Friction Factor", "Friction factor (dimensionless)")
        AddOutputIfMissing("Bed Cross-Section Area", "Cross-sectional area of the bed [m2]")
        AddOutputIfMissing("Archimedes Number", "Archimedes number (dimensionless)")
        AddOutputIfMissing("Re at Min. Fluidization", "Reynolds number at minimum fluidization")
        AddOutputIfMissing("Min. Fluidization Velocity", "Minimum fluidization velocity [m/s] (Wen & Yu)")
        AddOutputIfMissing("Fluid Density", "Fluid density used in calculation [kg/m3]")
        AddOutputIfMissing("Fluid Viscosity", "Fluid dynamic viscosity used [Pa.s]")
        AddOutputIfMissing("Inlet Pressure", "Inlet stream pressure [Pa]")
        AddOutputIfMissing("Outlet Pressure", "Outlet stream pressure [Pa]")
        AddOutputIfMissing("Phase Used", "1=Vapor, 2=Liquid1, 3=OverallLiquid, 4=Overall")

    End Sub

    Private Sub AddInputIfMissing(name As String, desc As String, defaultValue As Double)
        If Not InputParameters.ContainsKey(name) Then
            InputParameters.Add(name, New Parameter() With {.Name = name, .Description = desc, .Value = defaultValue})
        End If
    End Sub

    Private Sub AddOutputIfMissing(name As String, desc As String)
        If Not OutputParameters.ContainsKey(name) Then
            OutputParameters.Add(name, New Parameter() With {.Name = name, .Description = desc, .Value = 0.0})
        End If
    End Sub

    ''' <summary>
    ''' Copies class property values INTO InputParameters dictionary.
    ''' Used after XML loading (XML restores properties but not the dictionary).
    ''' </summary>
    Private Sub SyncPropertiesToParameters()
        If InputParameters.ContainsKey("Bed Diameter") Then InputParameters("Bed Diameter").Value = _bedDiameter
        If InputParameters.ContainsKey("Bed Height") Then InputParameters("Bed Height").Value = _bedHeight
        If InputParameters.ContainsKey("Particle Diameter") Then InputParameters("Particle Diameter").Value = _particleDiameter
        If InputParameters.ContainsKey("Void Fraction") Then InputParameters("Void Fraction").Value = _voidFraction
        If InputParameters.ContainsKey("Sphericity") Then InputParameters("Sphericity").Value = _sphericity
        If InputParameters.ContainsKey("Solid Density") Then InputParameters("Solid Density").Value = _solidDensity
        If InputParameters.ContainsKey("Equation") Then InputParameters("Equation").Value = CDbl(_equation)
        If InputParameters.ContainsKey("Fluid Phase") Then InputParameters("Fluid Phase").Value = CDbl(_fluidPhase)
        If InputParameters.ContainsKey("Wall Correction") Then InputParameters("Wall Correction").Value = If(_useWallCorrection, 1.0, 0.0)
    End Sub

    ''' <summary>
    ''' Copies InputParameters dictionary values INTO class properties.
    ''' Used before calculation (editor grid modifies the dictionary).
    ''' </summary>
    Private Sub SyncParametersToProperties()
        If InputParameters.ContainsKey("Bed Diameter") Then _bedDiameter = InputParameters("Bed Diameter").Value
        If InputParameters.ContainsKey("Bed Height") Then _bedHeight = InputParameters("Bed Height").Value
        If InputParameters.ContainsKey("Particle Diameter") Then _particleDiameter = InputParameters("Particle Diameter").Value
        If InputParameters.ContainsKey("Void Fraction") Then _voidFraction = InputParameters("Void Fraction").Value
        If InputParameters.ContainsKey("Sphericity") Then _sphericity = InputParameters("Sphericity").Value
        If InputParameters.ContainsKey("Solid Density") Then _solidDensity = InputParameters("Solid Density").Value
        If InputParameters.ContainsKey("Equation") Then _equation = CType(CInt(InputParameters("Equation").Value), PressureDropEquation)
        If InputParameters.ContainsKey("Fluid Phase") Then _fluidPhase = CType(CInt(InputParameters("Fluid Phase").Value), FluidPhaseOption)
        If InputParameters.ContainsKey("Wall Correction") Then _useWallCorrection = (InputParameters("Wall Correction").Value > 0.5)
    End Sub

    Private Sub SyncResultsToOutputParameters()
        If OutputParameters.ContainsKey("Pressure Drop") Then OutputParameters("Pressure Drop").Value = _calcDeltaP
        If OutputParameters.ContainsKey("Reynolds Number") Then OutputParameters("Reynolds Number").Value = _calcReynolds
        If OutputParameters.ContainsKey("Superficial Velocity") Then OutputParameters("Superficial Velocity").Value = _calcSuperficialVelocity
        If OutputParameters.ContainsKey("Friction Factor") Then OutputParameters("Friction Factor").Value = _calcFrictionFactor
        If OutputParameters.ContainsKey("Bed Cross-Section Area") Then OutputParameters("Bed Cross-Section Area").Value = _calcBedCrossSectionArea
        If OutputParameters.ContainsKey("Archimedes Number") Then OutputParameters("Archimedes Number").Value = _calcArchimedesNumber
        If OutputParameters.ContainsKey("Re at Min. Fluidization") Then OutputParameters("Re at Min. Fluidization").Value = _calcReynoldsMf
        If OutputParameters.ContainsKey("Min. Fluidization Velocity") Then OutputParameters("Min. Fluidization Velocity").Value = _calcMinFluidizationVelocity
        If OutputParameters.ContainsKey("Fluid Density") Then OutputParameters("Fluid Density").Value = _calcFluidDensity
        If OutputParameters.ContainsKey("Fluid Viscosity") Then OutputParameters("Fluid Viscosity").Value = _calcFluidViscosity
        If OutputParameters.ContainsKey("Inlet Pressure") Then OutputParameters("Inlet Pressure").Value = _calcInletPressure
        If OutputParameters.ContainsKey("Outlet Pressure") Then OutputParameters("Outlet Pressure").Value = _calcOutletPressure
        If OutputParameters.ContainsKey("Phase Used") Then
            Select Case _calcPhaseUsed
                Case "Vapor" : OutputParameters("Phase Used").Value = 1.0
                Case "Liquid1" : OutputParameters("Phase Used").Value = 2.0
                Case "OverallLiquid" : OutputParameters("Phase Used").Value = 3.0
                Case "Overall" : OutputParameters("Phase Used").Value = 4.0
                Case Else : OutputParameters("Phase Used").Value = 0.0
            End Select
        End If
    End Sub

    Public Function ReturnInstance(typename As String) As Object Implements Interfaces.IExternalUnitOperation.ReturnInstance
        Return New PackedBedPressureDrop()
    End Function

    Public Overrides Function CloneXML() As Object
        Dim objdata = XMLSerializer.XMLSerializer.Serialize(Me)
        Dim newobj As New PackedBedPressureDrop()
        newobj.LoadData(objdata)
        Return newobj
    End Function

    Public Overrides Function CloneJSON() As Object
        Dim jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(Me)
        Dim newobj = Newtonsoft.Json.JsonConvert.DeserializeObject(Of PackedBedPressureDrop)(jsonstring)
        Return newobj
    End Function

#End Region

#Region "Validation"

    Private Sub ValidateInputs()

        If _bedDiameter <= 0.0 Then Throw New Exception("Bed Diameter must be positive. Value: " & _bedDiameter.ToString("E4") & " m")
        If _bedHeight <= 0.0 Then Throw New Exception("Bed Height must be positive. Value: " & _bedHeight.ToString("E4") & " m")
        If _particleDiameter <= 0.0 Then Throw New Exception("Particle Diameter must be positive. Value: " & _particleDiameter.ToString("E4") & " m")
        If _voidFraction <= 0.0 OrElse _voidFraction >= 1.0 Then Throw New Exception("Void Fraction must be between 0 and 1. Value: " & _voidFraction.ToString("F4"))
        If _sphericity <= 0.0 OrElse _sphericity > 1.0 Then Throw New Exception("Sphericity must be between 0 and 1. Value: " & _sphericity.ToString("F4"))
        If _solidDensity <= 0.0 Then Throw New Exception("Solid Density must be positive. Value: " & _solidDensity.ToString("F2") & " kg/m3")
        If _particleDiameter >= _bedDiameter Then Throw New Exception("Particle Diameter must be smaller than Bed Diameter.")

    End Sub

#End Region

#Region "Smart Phase Detection"

    Private Function GetFluidProperties(inlet As MaterialStream) As Tuple(Of Double, Double, String)

        Dim density As Double = 0.0
        Dim viscosity As Double = 0.0
        Dim phaseUsed As String = ""

        Dim phasesToTry As New List(Of String)

        Select Case _fluidPhase
            Case FluidPhaseOption.Auto
                phasesToTry.AddRange({"Vapor", "Liquid1", "OverallLiquid", "Overall"})
            Case FluidPhaseOption.Vapor
                phasesToTry.AddRange({"Vapor", "Liquid1", "OverallLiquid", "Overall"})
            Case FluidPhaseOption.Liquid
                phasesToTry.AddRange({"Liquid1", "OverallLiquid", "Vapor", "Overall"})
            Case FluidPhaseOption.Overall
                phasesToTry.AddRange({"Overall", "Vapor", "Liquid1", "OverallLiquid"})
            Case Else
                phasesToTry.AddRange({"Vapor", "Liquid1", "OverallLiquid", "Overall"})
        End Select

        For Each phaseName In phasesToTry
            Try
                Dim phaseObj = inlet.GetPhase(phaseName)
                If phaseObj Is Nothing Then Continue For

                Dim phaseFraction As Double = 0.0
                Try
                    phaseFraction = phaseObj.Properties.molarfraction.GetValueOrDefault()
                Catch
                    phaseFraction = 1.0
                End Try

                If phaseFraction <= 0.0 AndAlso phaseName <> "Overall" AndAlso phaseName <> "OverallLiquid" Then
                    Continue For
                End If

                Dim rho As Double = phaseObj.Properties.density.GetValueOrDefault()
                Dim mu As Double = phaseObj.Properties.viscosity.GetValueOrDefault()

                If rho > 0.0 AndAlso mu > 0.0 Then
                    density = rho
                    viscosity = mu
                    phaseUsed = phaseName
                    Exit For
                End If
            Catch
                Continue For
            End Try
        Next

        If density <= 0.0 OrElse viscosity <= 0.0 Then
            Throw New Exception("Could not obtain valid fluid properties from any phase. Ensure the inlet stream is calculated.")
        End If

        If _fluidPhase <> FluidPhaseOption.Auto Then
            Dim expectedPhase As String = ""
            Select Case _fluidPhase
                Case FluidPhaseOption.Vapor : expectedPhase = "Vapor"
                Case FluidPhaseOption.Liquid : expectedPhase = "Liquid1"
                Case FluidPhaseOption.Overall : expectedPhase = "Overall"
            End Select
            If phaseUsed <> expectedPhase Then
                FlowSheet.WriteMessage("INFO: Requested phase not available. Using '" & phaseUsed & "' instead.")
            End If
        Else
            FlowSheet.WriteMessage("INFO: Auto phase detection selected '" & phaseUsed & "'.")
        End If

        Return New Tuple(Of Double, Double, String)(density, viscosity, phaseUsed)

    End Function

#End Region

#Region "Calculation Routine"

    Public Overrides Sub Calculate(Optional args As Object = Nothing)

        SyncParametersToProperties()
        ValidateInputs()

        Dim inlet As MaterialStream = GetInletMaterialStream(0)
        Dim outlet As MaterialStream = GetOutletMaterialStream(0)

        If inlet Is Nothing Then Throw New Exception("No material stream connected to the inlet port.")
        If outlet Is Nothing Then Throw New Exception("No material stream connected to the outlet port.")

        ' Phase detection
        Dim fluidProps = GetFluidProperties(inlet)
        Dim fluidDensity As Double = fluidProps.Item1
        Dim fluidViscosity As Double = fluidProps.Item2
        Dim phaseUsed As String = fluidProps.Item3

        _calcFluidDensity = fluidDensity
        _calcFluidViscosity = fluidViscosity
        _calcPhaseUsed = phaseUsed

        Dim volumetricFlow As Double = inlet.GetVolumetricFlow()
        If volumetricFlow <= 0.0 Then Throw New Exception("Volumetric flow is zero or negative.")

        FlowSheet.WriteMessage("=== Packed Bed Pressure Drop Calculation ===")
        FlowSheet.WriteMessage("Phase: " & phaseUsed & " | Q: " & volumetricFlow.ToString("E4") & " m3/s | rho: " & fluidDensity.ToString("F4") & " kg/m3 | mu: " & fluidViscosity.ToString("E4") & " Pa.s")
        FlowSheet.WriteMessage("Solid density: " & _solidDensity.ToString("F2") & " kg/m3")

        ' Bed geometry
        Dim bedArea As Double = PI * (_bedDiameter / 2.0) ^ 2
        _calcBedCrossSectionArea = bedArea
        Dim dpEff As Double = _sphericity * _particleDiameter

        FlowSheet.WriteMessage("Bed: D=" & _bedDiameter.ToString("F4") & "m, H=" & _bedHeight.ToString("F4") & "m, A=" & bedArea.ToString("F6") & "m2")
        FlowSheet.WriteMessage("Particle: dp=" & _particleDiameter.ToString("E4") & "m, dp_eff=" & dpEff.ToString("E4") & "m, eps=" & _voidFraction.ToString("F4") & ", phi=" & _sphericity.ToString("F4"))

        ' Superficial velocity and Reynolds
        Dim Vsup As Double = volumetricFlow / bedArea
        _calcSuperficialVelocity = Vsup
        Dim Re As Double = fluidDensity * Vsup * dpEff / fluidViscosity
        _calcReynolds = Re

        FlowSheet.WriteMessage("Vsup: " & Vsup.ToString("E4") & " m/s | Re: " & Re.ToString("F4"))

        ' Wall correction
        Dim wallFactorA As Double = 1.0
        Dim wallFactorB As Double = 1.0
        If _useWallCorrection Then
            Dim ratio As Double = _bedDiameter / _particleDiameter
            If ratio < 10.0 Then
                wallFactorA = 1.0 + 2.0 / (3.0 * ratio * (1.0 - _voidFraction))
                wallFactorB = (1.15 * (1.0 / ratio) ^ 2 + 0.87) ^ 2
                FlowSheet.WriteMessage("Wall correction ACTIVE (D/dp=" & ratio.ToString("F2") & ") - Aw: " & wallFactorA.ToString("F4") & ", Bw: " & wallFactorB.ToString("F4"))
            End If
        End If

        ' Equation coefficients
        Dim A_coeff As Double = 0.0
        Dim B_coeff As Double = 0.0
        Dim equationName As String = ""

        Select Case _equation
            Case PressureDropEquation.Ergun
                A_coeff = 150.0 : B_coeff = 1.75 : equationName = "Ergun"
            Case PressureDropEquation.McDonald
                A_coeff = 180.0 : B_coeff = 1.8 : equationName = "McDonald et al."
            Case PressureDropEquation.Gamane_Lannoy
                A_coeff = 360.0 : B_coeff = 3.6 : equationName = "Gamane & Lannoy"
            Case PressureDropEquation.Tallmadge
                A_coeff = 150.0
                B_coeff = If(Re > 0.0, 4.2 * Re ^ (1.0 / 6.0), 1.75)
                equationName = "Tallmadge"
            Case PressureDropEquation.KozenyCarman
                A_coeff = 180.0 : B_coeff = 0.0 : equationName = "Kozeny-Carman"
                If Re > 10.0 Then FlowSheet.WriteMessage("WARNING: Kozeny-Carman valid for Re<10. Current Re=" & Re.ToString("F2"))
        End Select

        A_coeff *= wallFactorA
        B_coeff *= wallFactorB

        FlowSheet.WriteMessage("Equation: " & equationName & " | A=" & A_coeff.ToString("F4") & ", B=" & B_coeff.ToString("F4"))

        ' Pressure drop
        Dim eps As Double = _voidFraction
        Dim viscousTerm As Double = A_coeff * fluidViscosity * Vsup * (1.0 - eps) ^ 2 / (dpEff ^ 2 * eps ^ 3)
        Dim inertialTerm As Double = B_coeff * fluidDensity * Vsup ^ 2 * (1.0 - eps) / (dpEff * eps ^ 3)
        Dim DeltaP As Double = (viscousTerm + inertialTerm) * _bedHeight

        Dim frictionFactor As Double = 0.0
        If fluidDensity * Vsup ^ 2 * (1.0 - eps) > 0.0 Then
            frictionFactor = DeltaP * dpEff * eps ^ 3 / (_bedHeight * fluidDensity * Vsup ^ 2 * (1.0 - eps))
        End If

        _calcFrictionFactor = frictionFactor
        _calcDeltaP = DeltaP

        FlowSheet.WriteMessage("dP/L viscous: " & viscousTerm.ToString("E4") & " Pa/m | inertial: " & inertialTerm.ToString("E4") & " Pa/m")
        FlowSheet.WriteMessage("Friction factor: " & frictionFactor.ToString("F6"))
        FlowSheet.WriteMessage("Pressure drop: " & DeltaP.ToString("F2") & " Pa (" & (DeltaP / 1000.0).ToString("F4") & " kPa)")

        ' Min. fluidization velocity (Wen & Yu, 1966)
        Dim grav As Double = 9.80665
        Dim Ar As Double = fluidDensity * (_solidDensity - fluidDensity) * grav * dpEff ^ 3 / (fluidViscosity ^ 2)
        _calcArchimedesNumber = Ar

        If Ar > 0.0 Then
            Dim Re_mf As Double = Sqrt(33.7 ^ 2 + 0.0408 * Ar) - 33.7
            _calcReynoldsMf = Re_mf
            Dim Vmf As Double = Re_mf * fluidViscosity / (fluidDensity * dpEff)
            _calcMinFluidizationVelocity = Vmf

            FlowSheet.WriteMessage("Ar: " & Ar.ToString("E4") & " | Re_mf: " & Re_mf.ToString("F4") & " | Vmf: " & Vmf.ToString("E4") & " m/s")

            If Vsup > Vmf Then
                FlowSheet.WriteMessage("WARNING: Vsup > Vmf - bed may be fluidized!")
            Else
                FlowSheet.WriteMessage("Operating at " & ((Vsup / Vmf) * 100.0).ToString("F1") & "% of Vmf.")
            End If
        Else
            _calcReynoldsMf = 0.0
            _calcMinFluidizationVelocity = 0.0
            FlowSheet.WriteMessage("WARNING: Cannot calculate Vmf (Ar<=0). Check solid vs fluid density.")
        End If

        ' Pressure checks
        _calcInletPressure = inlet.GetPressure()
        _calcOutletPressure = _calcInletPressure - DeltaP

        If DeltaP < 0.0 Then Throw New Exception("Negative pressure drop. Check inputs.")
        If _calcOutletPressure <= 0.0 Then Throw New Exception("Outlet pressure <= 0. dP (" & DeltaP.ToString("F0") & " Pa) exceeds inlet P (" & _calcInletPressure.ToString("F0") & " Pa).")
        If DeltaP > 0.5 * _calcInletPressure Then FlowSheet.WriteMessage("WARNING: dP > 50% of inlet P.")

        FlowSheet.WriteMessage("P_in: " & _calcInletPressure.ToString("F2") & " Pa | P_out: " & _calcOutletPressure.ToString("F2") & " Pa")

        ' Outlet stream
        outlet.Assign(inlet)
        outlet.SetPressure(_calcOutletPressure)
        If Me.PropertyPackage IsNot Nothing Then
            outlet.PropertyPackage = Me.PropertyPackage
            outlet.Calculate(True, True)
        End If

        FlowSheet.WriteMessage("=== Calculation Completed Successfully ===")

        SyncResultsToOutputParameters()

    End Sub

#End Region

#Region "Automatic Unit Operation Implementation"

    Public Property InputParameters As Dictionary(Of String, Parameter) = New Dictionary(Of String, Parameter)()
    Public Property OutputParameters As Dictionary(Of String, Parameter) = New Dictionary(Of String, Parameter)()
    Public Property ConnectionPorts As List(Of ConnectionPort) = New List(Of ConnectionPort)()

    Public Overrides Property ComponentName As String = DEFAULT_NAME
    Public Overrides Property ComponentDescription As String = DEFAULT_DESC

    Private ReadOnly Property IExternalUnitOperation_Name As String Implements Interfaces.IExternalUnitOperation.Name
        Get
            Return DEFAULT_NAME
        End Get
    End Property

    Public ReadOnly Property Description As String Implements Interfaces.IExternalUnitOperation.Description
        Get
            Return DEFAULT_DESC
        End Get
    End Property

#End Region

#Region "Automatic Drawing Support"

    Public Overrides Function GetIconBitmap() As Object
        Return My.Resources.icon
    End Function

    Private Image As SkiaSharp.SKImage

    Public Sub Draw(g As Object) Implements Interfaces.IExternalUnitOperation.Draw
        Dim canvas = DirectCast(g, SkiaSharp.SKCanvas)
        If Image Is Nothing Then
            Using bitmap = My.Resources.icon.ToSKBitmap()
                Image = SkiaSharp.SKImage.FromBitmap(bitmap)
            End Using
        End If
        Using p As New SkiaSharp.SKPaint With {.FilterQuality = SkiaSharp.SKFilterQuality.High}
            canvas.DrawImage(Image, New SkiaSharp.SKRect(GraphicObject.X, GraphicObject.Y, GraphicObject.X + GraphicObject.Width, GraphicObject.Y + GraphicObject.Height), p)
        End Using
    End Sub

    Public Sub CreateConnectors() Implements Interfaces.IExternalUnitOperation.CreateConnectors

        If GraphicObject.InputConnectors.Count = 0 Then
            Dim portIn As New Drawing.SkiaSharp.GraphicObjects.ConnectionPoint()
            portIn.IsEnergyConnector = False
            portIn.Type = Interfaces.Enums.GraphicObjects.ConType.ConIn
            portIn.Position = New DWSIM.DrawingTools.Point.Point(GraphicObject.X, GraphicObject.Y + GraphicObject.Height / 2)
            portIn.ConnectorName = "Inlet (Material Stream)"
            GraphicObject.InputConnectors.Add(portIn)
        Else
            GraphicObject.InputConnectors(0).Position = New DWSIM.DrawingTools.Point.Point(GraphicObject.X, GraphicObject.Y + GraphicObject.Height / 2)
            GraphicObject.InputConnectors(0).ConnectorName = "Inlet (Material Stream)"
        End If

        If GraphicObject.OutputConnectors.Count = 0 Then
            Dim portOut As New Drawing.SkiaSharp.GraphicObjects.ConnectionPoint()
            portOut.IsEnergyConnector = False
            portOut.Type = Interfaces.Enums.GraphicObjects.ConType.ConOut
            portOut.Position = New DWSIM.DrawingTools.Point.Point(GraphicObject.X + GraphicObject.Width, GraphicObject.Y + GraphicObject.Height / 2)
            portOut.ConnectorName = "Outlet (Material Stream)"
            GraphicObject.OutputConnectors.Add(portOut)
        Else
            GraphicObject.OutputConnectors(0).Position = New DWSIM.DrawingTools.Point.Point(GraphicObject.X + GraphicObject.Width, GraphicObject.Y + GraphicObject.Height / 2)
            GraphicObject.OutputConnectors(0).ConnectorName = "Outlet (Material Stream)"
        End If

        GraphicObject.EnergyConnector.Active = False

    End Sub

#End Region

#Region "Classic UI and Cross-Platform UI Editor Support"

    <Xml.Serialization.XmlIgnore> Public editwindow As Editor

    Public Overrides Sub DisplayEditForm()
        If editwindow Is Nothing Then
            editwindow = New Editor() With {.SimObject = Me}
        Else
            If editwindow.IsDisposed Then
                editwindow = New Editor() With {.SimObject = Me}
            End If
        End If
        FlowSheet.DisplayForm(editwindow)
    End Sub

    Public Overrides Sub UpdateEditForm()
        If editwindow IsNot Nothing Then
            If editwindow.InvokeRequired Then
                editwindow.Invoke(Sub() editwindow?.UpdateInfo())
            Else
                editwindow?.UpdateInfo()
            End If
        End If
    End Sub

    Public Overrides Sub CloseEditForm()
        editwindow?.Close()
    End Sub

    Public Overrides Function GetEditingForm() As Form
        Return Nothing
    End Function

    Public Sub PopulateEditorPanel(container As Object) Implements Interfaces.IExternalUnitOperation.PopulateEditorPanel
    End Sub

#End Region

End Class