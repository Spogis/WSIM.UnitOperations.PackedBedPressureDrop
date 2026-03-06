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

    Private Property UOName As String = "My Unit Operation"
    Private Property UODescription As String = "My Unit Operation Description"

    Public ReadOnly Property Prefix As String Implements Interfaces.IExternalUnitOperation.Prefix
        Get
            Return "MYUO-"
        End Get
    End Property

    Public Overrides Property ObjectClass As SimulationObjectClass = SimulationObjectClass.UserModels

    Public Overrides Function GetDisplayName() As String
        Return UOName
    End Function

    Public Overrides Function GetDisplayDescription() As String
        Return UODescription
    End Function

    'tells DWSIM that this Unit Operation is or isn't compatible with mobile versions
    Public Overrides ReadOnly Property MobileCompatible As Boolean = False

#End Region

#Region "Initialization and Cloning Support"

    Public Sub New(ByVal Name As String, ByVal Description As String)

        MyBase.CreateNew()
        Me.ComponentName = Name
        Me.ComponentDescription = Description

    End Sub

    Public Sub New()

        MyBase.New()

    End Sub

    'returns a new instance of this unit operation
    Public Function ReturnInstance(typename As String) As Object Implements Interfaces.IExternalUnitOperation.ReturnInstance

        Return New PackedBedPressureDrop()

    End Function

    'returns a new instance of unit operation, using XML cloning
    Public Overrides Function CloneXML() As Object

        Dim objdata = XMLSerializer.XMLSerializer.Serialize(Me)
        Dim newhumidifier As New PackedBedPressureDrop()
        newhumidifier.LoadData(objdata)

        Return newhumidifier

    End Function

    'returns a new instance of humidifer, using JSON cloning
    Public Overrides Function CloneJSON() As Object

        Dim jsonstring = Newtonsoft.Json.JsonConvert.SerializeObject(Me)
        Dim newhumidifier = Newtonsoft.Json.JsonConvert.DeserializeObject(Of PackedBedPressureDrop)(jsonstring)

        Return newhumidifier

    End Function

#End Region

#Region "Calculation Routine"

    Public Overrides Sub Calculate(Optional args As Object = Nothing)

        Dim inlet1 As MaterialStream = GetInletMaterialStream(0)

        Dim inlet2 As MaterialStream = GetInletMaterialStream(1)

        Dim outlet1 = GetOutletMaterialStream(0)

        Dim outlet2 = GetOutletMaterialStream(1)

        If inlet1 Is Nothing Then
            Throw New Exception("No stream connected to inlet port 1")
        End If

        If inlet2 Is Nothing Then
            Throw New Exception("No stream connected to inlet port 2")
        End If

        If outlet1 Is Nothing Then
            Throw New Exception("No stream connected to outlet port 1")
        End If

        If outlet2 Is Nothing Then
            Throw New Exception("No stream connected to outlet port 1")
        End If

        Dim T1 = inlet1.GetTemperature()
        Dim Comp1 = inlet1.GetOverallComposition()
        Dim P1 = inlet1.GetPressure()

        outlet1.SetTemperature(T1 * 1.2)
        outlet1.SetPressure(P1 * 2.0)
        outlet1.SetOverallComposition(Comp1)

        Dim T2 = inlet2.GetTemperature()
        Dim Comp2 = inlet2.GetOverallComposition()
        Dim P2 = inlet2.GetPressure()

        outlet2.SetTemperature(T2 * 1.2)
        outlet2.SetPressure(P2 * 1.2)
        outlet2.SetOverallComposition(Comp2)

    End Sub

#End Region

#Region "Automatic Unit Operation Implementation"

    Public Property InputParameters As Dictionary(Of String, Parameter) = New Dictionary(Of String, Parameter)()

    Public Property OutputParameters As Dictionary(Of String, Parameter) = New Dictionary(Of String, Parameter)()

    Public Property ConnectionPorts As List(Of ConnectionPort) = New List(Of ConnectionPort)()

    Public Overrides Property ComponentName As String = UOName

    Public Overrides Property ComponentDescription As String = UODescription

    Private ReadOnly Property IExternalUnitOperation_Name As String Implements Interfaces.IExternalUnitOperation.Name
        Get
            Return UOName
        End Get
    End Property

    Public ReadOnly Property Description As String Implements Interfaces.IExternalUnitOperation.Description
        Get
            Return UODescription
        End Get
    End Property

#End Region

