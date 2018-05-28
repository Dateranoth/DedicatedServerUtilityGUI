Public Class MainWindow
    Private Shared WithEvents KeepAlive As New Timer()
    Private Shared exitFlag As Boolean = False
    Private Sub MainWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        KeepAlive.Stop()
        'If Not System.IO.Directory.Exists(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & Application.ProductName) Then
        '    System.IO.Directory.CreateDirectory(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\" & Application.ProductName)
        'End If

        If GlobalVariables.AutoStart Then
            If GlobalFunctions.StartServer(GlobalVariables.ServerID, GlobalVariables.ProcessName, GlobalVariables.ServerPath, GlobalVariables.ServerEXE, GlobalVariables.ServerArgs) Then
                InitializeTimer()
            Else
                MsgBox("Server Failed to Start. Check Logs")
            End If
        End If
        Application.DoEvents()
    End Sub

    Private Shared Sub TimerEventProcessor(ByVal sender As Object, ByVal e As EventArgs) Handles KeepAlive.Tick
        If Not GlobalFunctions.CheckProcessRunning(GlobalVariables.ServerID, GlobalVariables.ProcessName) Then
            KeepAlive.Stop()
            MsgBox("Server Failed")
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If GlobalFunctions.StartServer(GlobalVariables.ServerID, GlobalVariables.ProcessName, GlobalVariables.ServerPath, GlobalVariables.ServerEXE, GlobalVariables.ServerArgs) Then
            InitializeTimer()
            MsgBox("Server Started")
        Else
            MsgBox("Server Failed to Start. Check Logs")
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If GlobalFunctions.StopServer(GlobalVariables.ServerID, GlobalVariables.ProcessName, GlobalVariables.ServerStopCmd) Then
            MsgBox("Server Stopped")
        Else
            MsgBox("Server Failed to Stop")
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        MsgBox(GlobalVariables.ServerPath & "\" & GlobalVariables.ServerEXE)
        MsgBox(GlobalVariables.ProcessName)
        MsgBox(GlobalVariables.ServerPath)
    End Sub

    Private Sub InitializeTimer()
        ' Run this procedure in an appropriate event.
        ' Set to 1 second.
        ' Enable timer.
        KeepAlive.Interval = 1000
        KeepAlive.Enabled = True
    End Sub

End Class
