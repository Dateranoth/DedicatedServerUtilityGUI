Public Class GlobalVariables
    Public Shared ServerID As Integer = My.Settings.LastProcessID
    Public Shared ServerPath As String = System.Text.RegularExpressions.Regex.Match(Trim(My.Settings.ServerPath), "^(.*[^\\])").Value
    Public Shared ServerEXE As String = Trim(My.Settings.ServerEXE)
    Public Shared ProcessName As String = GlobalFunctions.GetProcessName(ServerEXE)
    Public Shared ServerArgs As String = Trim(My.Settings.ServerStartArguments)
    Public Shared ServerStopCmd As String = Trim(My.Settings.ServerStopString)
    Public Shared AutoStart As Boolean = My.Settings.AutoStart
End Class