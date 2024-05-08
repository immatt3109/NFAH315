$file = "C:\Crestron\Projects\C# NFAH\NFAHRooms\bin\Debug\NFAHRooms.cpz"
$ip = "192.168.1.95"
$slotnumber = 1
$username = "admin"
$password = "password"
Send-CrestronProgram -Device $ip -LocalFile $file -ProgramSlot @slotnumber -Secure -Username $username -Password $password