#Region "Automatic Drawing Support"

    Public Overrides Function GetIconBitmap() As Object
        Return My.Resources.icon
    End Function

    Private Image As SkiaSharp.SKImage

    'this function draws the object on the flowsheet
    Public Sub Draw(g As Object) Implements Interfaces.IExternalUnitOperation.Draw

        'get the canvas object
        Dim canvas = DirectCast(g, SkiaSharp.SKCanvas)

        'load the icon image on memory
        If Image Is Nothing Then

            Using bitmap = My.Resources.icon.ToSKBitmap()
                Image = SkiaSharp.SKImage.FromBitmap(bitmap)
            End Using

        End If

        Dim x = Me.GraphicObject.X
        Dim y = Me.GraphicObject.Y
        Dim w = Me.GraphicObject.Width
        Dim h = Me.GraphicObject.Height

        'draw the image into the flowsheet inside the object's reserved rectangle area
        Using p As New SkiaSharp.SKPaint With {.FilterQuality = SkiaSharp.SKFilterQuality.High}
            canvas.DrawImage(Image, New SkiaSharp.SKRect(GraphicObject.X, GraphicObject.Y, GraphicObject.X + GraphicObject.Width, GraphicObject.Y + GraphicObject.Height), p)
        End Using

    End Sub

    'this function creates the connection ports in the flowsheet object
    Public Sub CreateConnectors() Implements Interfaces.IExternalUnitOperation.CreateConnectors

        If GraphicObject.InputConnectors.Count = 0 Then

            Dim port1 As New Drawing.SkiaSharp.GraphicObjects.ConnectionPoint()

            port1.IsEnergyConnector = False
            port1.Type = Interfaces.Enums.GraphicObjects.ConType.ConIn
            port1.Position = New DWSIM.DrawingTools.Point.Point(GraphicObject.X, GraphicObject.Y)
            port1.ConnectorName = "Inlet Port 1"

            GraphicObject.InputConnectors.Add(port1)

            Dim port2 As New Drawing.SkiaSharp.GraphicObjects.ConnectionPoint()

            port2.IsEnergyConnector = False
            port2.Type = Interfaces.Enums.GraphicObjects.ConType.ConIn
            port2.Position = New DWSIM.DrawingTools.Point.Point(GraphicObject.X, GraphicObject.Y + GraphicObject.Height)
            port2.ConnectorName = "Inlet Port 2"

            GraphicObject.InputConnectors.Add(port2)

        Else

            GraphicObject.InputConnectors(0).Position = New DWSIM.DrawingTools.Point.Point(GraphicObject.X, GraphicObject.Y)

            GraphicObject.InputConnectors(1).Position = New DWSIM.DrawingTools.Point.Point(GraphicObject.X, GraphicObject.Y + GraphicObject.Height)

        End If

        If GraphicObject.OutputConnectors.Count = 0 Then

            Dim port3 As New Drawing.SkiaSharp.GraphicObjects.ConnectionPoint()

            port3.IsEnergyConnector = False
            port3.Type = Interfaces.Enums.GraphicObjects.ConType.ConOut
            port3.Position = New DWSIM.DrawingTools.Point.Point(GraphicObject.X + GraphicObject.Width, GraphicObject.Y)
            port3.ConnectorName = "Outlet Port 1"

            GraphicObject.OutputConnectors.Add(port3)

            Dim port4 As New Drawing.SkiaSharp.GraphicObjects.ConnectionPoint()

            port4.IsEnergyConnector = False
            port4.Type = Interfaces.Enums.GraphicObjects.ConType.ConOut
            port4.Position = New DWSIM.DrawingTools.Point.Point(GraphicObject.X + GraphicObject.Width, GraphicObject.Y + GraphicObject.Height)
            port4.ConnectorName = "Outlet Port 2"

            GraphicObject.OutputConnectors.Add(port4)

        Else

            GraphicObject.OutputConnectors(0).Position = New DWSIM.DrawingTools.Point.Point(GraphicObject.X + GraphicObject.Width, GraphicObject.Y)
            GraphicObject.OutputConnectors(1).Position = New DWSIM.DrawingTools.Point.Point(GraphicObject.X + GraphicObject.Width, GraphicObject.Y + GraphicObject.Height)

        End If

        GraphicObject.EnergyConnector.Active = False

    End Sub

#End Region

#Region "Classic UI and Cross-Platform UI Editor Support"

    <Xml.Serialization.XmlIgnore> Public editwindow As Editor

    'display the editor on the classic user interface
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

    'this updates the editor window on classic ui
    Public Overrides Sub UpdateEditForm()

        If editwindow IsNot Nothing Then

            If editwindow.InvokeRequired Then

                editwindow.Invoke(Sub()
                                      editwindow?.UpdateInfo()
                                  End Sub)

            Else

                editwindow?.UpdateInfo()

            End If

        End If

    End Sub

    'this closes the editor on classic ui
    Public Overrides Sub CloseEditForm()

        editwindow?.Close()

    End Sub

    'returns the editing form
    Public Overrides Function GetEditingForm() As Form

        Return Nothing

    End Function

    'this function display the properties on the cross-platform user interface
    Public Sub PopulateEditorPanel(container As Object) Implements Interfaces.IExternalUnitOperation.PopulateEditorPanel

        'using extension methods from DWSIM.ExtensionMethods.Eto (DWISM.UI.Shared namespace)

    End Sub

#End Region

End Class