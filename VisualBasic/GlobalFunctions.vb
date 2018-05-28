Public Class GlobalFunctions
    Public Shared Function StartServer(ByRef ID As Integer, Name As String, Path As String, EXE As String, Args As String) As Boolean
        Dim ServerProcess As New Process()
        Try
            If Not System.IO.File.Exists(Path & "\" & EXE) Then
                Throw New Exception("File Does Not Exist at " & Path & "\" & EXE)
            End If
            If CheckProcessRunning(ID, Name) Then
                'Process Exists
                ServerProcess = Process.GetProcessById(ID)
                Return True
            Else
                ServerProcess.StartInfo.UseShellExecute = True
                ServerProcess.StartInfo.CreateNoWindow = False
                ServerProcess.StartInfo.RedirectStandardInput = False
                ServerProcess.StartInfo.FileName = Path & "\" & EXE
                ServerProcess.StartInfo.Arguments = Args
                ServerProcess.StartInfo.WorkingDirectory = Path
                ServerProcess.Start()
                ServerProcess.WaitForInputIdle()
                ID = ServerProcess.Id
                If ServerProcess.WaitForInputIdle(10000) Then
                    Return True
                Else
                    Throw New Exception("Process Failed to Start after 10 Seconds")
                End If
            End If

        Catch ex As Exception
            Console.WriteLine("StartServer Error: " & ex.Message)
            Return False
        End Try
    End Function

    Public Shared Function StopServer(ByRef ID As Integer, Name As String, Command As String) As Boolean
        Dim ServerProcess As New Process()
        Try
            If CheckProcessRunning(ID, Name) Then
                'Process Exists
                ServerProcess = Process.GetProcessById(ID)
            Else
                ID = 0
                Return True
            End If

            Dim failTime As Date = DateAdd(DateInterval.Second, 60, Now)
            Dim nextTime As Date = DateAdd(DateInterval.Second, 10, Now)
            AppActivate(ID)
            SendKeys.Send(Command)
            While ServerProcess.HasExited.Equals(False)
                If Now >= failTime Then
                    ServerProcess.CloseMainWindow()
                ElseIf Now >= nextTime Then
                    AppActivate(ID)
                    SendKeys.Send(Command)
                    nextTime = DateAdd(DateInterval.Second, 10, Now)
                End If
            End While
            ID = 0
            Return True
        Catch ex As Exception
            Console.WriteLine("StopServer Error: " & ex.Message)
            Return False
        End Try
    End Function

    Public Shared Function CheckProcessRunning(Id As Integer, Name As String) As Boolean
        Try
            For Each proc As Process In Process.GetProcesses
                If proc.Id = Id And proc.ProcessName = Name Then
                    Return True
                    Exit For
                End If
            Next
            Return False
        Catch ex As Exception
            Console.WriteLine("CheckProcessRunning Error: " & ex.Message)
            Return False
        End Try
    End Function

    Public Shared Function GetProcessName(EXE As String) As String
        Try
            Dim TempString As String = EXE
            Dim TempLength As Integer
            TempString = Trim(TempString)
            TempLength = Len(TempString)
            TempString = Left(TempString, (TempLength - 4))
            Return TempString
        Catch ex As Exception
            Console.WriteLine("GetProcessName Error: " & ex.Message)
            Return ""
        End Try
    End Function
End Class
