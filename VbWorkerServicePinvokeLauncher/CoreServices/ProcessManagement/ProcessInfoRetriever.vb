Namespace CoreServices.ProcessManagement

    ''' <summary>
    ''' Provides methods to retrieve information about processes.
    ''' </summary>
    ''' <remarks>
    ''' The <see cref="ProcessInfoRetriever"/> class contains utilities for retrieving process-related information, such as process IDs, based on specific criteria like process names and session identifiers.
    ''' </remarks>
    Friend Class ProcessInfoRetriever

        ''' <summary>
        ''' Gets the process ID of the process with the specified name where the console's session identifier matches the Terminal Services session identifier.
        ''' </summary>
        ''' <param name="processName">
        ''' The name of the process to find. This name is used to identify the process for which the ID is to be retrieved.
        ''' </param>
        ''' <param name="dwSessionId">
        ''' The session identifier of the console session. This value is used to match the session ID of the target process.
        ''' </param>
        ''' <returns>
        ''' The unique identifier (ID) of the process that matches the specified name and session ID. If no matching process is found, the return value is 0.
        ''' </returns>
        ''' <remarks>
        ''' This method retrieves the process ID from a list of processes that match the specified name and are running in the given session. The result is cast to a <c>UInteger</c>.
        ''' </remarks>
        ''' <remarks>
        ''' This method is marked with a suppression attribute because ReSharper may incorrectly suggest 
        ''' that the method should be marked as <c>Shared</c> (static). However, the method is not <c>Shared</c> 
        ''' because it relies on instance-level operations and creating new instances of the class.
        ''' Making it <c>Shared</c> would prevent instance creation and break the intended functionality.
        ''' </remarks>
        <SuppressMessage("StaticMembers", "CA1822:Mark members as static", Justification:="ReSharper incorrectly suggests marking this method as Shared. The method relies on instance-level operations.")>
        Friend Function GetSpecifiedId(processName As String, dwSessionId As UInteger) As UInteger
            Return CUInt((From p In Process.GetProcessesByName(processName) Where p.SessionId = dwSessionId Select p.Id).FirstOrDefault())
        End Function
    End Class
End Namespace
