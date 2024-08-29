Namespace Enums
    ''' <summary>
    '''     Process Security and Access Rights
    ''' </summary>
    Friend Class ProcessSecurityAccessRights
        ''' <summary>
        '''     The Microsoft Windows security model enables you to control access to process objects. 
        '''     For more information about security, see Access-Control Model.
        '''     https://msdn.microsoft.com/en-us/library/Aa374876(v=VS.85).aspx
        ''' </summary>
        ''' <remarks>
        '''   See https://msdn.microsoft.com/en-gb/library/windows/desktop/ms684880(v=vs.85).aspx
        ''' </remarks>
        <Flags>
        Friend Enum SecurityFlags As UInteger
            All = &H1F0FFF
            Terminate = &H1
            CreateThread = &H2
            VirtualMemoryOperation = &H8
            VirtualMemoryRead = &H10
            DuplicateHandle = &H40
            CreateProcess = &H80
            SetQuota = &H100
            SetInformation = &H200
            QueryInformation = &H400
            QueryLimitedInformation = &H1000
            Synchronize = &H100000
        End Enum
    End Class
End Namespace